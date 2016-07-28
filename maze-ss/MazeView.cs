using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace maze_ss
{
    public partial class MazeView : PictureBox
    {
        public static readonly Color CELL_COLOR = Color.FromArgb(255, 255, 255);
        public static readonly Color CLEAR_COLOR = Color.FromArgb(64, 64, 64);
        public static readonly Color SEARCH_CELL_COLOR = Color.FromArgb(81, 176, 224);

        public System.Drawing.Drawing2D.InterpolationMode InterpolationMode { get; set; }
        public System.Drawing.Drawing2D.PixelOffsetMode PixelOffsetMode { get; set; }

        private int mazeSize;
        private int gridSize;
        private Bitmap canvas;
        private int new_cell_count = 0;
        
        public MazeView()
        {
            InitializeComponent();
        }

        public void addMazeGen(MazeGen maze_gen)
        {
            maze_gen.AddCell += new MazeGen.AddCellHandler(addCell);
            maze_gen.MazeGenComplete += new EventHandler(mazeComplete);
        }

        public void addMazeSolver(MazeSolver maze_solver)
        {
            maze_solver.SolveStep += new MazeSolver.SolveStepHandler(solveStep);
        }

        public void addCell(Object sender, AddCellEventArgs e)
        {
            Point source = e.source;
            Point new_cell = e.new_cell;
            Point delta = new_cell.subtract(source);

            Graphics g = GetImageGraphics();

            int i = (new_cell.i * 2) + 1 - delta.i;
            int j = (new_cell.j * 2) + 1 - delta.j;
            if (delta.i < 0)
            {
                i += delta.i;
                delta = new Point(-delta.i, delta.j);
            }
            if (delta.j < 0)
            {
                j += delta.j;
                delta = new Point(delta.i, -delta.j);
            }

            g.FillRectangle(new SolidBrush(CELL_COLOR), i, j, 1 + delta.i, 1 + delta.j);
            
            g.Dispose();
            new_cell_count++;
            if (2 <= new_cell_count)
            {
                Refresh();
                new_cell_count = 0;
            }
        }

        public void mazeComplete(Object sender, EventArgs e)
        {
            Refresh();
            new_cell_count = 0;
        }

        public void reset(int size)
        {
            mazeSize = size;
            gridSize = (mazeSize * 2) + 1;

            new_cell_count = 0;
            canvas = new Bitmap(gridSize, gridSize, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Image = canvas;
            Graphics g = Graphics.FromImage(Image);
            g.Clear(CLEAR_COLOR);
            g.Dispose();
        }

        public void solveStep(Object sender, SolveStepEventArgs e)
        {
            Point added_cell = e.added_cell;

            Graphics g = GetImageGraphics();

            g.FillRectangle(new SolidBrush(SEARCH_CELL_COLOR), added_cell.i, added_cell.j, 1, 1);

            g.Dispose();
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.InterpolationMode = InterpolationMode;
            pe.Graphics.PixelOffsetMode = PixelOffsetMode;
            base.OnPaint(pe);
        }

        private Graphics GetImageGraphics()
        {
            Graphics g = Graphics.FromImage(Image);
            g.PixelOffsetMode = PixelOffsetMode;
            return g;
        }
    }

    public enum CellState
    {

    }
}

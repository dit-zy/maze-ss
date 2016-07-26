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

        public System.Drawing.Drawing2D.InterpolationMode InterpolationMode { get; set; }
        public System.Drawing.Drawing2D.PixelOffsetMode PixelOffsetMode { get; set; }

        private int mazeSize;
        private int gridSize;
        private Bitmap canvas;
        
        public MazeView()
        {
            InitializeComponent();
        }

        public void addMazeGen(MazeGen maze_gen)
        {
            maze_gen.AddCell += new MazeGen.AddCellHandler(addCell);
        }

        public void addCell(Object sender, AddCellEventArgs e)
        {
            Point source = e.source;
            Point new_cell = e.new_cell;
            Point delta = new_cell.subtract(source);

            Graphics g = GetCanvasGraphics();

            g.FillRectangle(new SolidBrush(CELL_COLOR), (new_cell.i * 2) + 1, (new_cell.j * 2) + 1, 1, 1);
            g.FillRectangle(new SolidBrush(CELL_COLOR), (new_cell.i * 2) + 1 - delta.i, (new_cell.j * 2) + 1 - delta.j, 1, 1);

            g.Dispose();
            Image = canvas;
        }

        public void reset(int size)
        {
            mazeSize = size;
            gridSize = (mazeSize * 2) + 1;

            canvas = new Bitmap(gridSize, gridSize, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Image = canvas;
            Graphics g = Graphics.FromImage(Image);
            g.Clear(CLEAR_COLOR);
            g.Dispose();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.InterpolationMode = InterpolationMode;
            pe.Graphics.PixelOffsetMode = PixelOffsetMode;
            base.OnPaint(pe);
        }

        private Graphics GetCanvasGraphics()
        {
            Graphics g = Graphics.FromImage(canvas);
            g.PixelOffsetMode = PixelOffsetMode;
            return g;
        }
    }

    public enum CellState
    {

    }
}

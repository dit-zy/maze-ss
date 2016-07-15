using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace maze_ss
{
    public partial class ScreenSaverForm : Form
    {
        public static readonly Color CLEAR_CELL_COLOR = Color.FromArgb(255, 255, 255);
        public static readonly Color NEW_CELL_COLOR = Color.FromArgb(63, 195, 235);
        public static readonly Color WALL_COLOR = Color.FromArgb(43, 39, 30);

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        private Point mouseLocation;
        private bool previewMode = false;
        private Random rand = new Random();

        private static int mazeSize = 31;
        private static int gridSize = mazeSize * 2 + 1;
        private Bitmap mazeImage;
        private int[,] grid = new int[gridSize, gridSize];

        private Maze maze;
        private int last_added_i;
        private int last_added_j;

        private List<int[,]> cell_build_order;

        public ScreenSaverForm(Rectangle Bounds)
        {
            InitializeComponent();
            this.Bounds = Bounds;

            maze = new Maze(mazeSize);
        }

        public void CellAdded(int i, int j, int di, int dj)
        {
            Graphics g = Graphics.FromImage(mazeImage);
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            //g.FillRectangle(new SolidBrush(CLEAR_CELL_COLOR), 1 + last_added_i * 2, 1 + last_added_j * 2, 1, 1);
            g.FillRectangle(new SolidBrush(CLEAR_CELL_COLOR), 1 + (i * 2) + di, 1 + (j * 2) + dj, 1, 1);
            //g.FillRectangle(new SolidBrush(NEW_CELL_COLOR), 1 + i * 2, 1 + j * 2, 1, 1);
            g.FillRectangle(new SolidBrush(CLEAR_CELL_COLOR), 1 + i * 2, 1 + j * 2, 1, 1);

            g.Dispose();

            last_added_i = i;
            last_added_j = j;

            //mazeImage = buffer;
            imageBox.Image = mazeImage;
        }

        public ScreenSaverForm(IntPtr PreviewWndHandle)
        {
            InitializeComponent();

            // Set the preview window as the parent of this window
            SetParent(this.Handle, PreviewWndHandle);

            // Make this a child window so it will close when the parent dialog closes
            // GWL_STYLE = -16, WS_CHILD = 0x40000000
            SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));

            // Place our window inside the parent
            Rectangle ParentRect;
            GetClientRect(PreviewWndHandle, out ParentRect);
            Size = ParentRect.Size;
            Location = new Point(0, 0);

            previewMode = true;
        }

        private void ScreenSaverForm_Load(object sender, EventArgs e)
        {
            //Cursor.Hide();
            //TopMost = true;

            int side;
            if (Bounds.Height <= Bounds.Width)
                side = Bounds.Height;
            else
                side = Bounds.Width;

            imageBox.Location = new Point((Bounds.Width - side) / 2, (Bounds.Height - side) / 2);
            imageBox.Size = new Size(side, side);

            mazeImage = new Bitmap(gridSize, gridSize, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(mazeImage);
            g.Clear(WALL_COLOR);
            g.Dispose();

            imageBox.Image = mazeImage;
            
            last_added_i = mazeSize / 2;
            last_added_j = last_added_i;
            maze.Initialize(last_added_i, last_added_j);
            cell_build_order = maze.GetCellBuildOrder();

            maze_gen_timer.Start();
        }

        private void ScreenSaverForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (!previewMode)
            {
                if (!mouseLocation.IsEmpty)
                {
                    // Terminate if mouse is moved a significant distance
                    if (Math.Abs(mouseLocation.X - e.X) > 5 ||
                        Math.Abs(mouseLocation.Y - e.Y) > 5)
                        Application.Exit();
                }

                // Update current mouse location
                mouseLocation = e.Location;
            }
        }

        private void ScreenSaverForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!previewMode)
            {
                Application.Exit();
            }
        }

        private void ScreenSaverForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (!previewMode)
            {
                Application.Exit();
            }
        }

        private void imageBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!previewMode)
            {
                if (!mouseLocation.IsEmpty)
                {
                    // Terminate if mouse is moved a significant distance
                    if (Math.Abs(mouseLocation.X - e.X) > 5 ||
                        Math.Abs(mouseLocation.Y - e.Y) > 5)
                        Application.Exit();
                }

                // Update current mouse location
                mouseLocation = e.Location;
            }
        }

        private void imageBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (!previewMode)
            {
                Application.Exit();
            }
        }

        private void maze_gen_timer_Tick(object sender, EventArgs e)
        {
            if(0 == cell_build_order.Count)
            {
                maze_gen_timer.Stop();
                CellAdded(-100, -100, 0, 0);
                return;
            }

            int[,] next_cell = cell_build_order.First();
            cell_build_order.RemoveAt(0);
            CellAdded(next_cell[0, 0], next_cell[0, 1], next_cell[1, 0], next_cell[1, 1]);
        }
    }
}

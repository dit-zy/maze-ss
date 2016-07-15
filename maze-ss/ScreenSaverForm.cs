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
        private System.Drawing.Point mouseLocation;
        private MazeGen maze_gen;
        private Timer maze_gen_timer;

        private Random seed_generator;

        public ScreenSaverForm(Rectangle Bounds, int seed)
        {
            InitializeComponent();
            this.Bounds = Bounds;

            seed_generator = new Random(seed);
        }

        private void ScreenSaverForm_Load(object sender, EventArgs e)
        {
            Cursor.Hide();
            TopMost = true;
            start();
        }

        async private void delayStart()
        {
            await Task.Delay(2000);

            start();
        }

        private void start()
        {
            maze_gen = new MazeGen(seed_generator.Next());
            maze_gen.MazeGenComplete += new EventHandler(mazeGenComplete);

            mazeView.reset();
            mazeView.addMazeGen(maze_gen);

            maze_gen_timer = new Timer();
            maze_gen_timer.Interval = 20;
            maze_gen_timer.Tick += new EventHandler(maze_gen.timer_tick);

            maze_gen_timer.Start();
        }

        public void mazeGenComplete(Object sender, EventArgs e)
        {
            maze_gen_timer.Stop();
            maze_gen_timer.Dispose();
            maze_gen_timer = null;

            maze_gen = null;

            delayStart();
        }

        private void ScreenSaverForm_MouseMove(object sender, MouseEventArgs e)
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

        private void ScreenSaverForm_KeyDown(object sender, KeyEventArgs e)
        {
            Application.Exit();
        }

        private void ScreenSaverForm_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }
    }
}

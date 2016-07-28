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
        public static readonly int GEN_TIMER_INTERVAL = 10;
        public static readonly int SOLVER_TIMER_INTERVAL = 10;
        public static readonly int MAZE_SIZE = 31;

        private System.Drawing.Point mouseLocation;
        private MazeGen maze_gen;
        private Timer maze_timer;
        private MazeSolver maze_solver;

        private Random rand;

        public ScreenSaverForm(Rectangle Bounds, int seed)
        {
            InitializeComponent();
            this.Bounds = Bounds;

            rand = new Random(seed);
        }

        private void ScreenSaverForm_Load(object sender, EventArgs e)
        {
            //Cursor.Hide();
            //TopMost = true;

            delayStart();
        }

        async private void delayStart()
        {
            await Task.Delay(2000);

            start();
        }

        private void start()
        {
            maze_gen = new MazeGen(MAZE_SIZE, rand.Next());
            maze_gen.MazeGenComplete += new EventHandler(mazeGenComplete);

            maze_solver = new MazeSolver(MAZE_SIZE);
            maze_solver.addMazeGen(maze_gen);
            maze_solver.Solution += new MazeSolver.SolutionHandler(solverSolution);

            mazeView.reset(MAZE_SIZE);
            mazeView.addMazeGen(maze_gen);
            mazeView.addMazeSolver(maze_solver);

            maze_timer = new Timer();
            maze_timer.Interval = GEN_TIMER_INTERVAL;
            maze_timer.Tick += new EventHandler(maze_gen.timer_tick);

            maze_timer.Start();
        }

        private void solverSolution(object sender, SolutionEventArgs e)
        {
            maze_timer.Stop();
            maze_timer.Dispose();
            maze_timer = null;

            maze_solver = null;

            delayStart();
        }

        public void mazeGenComplete(Object sender, EventArgs e)
        {
            maze_timer.Stop();
            maze_timer.Dispose();
            maze_gen = null;

            maze_timer = new Timer();
            maze_timer.Interval = SOLVER_TIMER_INTERVAL;
            maze_solver.addTimer(maze_timer);

            int i = rand.Next(2);
            int j = rand.Next(2);
            Point start = new Point((MAZE_SIZE - 1) * i, (MAZE_SIZE - 1) * j);
            Point goal = new Point((MAZE_SIZE - 1) * (1 - i), (MAZE_SIZE - 1) * (1 - j));
            maze_solver.init(start, goal);

            maze_timer.Start();
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

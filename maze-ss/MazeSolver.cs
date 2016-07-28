using Wintellect.PowerCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maze_ss
{
    public class MazeSolver
    {
        private readonly Point[] neighbor_deltas = new Point[]
        {
                new Point( 1,  0),
                new Point( 0,  1),
                new Point(-1,  0),
                new Point( 0, -1)
        };

        public delegate void SolveStepHandler(Object sender, SolveStepEventArgs e);
        public delegate void SolutionHandler(Object sender, SolutionEventArgs e);
        public event SolveStepHandler SolveStep;
        public event SolutionHandler Solution;

        int[,] cell;
        Point[,] cell_parent;
        Point goal;
        Point maze_size;

        OrderedBag<SearchCell> search_queue;

        public MazeSolver(Point mazeSize)
        {
            this.maze_size = (mazeSize.multiply(2)).add(1, 1);
            cell = new int[maze_size.i, maze_size.j];
            cell_parent = new Point[maze_size.i, maze_size.j];
        }

        protected virtual void OnSolveStep(SolveStepEventArgs e)
        {
            SolveStep?.Invoke(this, e);
        }

        protected virtual void OnSolution()
        {
            Solution?.Invoke(this, new SolutionEventArgs());
        }

        public void addMazeGen(MazeGen mazeGen)
        {
            mazeGen.AddCell += new MazeGen.AddCellHandler(addCell);
        }

        public void addTimer(System.Windows.Forms.Timer t)
        {
            t.Tick += timerTick;
        }

        public void init(Point start, Point goal)
        {
            goal = new Point((goal.i * 2) + 1, (goal.j * 2) + 1);
            this.goal = goal;
            start = new Point((start.i * 2) + 1, (start.j * 2) + 1);
            cell_parent[start.i, start.j] = start;
            search_queue = new OrderedBag<SearchCell>();
            search_queue.Add(new SearchCell(start, maze_size.i + maze_size.j - 2, 0));
        }

        private void step()
        {
            SearchCell next = search_queue.GetFirst();
            search_queue.RemoveFirst();
            cell[next.cell.i, next.cell.j] = 2;
            
            if(next.cell.equals(goal))
            {
                OnSolution();
                return;
            }

            OnSolveStep(new SolveStepEventArgs(next.cell));

            foreach (Point delta in neighbor_deltas)
            {
                Point neighbor = next.cell.add(delta);
                if (neighbor.inBounds(maze_size.i, maze_size.j) && cell[neighbor.i, neighbor.j] == 1)
                {
                    cell[neighbor.i, neighbor.j] = 3;
                    cell_parent[neighbor.i, neighbor.j] = next.cell;
                    int neighbor_g = next.g + 1;
                    int neighbor_h = Math.Abs(goal.i - neighbor.i) + Math.Abs(goal.j - neighbor.j);
                    search_queue.Add(new SearchCell(neighbor, neighbor_g + neighbor_h, neighbor_g));
                }
            }
        }

        private void timerTick(object sender, EventArgs e)
        {
            step();
        }

        public void addCell(Object sender, AddCellEventArgs e)
        {
            Point delta = e.new_cell.subtract(e.source);
            cell[(e.new_cell.i * 2) + 1, (e.new_cell.j * 2) + 1] = 1;
            cell[(e.new_cell.i * 2) + 1 - delta.i, (e.new_cell.j * 2) + 1 - delta.j] = 1;
        }
    }

    public class SolveStepEventArgs : EventArgs
    {
        public readonly Point added_cell;

        public SolveStepEventArgs(Point cell)
        {
            added_cell = new Point(cell);
        }
    }

    public class SolutionEventArgs : EventArgs
    {

    }

    public class SearchCell : IComparable<SearchCell>
    {
        public readonly Point cell;
        public readonly int f;
        public readonly int g;

        public SearchCell(Point cell, int f, int g)
        {
            this.cell = cell;
            this.f = f;
            this.g = g;
        }

        public int CompareTo(SearchCell other)
        {
            return f - other.f;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maze_ss
{
    public class MazeGen
    {
        public delegate void AddCellHandler(Object sender, AddCellEventArgs e);
        public event AddCellHandler AddCell;
        public event EventHandler MazeGenComplete;

        private int mazeSize;
        private Random rand;
        private Stack<AddCellEventArgs> search_stack;
        private bool[,] visited_cells;

        protected virtual void OnAddCell(AddCellEventArgs e)
        {
            AddCell?.Invoke(this, e);
        }

        protected virtual void OnMazeGenComplete()
        {
            MazeGenComplete?.Invoke(this, EventArgs.Empty);
        }

        public MazeGen(int mazeSize, int seed)
        {
            this.mazeSize = mazeSize;

            rand = new Random(seed);
            search_stack = new Stack<AddCellEventArgs>(mazeSize);
            visited_cells = new bool[mazeSize, mazeSize];

            Point starting_point = new Point(mazeSize / 2, mazeSize / 2);
            search_stack.Push(new AddCellEventArgs(starting_point, starting_point));
        }

        public void timer_tick(Object sender, EventArgs e)
        {
            genNextCell();
        }

        private void genNextCell()
        {
            AddCellEventArgs stack_top = search_stack.Pop();
            while(0 < search_stack.Count && visited_cells[stack_top.new_cell.i, stack_top.new_cell.j])
            {
                stack_top = search_stack.Pop();
            }

            if(!visited_cells[stack_top.new_cell.i, stack_top.new_cell.j])
            {
                addCell(stack_top);
            }
            else
            { 
                OnMazeGenComplete();
            }
        }

        private void addCell(AddCellEventArgs stack_top)
        {
            OnAddCell(stack_top);
            Point nextCell = stack_top.new_cell;
            visited_cells[nextCell.i, nextCell.j] = true;

            Point[] neighbor_deltas = new Point[]
            {
                new Point( 1,  0),
                new Point( 0,  1),
                new Point(-1,  0),
                new Point( 0, -1)
            };
            List<Point> neighbors = new List<Point>(4);
            foreach (Point delta in neighbor_deltas)
            {
                Point neighbor = nextCell.add(delta);
                if (0 <= neighbor.i && neighbor.i < mazeSize && 0 <= neighbor.j && neighbor.j < mazeSize)
                {
                    neighbors.Add(neighbor);
                }
            }

            while (0 < neighbors.Count)
            {
                int i = rand.Next(neighbors.Count);
                search_stack.Push(new AddCellEventArgs(nextCell, neighbors[i]));
                neighbors.RemoveAt(i);
            }
        }
    }

    public class AddCellEventArgs : EventArgs
    {
        public Point source;
        public Point new_cell;

        public AddCellEventArgs(Point source, Point new_cell)
        {
            this.source = source;
            this.new_cell = new_cell;
        }
    }
}

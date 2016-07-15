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

        protected virtual void OnAddCell(AddCellEventArgs e)
        {
            if(AddCell != null)
            {
                AddCell(this, e);
            }
        }

        public MazeGen()
        {
            
        }

        public void timer_tick(Object sender, EventArgs e)
        {

        }
    }

    public class AddCellEventArgs : EventArgs
    {
        Point cell;

        public AddCellEventArgs(Point cell)
        {
            this.cell = cell;
        }
    }
}

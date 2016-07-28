using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maze_ss
{
    public class Point
    {
        public int i { get; }
        public int j { get; }

        public Point(int i, int j)
        {
            this.i = i;
            this.j = j;
        }

        public Point(Point original)
        {
            i = original.i;
            j = original.j;
        }

        public bool equals(Point other)
        {
            return i == other.i && j == other.j;
        }

        public Point add(int di, int dj)
        {
            return new Point(i + di, j + dj);
        }

        public Point add(Point p)
        {
            return add(p.i, p.j);
        }

        public Point subtract(Point p)
        {
            return new Point(i - p.i, j - p.j);
        }

        public Point negate()
        {
            return new Point(-i, -j);
        }

        public bool inBounds(int iBound, int jBound)
        {
            return 0 <= i && i < iBound && 0 <= j && j < jBound;
        }

        public Point multiply(int multiple)
        {
            return new Point(i * multiple, j * multiple);
        }
    }
}

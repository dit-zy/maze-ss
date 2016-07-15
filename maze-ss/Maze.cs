using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maze_ss
{
    public class Maze
    {
        private int size;
        private int[,] cells;
        private List<Pair<Pair<int, int>, Pair<int, int>>> cell_build_order;

        public Maze(int size)
        {
            this.size = size;
            cells = new int[size * 2 + 1, size * 2 + 1];

            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    cells[1 + i * 2, 1 + j * 2] = 1;
                }
            }

            cell_build_order = new List<Pair<Pair<int, int>, Pair<int, int>>>(size ^ 2);
        }

        public void Initialize(int starting_x, int starting_y)
        {
            Pair<int,int>[] neighbor_deltas = new Pair<int,int>[4]
            {
                new Pair<int, int>( 0,  1),
                new Pair<int, int>( 1,  0),
                new Pair<int, int>( 0, -1),
                new Pair<int, int>(-1,  0),
            };
            bool[,] visited = new bool[size, size];
            bool[,] in_search_space = new bool[size, size];
            List<Pair<int, int>> search_space = new List<Pair<int, int>>(size);

            Pair<int, int> start = new Pair<int, int>(starting_x, starting_y);
            search_space.Add(start);
            in_search_space[start.u, start.v] = true;

            Random rand = new Random();
            while (0 < search_space.Count)
            {
                int i = rand.Next(search_space.Count);
                Pair<int, int> next = search_space.ElementAt(i);
                search_space.RemoveAt(i);

                visited[next.u, next.v] = true;

                List<Pair<int, int>> connection_candidates = new List<Pair<int, int>>(4);
                foreach (Pair<int, int> delta in neighbor_deltas)
                {
                    Pair<int, int> neighbor = addPairs(next, delta);
                    if(0 <= neighbor.u && neighbor.u < size && 0 <= neighbor.v && neighbor.v < size)
                    {
                        if(visited[neighbor.u, neighbor.v])
                        {
                            connection_candidates.Add(delta);
                        }
                        else if(!in_search_space[neighbor.u, neighbor.v])
                        {
                            search_space.Add(neighbor);
                            in_search_space[neighbor.u, neighbor.v] = true;
                        }
                    }
                }

                Pair<int, int> conn_delta = new Pair<int, int>(0, 0);
                if(0 < connection_candidates.Count)
                {
                    conn_delta = connection_candidates.ElementAt(rand.Next(connection_candidates.Count));
                    cells[1 + (next.u * 2) + conn_delta.u, 1 + (next.v * 2) + conn_delta.v] = 2;
                }

                cell_build_order.Add(new Pair<Pair<int, int>, Pair<int, int>>(next, conn_delta));
            }
        }

        public List<int[,]> GetCellBuildOrder()
        {
            List<int[,]> return_list = new List<int[,]>(size ^ 2);
            foreach (Pair<Pair<int, int>, Pair<int, int>> added_cell in cell_build_order)
            {
                return_list.Add(new int[2, 2] { { added_cell.u.u, added_cell.u.v }, { added_cell.v.u, added_cell.v.v } });
            }

            return return_list;
        }

        private Pair<int, int> addPairs(Pair<int, int> n, Pair<int, int> m)
        {
            return new Pair<int, int>(n.u + m.u, n.v + m.v);
        }

        private class Pair<U, V>
        {
            public U u;
            public V v;

            public Pair(U u, V v)
            {
                this.u = u;
                this.v = v;
            }
        }
    }
}

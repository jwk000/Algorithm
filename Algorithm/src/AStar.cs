using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * AStar算法是一种静态路网中求解最短路径最有效的直接搜索方法
     * open列表：一个记录所有被考虑来寻找最短路径的网格集合
     * close列表：一个记录下不会被考虑的网格集合
     * G ：表示从起点方格移动到网格上指定方格的移动耗费 (可沿斜方向移动).
     * H ：表示从指定的方格移动到终点方格的预计耗费 (H启发函数).
     * F ：此格子的评估值，F=G+H
     * 
     * 步骤：
     * 1. 起点加入open集合；
     * 2. 寻找open集合里F值最小的节点A；
     * 3. 遍历A的可达节点i（i不能在close集合中），计算F，如果F<i.F，则把i的父节点记为A，i加入open集合；A从open集合移入close集合；
     * 4. 重复2-3，直到终点被加入了close集合，则找到最短路径；
     * 5. 如果open集合为空，则不存在起点到终点的路径；
     */
    public class AStar
    {
        public class Grid :IComparable<Grid>
        {
            public int X,Y;
            public int F, G, H;
            public Grid Parent;
            public int GridType;

            public int CompareTo(Grid other)
            {
                return F - other.F;
            }
        }

        const int Walkable = 0;
        const int Block = 1;
        const int Start = 2;
        const int End = 3;

        Grid[,] mGrids; //grid[i]=g表示格子i的数据为g

        PriorityQueue<Grid> mOpenSet = new PriorityQueue<Grid>();
        HashSet<Grid> mCloseSet = new HashSet<Grid>();

        //map[x,y]=t表示坐标x,y处的格子类型为t（0空地 1障碍 2起点 3终点）
        public List<Grid> Search(int[,]map)
        {
            int row = map.GetLength(0);
            int col = map.GetLength(1);
            mGrids = new Grid[row,col];

            Grid start=null, end=null;
            for(int x = 0; x < row; x++)
            {
                for(int y = 0; y < col; y++)
                {
                    mGrids[x, y] = new Grid()
                    {
                        X = x,
                        Y = y,
                        F = -1,
                        GridType = map[x, y]
                    };
                    if (mGrids[x, y].GridType == Start)
                    {
                        start = mGrids[x, y];
                    }else if (mGrids[x, y].GridType == End)
                    {
                        end = mGrids[x, y];
                    }
                }
            }

            mOpenSet.Push(start);
            while (mOpenSet.Size > 0)
            {
                Grid g = mOpenSet.Pop();
                mCloseSet.Add(g);
                if (g == end)
                {
                    break;
                }
                for(int i = -1; i <= 1; i++)
                {
                    for(int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0) continue;
                        Grid r = mGrids[g.X + i, g.Y + j];
                        if(r.GridType==Walkable && !mCloseSet.Contains(r))
                        {
                            int d = 10;
                            if (i != 0 && j != 0) d = 14;//斜线是1.4倍距离
                            int G = g.G + d;
                            int H = (r.X - end.X) * (r.X - end.X) + (r.Y - end.Y) * (r.Y - end.Y);//欧几里得距离
                            int F = G + H;
                            if (r.F < 0 || F < r.F)
                            {
                                r.F = F;
                                r.G = G;
                                r.H = H;
                                r.Parent = g;
                                mOpenSet.Push(r);
                            }
                        }
                    }
                }
            }

            List<Grid> path = new List<Grid>();
            path.Add(end);
            var p = end.Parent;
            while (p != start)
            {
                path.Add(p);
                p = p.Parent;
            }
            path.Add(start);
            path.Reverse();

            return path;
        }


    }
}

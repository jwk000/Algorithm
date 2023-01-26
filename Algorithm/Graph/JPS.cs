using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    /*
     * Jps，Jump Point Search,跳点搜索，也有人称之为“拐点寻路”。
     * 寻路过程中需要保存有效点的集合，分为可探索点集合openList，已探索点集合closeList
     * 同A星的概念 g为起点经过其他点到当前点的代价和，h为到目标点的代价，f为当前点的与起点终点间价值的和即f=g+h。
     * 节点 x 的8个邻居中有障碍，且 x 的父节点 p 经过x 到达 n 的距离代价比不经过 x 到达的 n 的任意路径的距离代价小，则称 n 是 x 的强迫邻居。
     * 跳点：当前点 x 满足以下三个条件之一：
     * a) 节点 x 是起点/终点。
     * b) 节点 x 至少有一个强迫邻居。
     * c) 如果父节点在斜方向（意味着这是斜向搜索），节点x的水平或垂直方向上有满足条件a，b的点。
     * 
     * 搜索规则
     * 1. 在搜索时，如果直线和斜向都能通过，先在直线方向搜索跳点，然后在斜向上搜索跳点。
     * 2. n是x的邻居，若有从P(x)到n且不经过x的路径，且该路径小于等于从P(x)经过x到达n的路径的长度，那么走到x后下一个节点不会走到n。
     *    也就是说除了强迫邻居和前进方向的3个邻居，其他邻居不计算。
     * 3. 只有跳点才会被加入openlist里，最后找出来的最短路径也是由跳点组成。
     * 
     * ref:https://www.cnblogs.com/KillerAery/p/12242445.html
     * ref:https://zhuanlan.zhihu.com/p/181734749
     */
    public class JPS
    {
        public class Grid : IComparable<Grid>
        {
            public int X, Y;
            public int F, G, H;
            public Grid Parent;
            public int GridType;
            public bool Searched;

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
        Grid start = null, end = null;
        int row, col;
        PriorityQueue<Grid> mOpenQueue = new PriorityQueue<Grid>();
        HashSet<Grid> mCloseSet = new HashSet<Grid>();
        HashSet<Grid> mOpenSet = new HashSet<Grid>();

        //map[x,y]=t表示坐标x,y处的格子类型为t（0空地 1障碍 2起点 3终点）
        public void Init(int[,] map)
        {
            row = map.GetLength(0);
            col = map.GetLength(1);
            mGrids = new Grid[row, col];

            for (int x = 0; x < row; x++)
            {
                for (int y = 0; y < col; y++)
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
                    }
                    else if (mGrids[x, y].GridType == End)
                    {
                        end = mGrids[x, y];
                    }
                }
            }

        }

        public List<Grid> Search()
        {
            mOpenQueue.Push(start);
            while (mOpenQueue.Size > 0)
            {
                Grid current = mOpenQueue.Pop();
                mOpenSet.Remove(current);
                mCloseSet.Add(current);
                if (current == end)
                {
                    return TraceBackPath();
                }
                var jumps = GetJumpPoints(current);//只寻找跳点
                foreach (var jumpPoint in jumps)
                {
                    if (mCloseSet.Contains(jumpPoint))
                    {
                        continue;
                    }
                    int G = current.G + CalcDistance(jumpPoint, current);
                    int H = CalcDistance(jumpPoint, end);
                    int F = G + H;
                    if (jumpPoint.F < 0 || F < jumpPoint.F)
                    {
                        jumpPoint.G = G;
                        jumpPoint.H = H;
                        jumpPoint.F = F;
                        jumpPoint.Parent = current;
                        if (!mOpenSet.Contains(jumpPoint))
                        {
                            mOpenSet.Add(jumpPoint);
                            mOpenQueue.Push(jumpPoint);
                        }
                        else
                        {
                            mOpenQueue.Update(jumpPoint);
                        }
                    }
                }
            }

            return null;
        }

        List<Grid> TraceBackPath()
        {
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

        int CalcDistance(Grid A, Grid B)
        {
            int dx = Math.Abs(A.X - B.X);
            int dy = Math.Abs(A.Y - B.Y);
            int D = 0;
            if (dx > dy)
            {
                D = dy * 14 + (dx - dy) * 10;
            }
            else
            {
                D = dx * 14 + (dy - dx) * 10;
            }
            return D;
        }



        Grid GetGrid(int x, int y)
        {
            if (x < 0 || x >= row || y < 0 || y >= col)
            {
                return null;
            }
            return mGrids[x, y];
        }

        bool IsWalkable(Grid g)
        {
            if (g != null)
            {
                g.Searched = true;
                return g.GridType != Block;
            }
            return false;
        }

        //沿着邻居的方向寻找跳点
        List<Grid> GetJumpPoints(Grid current)
        {
            List<Grid> ret = new List<Grid>();
            var neighbors = FindNeighbors(current);
            foreach (var n in neighbors)
            {
                var p = Jump(n, current);
                if (p != null)
                {
                    ret.Add(p);
                }
            }
            return ret;
        }

        //寻找邻居对应搜索规则2
        List<Grid> FindNeighbors(Grid current)
        {
            List<Grid> ret = new List<Grid>();
            if (current == start)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0) continue;
                        var g = mGrids[current.X + i, current.Y + j];
                        if (g.GridType == Walkable)
                        {
                            ret.Add(g);
                        }
                    }
                }
            }
            else
            {
                var p = current.Parent;
                int Dx = MyMath.clamp(current.X - p.X, -1, 1);
                int Dy = MyMath.clamp(current.Y - p.Y, -1, 1);

                var up = GetGrid(current.X, current.Y + Dy);
                var down = GetGrid(current.X, current.Y - Dy);
                var left = GetGrid(current.X - Dx, current.Y);
                var right = GetGrid(current.X + Dx, current.Y);
                var leftup = GetGrid(current.X - Dx, current.Y + Dy);
                var rightup = GetGrid(current.X + Dx, current.Y + Dy);
                var rightdown = GetGrid(current.X + Dx, current.Y - Dy);

                if (Dx != 0 && Dy != 0)//对角方向(rightup就是前方，所有方位都是相对位置)
                {

                    if (IsWalkable(up))
                    {
                        ret.Add(up);
                    }
                    if (IsWalkable(right))
                    {
                        ret.Add(right);
                    }
                    if ((IsWalkable(up) || IsWalkable(right)) && IsWalkable(rightup))
                    {
                        ret.Add(rightup);
                    }
                    if (!IsWalkable(left) && IsWalkable(up) && IsWalkable(leftup))//leftup是current的强迫邻居
                    {
                        ret.Add(leftup);
                    }
                    if (!IsWalkable(down) && IsWalkable(right) && IsWalkable(rightdown))//强迫邻居
                    {
                        ret.Add(rightdown);
                    }
                }
                else if (Dx == 0)//垂直方向
                {
                    if (IsWalkable(up))
                    {
                        ret.Add(up);

                        right = GetGrid(current.X + 1, current.Y);
                        rightup = GetGrid(current.X + 1, current.Y + Dy);
                        left = GetGrid(current.X - 1, current.Y);
                        leftup = GetGrid(current.X - 1, current.Y + Dy);

                        if (!IsWalkable(right) && IsWalkable(rightup))
                        {
                            ret.Add(rightup);
                        }
                        if (!IsWalkable(left) && IsWalkable(leftup))
                        {
                            ret.Add(leftup);
                        }
                    }
                }
                else //水平方向
                {
                    if (IsWalkable(right))
                    {
                        ret.Add(right);

                        up = GetGrid(current.X, current.Y + 1);
                        rightup = GetGrid(current.X + Dx, current.Y + 1);
                        down = GetGrid(current.X, current.Y - 1);
                        rightdown = GetGrid(current.X + Dx, current.Y - 1);

                        if (!IsWalkable(up) && IsWalkable(rightup))
                        {
                            ret.Add(rightup);
                        }
                        if (!IsWalkable(down) && IsWalkable(rightdown))
                        {
                            ret.Add(rightdown);
                        }
                    }
                }
            }

            return ret;
        }

        //从current开始寻找下个跳点（终点或者有强迫邻居的点）
        Grid Jump(Grid current, Grid parent)
        {
            if (!IsWalkable(current))
            {
                return null;
            }
            if (current == end)
            {
                return current;
            }

            int Dx = MyMath.clamp(current.X - parent.X, -1, 1);
            int Dy = MyMath.clamp(current.Y - parent.Y, -1, 1);

            var up = GetGrid(current.X, current.Y + Dy);
            var down = GetGrid(current.X, current.Y - Dy);
            var left = GetGrid(current.X - Dx, current.Y);
            var right = GetGrid(current.X + Dx, current.Y);
            var leftup = GetGrid(current.X - Dx, current.Y + Dy);
            var rightup = GetGrid(current.X + Dx, current.Y + Dy);
            var rightdown = GetGrid(current.X + Dx, current.Y - Dy);
            var forward = rightup;

            if (Dx != 0 && Dy != 0)//对角方向
            {
                //有强迫邻居
                if ((IsWalkable(rightdown) && !IsWalkable(down)) || (IsWalkable(leftup) && !IsWalkable(left)))
                {
                    return current;
                }
                //横向寻找跳点
                if (Jump(right, current) != null)
                {
                    return current;
                }
                //纵向寻找跳点
                if (Jump(up, current) != null)
                {
                    return current;
                }
            }
            else if (Dx == 0)// 纵向
            {
                right = GetGrid(current.X + 1, current.Y);
                rightup = GetGrid(current.X + 1, current.Y + Dy);
                left = GetGrid(current.X - 1, current.Y);
                leftup = GetGrid(current.X - 1, current.Y + Dy);

                //强迫邻居
                if ((!IsWalkable(right) && IsWalkable(rightup)) || (!IsWalkable(left) && IsWalkable(leftup)))
                {
                    return current;
                }
            }
            else //横向
            {
                up = GetGrid(current.X, current.Y + 1);
                rightup = GetGrid(current.X + Dx, current.Y + 1);
                down = GetGrid(current.X, current.Y - 1);
                rightdown = GetGrid(current.X + Dx, current.Y - 1);

                //强迫邻居
                if ((!IsWalkable(up) && IsWalkable(rightup)) || (!IsWalkable(down) && IsWalkable(rightdown)))
                {
                    return current;
                }
            }

            //前进方向寻找跳点（横向只会沿着横向找，纵向之后沿着纵向找，对角只会沿着对角找）
            return Jump(forward, current);
        }
    }
}

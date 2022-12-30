using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Algorithm;

namespace VisualAlgorithm
{
    public partial class PathFinder : Form
    {
        public PathFinder()
        {
            InitializeComponent();
            InitGrids();
            ClientSize = new Size(ColNum * GridSize, (1 + RowNum) * GridSize);
            BackColor = Color.White;
            DoubleBuffered = true;
        }

        const int GridSize = 32;
        const int RowNum = 20;
        const int ColNum = 20;
        enum GridType
        {
            Blank,
            Block,
            Start,
            End
        }
        class Grid : IComparable<Grid>
        {
            public GridType GridType;
            public int Y, X;
            public Rectangle rect;
            public bool Searched = false;
            public Grid Parent = null;
            public int G, H, F;

            public int CompareTo(Grid other)
            {
                return F - other.F;
            }

        }

        Grid[,] AllGrids = new Grid[ColNum, RowNum];
        Point[] ThePath = null;

        void InitGrids()
        {
            for (int r = 0; r < RowNum; r++)
            {
                for (int c = 0; c < ColNum; c++)
                {
                    AllGrids[c, r] = new Grid
                    {
                        GridType = GridType.Blank,
                        X = c,
                        Y = r,
                        rect = new Rectangle(c * GridSize, (1 + r) * GridSize, GridSize, GridSize)
                    };
                }
            }
        }

        void ResetGrids()
        {
            foreach (var g in AllGrids)
            {
                g.Searched = false;
                g.Parent = null;
                g.G = 0;
                g.H = 0;
                g.F = -1;
            }

        }

        Pen gridpen = new Pen(Color.Brown, 2);

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //网格
            for (int i = 0; i < ColNum; i++)
            {
                for (int j = 0; j < RowNum; j++)
                {
                    var g = AllGrids[i, j];
                    if (g.Searched)
                    {
                        e.Graphics.FillRectangle(Brushes.AliceBlue, g.rect);
                    }

                    if (g.GridType == GridType.Block)
                    {
                        e.Graphics.FillRectangle(Brushes.Black, g.rect);
                    }
                    else if (g.GridType == GridType.Start)
                    {
                        e.Graphics.FillRectangle(Brushes.Red, g.rect);
                    }
                    else if (g.GridType == GridType.End)
                    {
                        e.Graphics.FillRectangle(Brushes.Blue, g.rect);
                    }

                    e.Graphics.DrawRectangle(Pens.Gray, g.rect);

                }
            }

            //路径
            if (ThePath != null)
            {
                e.Graphics.DrawLines(gridpen, ThePath);
            }
        }

        Grid startGrid, endGrid;
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            int c = e.X / GridSize;
            int r = e.Y / GridSize;
            var grid = AllGrids[c, r - 1];
            if (e.Button == MouseButtons.Left)
            {
                if (grid.GridType == GridType.Blank)
                {
                    grid.GridType = GridType.Block;

                    if (startDown)
                    {
                        if (startGrid != null)
                        {
                            startGrid.GridType = GridType.Blank;
                        }
                        grid.GridType = GridType.Start;
                        startGrid = grid;
                    }
                    else if (endDown)
                    {
                        if (endGrid != null)
                        {
                            endGrid.GridType = GridType.Blank;
                        }
                        grid.GridType = GridType.End;
                        endGrid = grid;
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (grid.GridType == GridType.Block)
                {
                    grid.GridType = GridType.Blank;
                }

            }

            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int c = e.X / GridSize;
            int r = e.Y / GridSize - 1;
            if (c < 0) c = 0;
            if (c > ColNum - 1) c = ColNum - 1;
            if (r < 0) r = 0;
            if (r > RowNum - 1) r = RowNum - 1;
            var grid = AllGrids[c, r];
            if (e.Button == MouseButtons.Left)
            {
                if (grid.GridType == GridType.Blank)
                {
                    grid.GridType = GridType.Block;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (grid.GridType == GridType.Block)
                {
                    grid.GridType = GridType.Blank;
                }
            }
            Invalidate();
        }


        bool startDown = false;
        bool endDown = false;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.S)
            {
                startDown = true;
                endDown = false;
            }
            if (e.KeyCode == Keys.E)
            {
                endDown = true;
                startDown = false;
            }
            if (e.KeyCode == Keys.Escape)
            {
                ThePath = null;
                foreach (var g in AllGrids)
                {
                    g.GridType = GridType.Blank;
                }
            }
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.KeyCode == Keys.S)
            {
                startDown = false;
            }
            if (e.KeyCode == Keys.E)
            {
                endDown = false;
            }
        }

        #region DFS
        private void dFSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetGrids();
            RunDFS();
        }

        void RunDFS()
        {
            List<Grid> path = new List<Grid>();
            if (DFS(startGrid, path))
            {
                ThePath = path.Select(g => new Point(g.rect.X + GridSize / 2, g.rect.Y + GridSize / 2)).ToArray();
            }
            Invalidate();

        }

        bool DFS(Grid g, List<Grid> path)
        {
            g.Searched = true;
            if (g == endGrid)
            {
                path.Add(g);
                return true;
            }
            var round = GetRound8(g);
            foreach (var r in round)
            {
                if (DFS(r, path))
                {
                    path.Add(g);
                    return true;
                }
            }
            return false;
        }

        List<Grid> GetRound8(Grid g)
        {
            List<Grid> ret = new List<Grid>();
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int x = g.X + i, y = g.Y + j;
                    if (x >= 0 && x < ColNum && y >= 0 && y < RowNum)
                    {
                        var grid = AllGrids[x, y];
                        if (grid != g && grid.GridType != GridType.Block && !grid.Searched)
                        {
                            ret.Add(grid);
                        }
                    }
                }
            }
            return ret;
        }
        #endregion

        #region BestFit
        private void bestFitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetGrids();
            RunBestFit();
        }

        //只考虑到目标位置最近的格子
        void RunBestFit()
        {
            List<Grid> path = new List<Grid>();
            List<Grid> open = new List<Grid>();
            startGrid.H = calcH3(startGrid);
            startGrid.Searched = true;
            open.Add(startGrid);
            while (open.Count > 0)
            {
                open.Sort((a, b) => a.H - b.H);
                var g = open[0];
                g.Searched = true;
                open.Remove(g);
                if (g == endGrid)
                {
                    path.Add(g);
                    while (g.Parent != null)
                    {
                        g = g.Parent;
                        path.Add(g);
                    }
                    break;
                }
                List<Grid> round = GetRound8(g);
                foreach (var r in round)
                {
                    r.H = calcH3(r);
                    r.Parent = g;
                    r.Searched = true;

                    open.Add(r);

                }
            }

            if (path.Count > 0)
            {
                ThePath = path.Select(g => new Point(g.rect.X + GridSize / 2, g.rect.Y + GridSize / 2)).ToArray();
            }
            Invalidate();
        }

        #endregion

        #region BFS
        private void bFSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetGrids();
            RunBFS();
        }

        void RunBFS()
        {
            List<Grid> path = new List<Grid>();
            Queue<Grid> queue = new Queue<Grid>();

            queue.Enqueue(startGrid);
            while (queue.Count > 0)
            {
                var g = queue.Dequeue();
                g.Searched = true;
                if (g == endGrid)
                {
                    path.Add(g);
                    while (g.Parent != null)
                    {
                        g = g.Parent;
                        path.Add(g);
                    }
                    break;
                }
                var round = GetRound8(g);
                foreach (var r in round)
                {
                    r.Parent = g;
                    r.Searched = true;
                    queue.Enqueue(r);
                }
            }
            if (path.Count > 0)
            {
                ThePath = path.Select(g => new Point(g.rect.X + GridSize / 2, g.rect.Y + GridSize / 2)).ToArray();
            }
            Invalidate();

        }
        #endregion

        #region AStar
        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetGrids();
            RunAStar();
        }


        void RunAStar()
        {
            List<Grid> path = new List<Grid>();
            List<Grid> open = new List<Grid>();//正在探索的格子
            HashSet<Grid> close = new HashSet<Grid>();//探索完的格子
            startGrid.G = 0;
            startGrid.H = calcH3(startGrid);
            startGrid.F = startGrid.G + startGrid.H;
            startGrid.Searched = true;
            open.Add(startGrid);
            while (open.Count > 0)
            {
                open.Sort((a, b) => a.F - b.F);
                var g = open[0];
                open.Remove(g);
                close.Add(g);
                if (g == endGrid)
                {
                    path.Add(g);
                    while (g.Parent != null)
                    {
                        g = g.Parent;
                        path.Add(g);
                    }
                    break;
                }
                //寻找可行走的邻接格子，不能在close集合中
                List<Grid> round = new List<Grid>();
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        int x = g.X + i, y = g.Y + j;
                        if (x >= 0 && x < ColNum && y >= 0 && y < RowNum)
                        {
                            var grid = AllGrids[x, y];
                            if (grid != g && grid.GridType != GridType.Block && !close.Contains(grid))//不在close集合
                            {
                                round.Add(grid);
                            }
                        }
                    }
                }
                foreach (var r in round)
                {
                    int value = 14;
                    if (g.X == r.X || g.Y == r.Y)
                    {
                        value = 10;
                    }
                    int G = g.G + value;
                    int H = calcH3(r);
                    int F = G + H;
                    if (r.Searched && r.F > F)//从g移动到r比原来非g移动到r更短则修改parent
                    {
                        r.G = G;
                        r.H = H;
                        r.F = F;
                        r.Parent = g;
                    }

                    if (!r.Searched)
                    {
                        r.G = G;
                        r.H = H;
                        r.F = F;
                        r.Parent = g;
                        r.Searched = true;

                        open.Add(r);
                    }
                }
            }

            if (path.Count > 0)
            {
                path.Reverse();
                ThePath = path.Select(g => new Point(g.rect.X + GridSize / 2, g.rect.Y + GridSize / 2)).ToArray();
            }
            Invalidate();
        }
        //切比雪夫距离
        int calcH1(Grid grid)
        {
            return Math.Max(Math.Abs(grid.X - endGrid.X), Math.Abs(grid.Y - endGrid.Y));
        }

        //曼哈顿距离
        int calcH2(Grid grid)
        {
            return Math.Abs(grid.X - endGrid.X) + Math.Abs(grid.Y - endGrid.Y);
        }

        //欧几里得距离
        int calcH3(Grid grid)
        {
            int dx = grid.X - endGrid.X;
            int dy = grid.Y - endGrid.Y;
            return dx * dx + dy * dy;
        }

        #endregion

        #region Djkstra
        private void djkstraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetGrids();
            RunDjkstra();
        }

        void RunDjkstra()
        {
            List<Grid> path = new List<Grid>();
            List<Grid> open = new List<Grid>();
            HashSet<Grid> close = new HashSet<Grid>();

            startGrid.Searched = true;
            open.Add(startGrid);
            while (open.Count > 0)
            {
                open.Sort((a, b) => a.G - b.G);
                var g = open[0];
                open.Remove(g);
                close.Add(g);

                if (g == endGrid)
                {
                    path.Add(g);
                    while (g.Parent != null)
                    {
                        g = g.Parent;
                        path.Add(g);
                    }
                    break;
                }


                //寻找g周围可行走的邻接格子，不能在open集合中
                List<Grid> round = new List<Grid>();
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        int x = g.X + i, y = g.Y + j;
                        if (x >= 0 && x < ColNum && y >= 0 && y < RowNum)
                        {
                            var grid = AllGrids[x, y];
                            if (grid != g && grid.GridType != GridType.Block && !close.Contains(grid))
                            {
                                round.Add(grid);
                            }
                        }
                    }
                }

                foreach (var r in round)
                {
                    int value = 14;
                    if (g.X == r.X || g.Y == r.Y)
                    {
                        value = 10;
                    }
                    int G = g.G + value;
                    if (r.Searched && r.G > G)//从g移动到r比原来非g移动到r更短则修改parent
                    {
                        r.G = G;
                        r.Parent = g;
                    }

                    if (!r.Searched)
                    {
                        r.G = G;
                        r.Parent = g;
                        r.Searched = true;

                        open.Add(r);
                    }
                }
            }

            if (path.Count > 0)
            {
                path.Reverse();
                ThePath = path.Select(g => new Point(g.rect.X + GridSize / 2, g.rect.Y + GridSize / 2)).ToArray();
            }
            Invalidate();
        }

        #endregion

        #region JPS
        private void jPSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetGrids();
            RunJPS();
        }

        void RunJPS()
        {
            PriorityQueue<Grid> mOpenQueue = new PriorityQueue<Grid>();
            HashSet<Grid> mCloseSet = new HashSet<Grid>();
            HashSet<Grid> mOpenSet = new HashSet<Grid>();
            List<Grid> path=null;

            mOpenQueue.Push(startGrid);
            while (mOpenQueue.Size > 0)
            {
                Grid current = mOpenQueue.Pop();
                mOpenSet.Remove(current);
                mCloseSet.Add(current);
                if (current == endGrid)
                {
                    path = TraceBackPath();
                    break;
                }
                var jumps = GetJumpPoints(current);//只寻找跳点
                foreach (var jumpPoint in jumps)
                {
                    if (mCloseSet.Contains(jumpPoint))
                    {
                        continue;
                    }
                    int G = current.G + CalcDistance(jumpPoint, current);
                    int H = CalcDistance(jumpPoint, endGrid);
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

            if (path != null)
            {
                ThePath = path.Select(g => new Point(g.rect.X + GridSize / 2, g.rect.Y + GridSize / 2)).ToArray();
            }
            Invalidate();
        }

        List<Grid> TraceBackPath()
        {
            List<Grid> path = new List<Grid>();

            path.Add(endGrid);
            var p = endGrid.Parent;
            while (p != startGrid)
            {
                path.Add(p);
                p = p.Parent;
            }
            path.Add(startGrid);
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
            if (x < 0 || x >= ColNum || y < 0 || y >= RowNum)
            {
                return null;
            }
            return AllGrids[x, y];
        }

        bool IsWalkable(Grid g)
        {
            if (g != null)
            {
                g.Searched = true;
                return g.GridType != GridType.Block;
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
            if (current == startGrid)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0) continue;
                        var g = GetGrid(current.X + i, current.Y + j);
                        if (IsWalkable(g))
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
            if (current == endGrid)
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

        #endregion


    }
}

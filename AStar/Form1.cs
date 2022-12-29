using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AStar
{
    public partial class Form1 : Form
    {
        public Form1()
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
        class Grid
        {
            public GridType type;
            public int Y, X;
            public Rectangle rect;
            public bool searched = false;
            public Grid parent;
            public int G, H, F;
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
                        type = GridType.Blank,
                        X = c,
                        Y = r,
                        rect = new Rectangle(c * GridSize, (1 + r) * GridSize, GridSize, GridSize)
                    };
                }
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
                    if (g.searched)
                    {
                        e.Graphics.FillRectangle(Brushes.AliceBlue, g.rect);
                    }

                    if (g.type == GridType.Block)
                    {
                        e.Graphics.FillRectangle(Brushes.Black, g.rect);
                    }
                    else if (g.type == GridType.Start)
                    {
                        e.Graphics.FillRectangle(Brushes.Red, g.rect);
                    }
                    else if (g.type == GridType.End)
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
                if (grid.type == GridType.Blank)
                {
                    grid.type = GridType.Block;

                    if (startDown)
                    {
                        if (startGrid != null)
                        {
                            startGrid.type = GridType.Blank;
                        }
                        grid.type = GridType.Start;
                        startGrid = grid;
                    }
                    else if (endDown)
                    {
                        if (endGrid != null)
                        {
                            endGrid.type = GridType.Blank;
                        }
                        grid.type = GridType.End;
                        endGrid = grid;
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (grid.type == GridType.Block)
                {
                    grid.type = GridType.Blank;
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
                if (grid.type == GridType.Blank)
                {
                    grid.type = GridType.Block;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (grid.type == GridType.Block)
                {
                    grid.type = GridType.Blank;
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
                    g.type = GridType.Blank;
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
            RunDFS();
        }

        void RunDFS()
        {
            foreach (var g in AllGrids)
            {
                g.searched = false;
            }
            List<Grid> path = new List<Grid>();
            if (Search(startGrid, path))
            {
                ThePath = path.Select(g => new Point(g.rect.X + GridSize / 2, g.rect.Y + GridSize / 2)).ToArray();
            }
            Invalidate();

        }

        bool Search(Grid g, List<Grid> path)
        {
            g.searched = true;
            if (g == endGrid)
            {
                path.Add(g);
                return true;
            }
            var round = GetRound8(g);
            foreach (var r in round)
            {
                if (Search(r, path))
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
                        if (grid != g && grid.type != GridType.Block && !grid.searched)
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
            RunBestFit();
        }

        //只考虑到目标位置最近的格子
        void RunBestFit()
        {
            foreach (var g in AllGrids)
            {
                g.searched = false;
            }
            List<Grid> path = new List<Grid>();
            List<Grid> open = new List<Grid>();
            startGrid.H = calcH2(startGrid);
            startGrid.searched = true;
            open.Add(startGrid);
            while (open.Count > 0)
            {
                open.Sort((a, b) => a.H - b.H);
                var g = open[0];
                g.searched = true;
                open.Remove(g);
                if (g == endGrid)
                {
                    path.Add(g);
                    while (g.parent != null)
                    {
                        g = g.parent;
                        path.Add(g);
                    }
                    break;
                }
                List<Grid> round = GetRound8(g);
                foreach (var r in round)
                {
                    r.H = calcH2(r);
                    r.parent = g;
                    r.searched = true;

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
            RunBFS();
        }

        void RunBFS()
        {
            foreach (var g in AllGrids)
            {
                g.searched = false;
            }

            List<Grid> path = new List<Grid>();
            Queue<Grid> queue = new Queue<Grid>();

            queue.Enqueue(startGrid);
            while (queue.Count > 0)
            {
                var g = queue.Dequeue();
                g.searched = true;
                if (g == endGrid)
                {
                    path.Add(g);
                    while (g.parent != null)
                    {
                        g = g.parent;
                        path.Add(g);
                    }
                    break;
                }
                var round = GetRound8(g);
                foreach (var r in round)
                {
                    r.parent = g;
                    r.searched = true;
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
            RunAStar();
        }


        void RunAStar()
        {
            foreach (var g in AllGrids)
            {
                g.searched = false;
            }
            List<Grid> path = new List<Grid>();
            List<Grid> open = new List<Grid>();
            startGrid.G = 0;
            startGrid.H = calcH2(startGrid);
            startGrid.F = startGrid.G + startGrid.H;
            startGrid.searched = true;
            open.Add(startGrid);
            while (open.Count > 0)
            {
                open.Sort((a, b) => a.F - b.F);
                var g = open[0];
                open.Remove(g);
                if (g == endGrid)
                {
                    path.Add(g);
                    while (g.parent != null)
                    {
                        g = g.parent;
                        path.Add(g);
                    }
                    break;
                }
                List<Grid> round = new List<Grid>();
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        int x = g.X + i, y = g.Y + j;
                        if (x >= 0 && x < ColNum && y >= 0 && y < RowNum)
                        {
                            var grid = AllGrids[x, y];
                            if (grid != g && grid.type != GridType.Block)
                            {
                                round.Add(grid);
                            }
                        }
                    }
                }
                foreach (var r in round)
                {
                    int G = g.G + 1;
                    int H = calcH2(r);
                    int F = G + H;
                    if (r.searched && r.F > F)
                    {
                        r.G = G;
                        r.H = H;
                        r.F = F;
                        r.parent = g;
                    }

                    if (!r.searched)
                    {
                        r.G = G;
                        r.H = H;
                        r.F = F;
                        r.parent = g;
                        r.searched = true;

                        open.Add(r);
                    }
                }
            }

            if (path.Count > 0)
            {
                ThePath = path.Select(g => new Point(g.rect.X + GridSize / 2, g.rect.Y + GridSize / 2)).ToArray();
            }
            Invalidate();
        }
        //切比雪夫距离
        int calcH(Grid grid)
        {
            return Math.Max(Math.Abs(grid.X - endGrid.X), Math.Abs(grid.Y - endGrid.Y));
        }
        
        //曼哈顿距离
        int calcH3(Grid grid)
        {
            return Math.Abs(grid.X - endGrid.X) + Math.Abs(grid.Y - endGrid.Y);
        }

        //欧几里得距离
        int calcH2(Grid grid)
        {
            int dx = grid.X - endGrid.X;
            int dy = grid.Y - endGrid.Y;
            return dx * dx + dy * dy;
        }

        #endregion

        #region Djkstra
        private void djkstraToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region JPS
        private void jPSToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        #endregion


    }
}

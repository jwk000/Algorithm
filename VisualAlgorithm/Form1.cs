using Algorithm;
using System.Numerics;

namespace VisualAlgorithm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
        }

        Action<Graphics> mDrawAction = null;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (mDrawAction != null)
            {
                mDrawAction.Invoke(e.Graphics);
            }
        }

        private void michelSampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sample sample = new Sample();
            var ret = sample.MichellSample(ClientRectangle.Width, ClientRectangle.Height, 1000);

            mDrawAction = g =>
            {
                foreach(var p in ret)
                {
                    g.FillRectangle(Brushes.Black, new Rectangle(p, new Size(2, 2)));
                }
            };

            Invalidate();
        }

        private void randomSampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sample sample = new Sample();
            var ret = sample.RandomSample(ClientRectangle.Width, ClientRectangle.Height, 1000);

            mDrawAction = g =>
            {
                foreach (var p in ret)
                {
                    g.FillRectangle(Brushes.Black, new Rectangle(p, new Size(2, 2)));
                }
            };

            Invalidate();

        }

        private void poissonSampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sample sample = new Sample();
            var ret = sample.PoissonDiscSample(ClientRectangle.Width, ClientRectangle.Height, 1000);

            mDrawAction = g =>
            {
                foreach (var p in ret)
                {
                    g.FillRectangle(Brushes.Black, new Rectangle(p, new Size(2, 2)));
                }
            };

            Invalidate();
        }

        private void kdtreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Vector2> input = new List<Vector2>();
            input.Add(new Vector2(3.5f, 4.5f));
            input.Add(new Vector2(1.5f, 6.5f));
            input.Add(new Vector2(2f, 4f));
            input.Add(new Vector2(5f, 3f));
            input.Add(new Vector2(3f, 2f));
            input.Add(new Vector2(6f, 1f));
            input.Add(new Vector2(1f, 2.2f));
            input.Add(new Vector2(7f, 4.5f));

            KDTree kdtree = new KDTree();
            kdtree.Build(input);

            //×ø±ê·Å´ó100±¶
            mDrawAction = g=> drawKDNode(g, kdtree.Root, 10, 800, 10, 700);
            Invalidate();
        }

        void drawKDNode(Graphics g, KDTree.KDNode node,float left,float right,float top,float bottom)
        {
            if (node == null) return;
            PointF point = new PointF(node.data.X * 100, node.data.Y * 100);
            if (node.split == 0)
            {
                g.DrawLine(Pens.Black, point.X, top, point.X, bottom);
                drawKDNode(g, node.left, left, point.X, top, bottom);
                drawKDNode(g, node.right, point.X, right, top, bottom);
            }
            else
            {
                g.DrawLine(Pens.Black, left, point.Y, right, point.Y);
                drawKDNode(g, node.left, left, right, top, point.Y);
                drawKDNode(g, node.right, left, right, point.Y, bottom);
            }
            g.FillEllipse(Brushes.Brown, point.X - 2, point.Y - 2, 4, 4);
            g.DrawString(node.data.ToString(), SystemFonts.DefaultFont, Brushes.Brown, point.X + 10, point.Y - 2);
        }


        PathFinder mPathFinderForm = new PathFinder();
        private void pathfinderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mPathFinderForm.Show();
        }
    }
}
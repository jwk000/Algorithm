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
            this.timer1.Tick += OnTick;
            this.timer1.Interval = 100;
            this.timer1.Start();
        }
        Action mTickAction = null;
        Action<Graphics> mDrawAction = null;

        private void OnTick(object? sender, EventArgs e)
        {
            if (mTickAction != null)
            {
                mTickAction();
            }
        }
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
                foreach (var p in ret)
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
            List<System.Numerics.Vector2> input = new List<System.Numerics.Vector2>();
            input.Add(new System.Numerics.Vector2(3.5f, 4.5f));
            input.Add(new System.Numerics.Vector2(1.5f, 6.5f));
            input.Add(new System.Numerics.Vector2(2f, 4f));
            input.Add(new System.Numerics.Vector2(5f, 3f));
            input.Add(new System.Numerics.Vector2(3f, 2f));
            input.Add(new System.Numerics.Vector2(6f, 1f));
            input.Add(new System.Numerics.Vector2(1f, 2.2f));
            input.Add(new System.Numerics.Vector2(7f, 4.5f));

            KDTree kdtree = new KDTree();
            kdtree.Build(input);

            //����Ŵ�100��
            mDrawAction = g => drawKDNode(g, kdtree.Root, 10, 800, 10, 700);
            Invalidate();
        }

        void drawKDNode(Graphics g, KDTree.KDNode node, float left, float right, float top, float bottom)
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

        private void quadtreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Width = 800;
            int Height = 800;
            int ObjMinSize = 10;
            int ObjMaxSize = 30;
            this.ClientSize = new Size(Width, Height);
            
            QuadTree qdtree = new QuadTree(5, 4, 0, Width, 0, Height);
            for (int i = 0; i < 80; i++)
            {
                int x = randor.Next(Width-ObjMaxSize);
                int y = randor.Next(Height-ObjMaxSize);
                int w = randor.Next(ObjMinSize, ObjMaxSize);
                int h = randor.Next(ObjMinSize, ObjMaxSize);
                QuadTree.QTObject obj = new QuadTree.QTObject(x, y, w, h);

                qdtree.AddObject(obj);
            }

            mDrawAction = g => drawQuadTree(g, qdtree);
            mTickAction = () => onTickQuadTree(qdtree,qdtree.AllObjects[1]);

            Invalidate();
        }
        MyRandom randor = new MyRandom();

        void onTickQuadTree(QuadTree tree, QuadTree.QTObject obj)
        {
            int x = randor.Next(0, 30);
            int y = randor.Next(0, 10);
            obj.X += x;
            obj.Y += y;
            if (obj.X+obj.W < 0)
            {
                obj.X = 800;
            }
            if (obj.X > 800)
            {
                obj.X = 0;
            }
            if (obj.Y + obj.H < 0)
            {
                obj.Y = 800;
            }
            if (obj.Y > 800)
            {
                obj.Y = 0;
            }
            tree.UpdateObject(obj);
            Invalidate();
        }

        void drawQuadTree(Graphics g, QuadTree tree)
        {
            foreach (var obj in tree.AllObjects.Values)
            {
                g.DrawRectangle(Pens.Black, obj.X, obj.Y, obj.W, obj.H);
            }
            var me = tree.AllObjects[1];
            g.FillRectangle(Brushes.Green, me.X, me.Y, me.W, me.H);
            drawQTNode(g,tree.Root);
            foreach(var o in tree.GetIntreastObjects(me))
            {
                g.FillRectangle(Brushes.Red, o.X, o.Y, o.W, o.H);
            }
        }

        Pen[] QuadTreePens = new Pen[5]
        {
            Pens.DarkGreen,Pens.DodgerBlue,Pens.Orange,Pens.Brown,Pens.Cyan
        };
        void drawQTNode(Graphics g, QuadTree.QTNode node)
        {
            int width = node.Right - node.Left;
            int height = node.Down - node.Up;
            Pen p = QuadTreePens[node.level];
            g.DrawRectangle(p, node.Left, node.Up, width-1, height-1);
            if (node.SubNodes != null)
            {
                foreach (var n in node.SubNodes)
                {
                    drawQTNode(g, n);
                }
            }
            else
            {
                g.DrawString(node.Objects.Count.ToString(), SystemFonts.DefaultFont, Brushes.Red, node.Left + width / 2, node.Up + height / 2);

            }

        }
    }
}
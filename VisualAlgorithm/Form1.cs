using Algorithm;
using System.Data.Common;

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
            this.timer1.Interval = 30;
            this.timer1.Start();
        }
        Action mOnTick;
        Action<Graphics> mOnPaint;
        Action<MouseEventArgs> mOnMouseClick;
        Action<MouseEventArgs> mOnMouseMove;
        Action<MouseEventArgs> mOnMouseUp;
        Action<MouseEventArgs> mOnMouseDown;
        Action<MouseEventArgs> mOnMouseWheel;

        private void OnTick(object? sender, EventArgs e)
        {
            if (mOnTick != null)
            {
                mOnTick();
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (mOnPaint != null)
            {
                mOnPaint.Invoke(e.Graphics);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (mOnMouseClick != null)
            {
                mOnMouseClick(e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (mOnMouseMove != null)
            {
                mOnMouseMove(e);
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (mOnMouseUp != null)
            {
                mOnMouseUp.Invoke(e);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (mOnMouseDown != null)
            {
                mOnMouseDown.Invoke(e);
            }
        }

        private void michelSampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sample sample = new Sample();
            var ret = sample.MichellSample(ClientRectangle.Width, ClientRectangle.Height, 1000);

            mOnPaint = g =>
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

            mOnPaint = g =>
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

            mOnPaint = g =>
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
            input.Add(new Vector2(3.5f, 5f));
            input.Add(new Vector2(1.5f, 6.5f));
            input.Add(new Vector2(2f, 4f));
            input.Add(new Vector2(5f, 3f));
            input.Add(new Vector2(3f, 2f));
            input.Add(new Vector2(6f, 1f));
            input.Add(new Vector2(1f, 2.2f));
            input.Add(new Vector2(7f, 4.5f));

            KDTree kdtree = new KDTree();
            kdtree.Build(input);

            //坐标放大100倍
            mOnPaint = g => drawKDNode(g, kdtree.Root, 10, 800, 10, 700);
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
                int x = randor.Next(Width - ObjMaxSize);
                int y = randor.Next(Height - ObjMaxSize);
                int w = randor.Next(ObjMinSize, ObjMaxSize);
                int h = randor.Next(ObjMinSize, ObjMaxSize);
                QuadTree.QTObject obj = new QuadTree.QTObject(x, y, w, h);

                qdtree.AddObject(obj);
            }

            mOnPaint = g => drawQuadTree(g, qdtree);
            mOnTick = () => onTickQuadTree(qdtree, qdtree.AllObjects[1]);

            Invalidate();
        }
        MyRandom randor = new MyRandom();

        void onTickQuadTree(QuadTree tree, QuadTree.QTObject obj)
        {
            int x = randor.Next(0, 30);
            int y = randor.Next(0, 10);
            obj.X += x;
            obj.Y += y;
            if (obj.X + obj.W < 0)
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
            drawQTNode(g, tree.Root);
            foreach (var o in tree.GetIntreastObjects(me))
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
            g.DrawRectangle(p, node.Left, node.Up, width - 1, height - 1);
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

        //维诺图
        private void voronoiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //随机30个点R，和30种颜色C，遍历所有像素P，找到P[i]距离R最近的点R[j]，取C[j]作为P[i]的颜色
            int N = 30;
            int W = 1024;
            int H = 768;
            ClientSize = new Size(W, H);
            Point2[] R = new Point2[N];
            Color[] C = new Color[N];
            for (int i = 0; i < N; i++)
            {
                R[i] = new Point2(randor.Next(W), randor.Next(H));
                C[i] = Color.FromArgb(randor.Next(255), randor.Next(255), randor.Next(255));
            }
            Bitmap bmp = new Bitmap(W, H);

            mOnPaint = g => g.DrawImage(bmp, 0, 0);
            mOnTick = () =>
            {
                for (int i = 0; i < N; i++)
                {
                    R[i].X += randor.Next(10) - 5;
                    R[i].Y += randor.Next(10) - 5;
                }

                for (int x = 0; x < W; x++)
                {
                    for (int y = 0; y < H; y++)
                    {
                        float minDist = W * H;
                        Color color = Color.AliceBlue;
                        for (int i = 0; i < N; i++)
                        {
                            Point2 p = R[i];
                            float d = (p.X - x) * (p.X - x) + (p.Y - y) * (p.Y - y);
                            if (d < minDist)
                            {
                                minDist = d;
                                color = C[i];
                                //int cc = (int)minDist % 255;
                                //int cc = (int)(R[i].X + R[i].Y) % 255;
                                //color = Color.FromArgb(10, 100, cc);
                            }
                        }

                        bmp.SetPixel(x, y, color);
                    }
                }

                Invalidate();
            };

        }

        //贝塞尔曲线
        List<Point> mBezierCtrlPts = new List<Point>();
        private void bezierCurveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mBezierCtrlPts.Clear();
            mOnMouseClick = e =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    mBezierCtrlPts.Add(e.Location);
                    if (mBezierCtrlPts.Count > 1)
                    {
                        mOnPaint = g => g.DrawLines(Pens.Black, mBezierCtrlPts.ToArray());
                        Invalidate();
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    mOnMouseMove = null;
                    BezierCurve bc = new BezierCurve();
                    mOnPaint = g =>
                    {
                        g.DrawLines(Pens.Black, mBezierCtrlPts.ToArray());

                        List<Vector2> ctrls = mBezierCtrlPts.Select(p => new Vector2(p.X, p.Y)).ToList();
                        List<PointF> points = bc.DrawBezier(ctrls).Select(v => new PointF(v.X, v.Y)).ToList();
                        g.DrawLines(Pens.IndianRed, points.ToArray());

                        //g.DrawBeziers(Pens.Green, mBezierCtrlPts.ToArray());
                    };
                    Invalidate();
                }
            };

            mOnMouseMove = e =>
            {
                List<Point> pts = new List<Point>();
                if (mBezierCtrlPts.Count > 0)
                {
                    pts.AddRange(mBezierCtrlPts);
                    pts.Add(e.Location);
                    mOnPaint = g => g.DrawLines(Pens.Black, pts.ToArray());
                    Invalidate();
                }
            };

        }

        //绳子模拟
        int mDragState = 0;//0未拖拽 1拖拽中 

        private void ropeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MassSpringSystem rope = new MassSpringSystem();
            rope.Init();

            mOnPaint = g =>
            {
                List<PointF> pts = rope.GetPoints().Select(p => new PointF(p.X, p.Y)).ToList();
                foreach (PointF p in pts)
                {
                    g.DrawEllipse(Pens.Red, new RectangleF(p, new Size(5, 5)));
                }
                g.DrawLines(Pens.Black, pts.ToArray());
            };

            mOnTick = () =>
            {
                rope.Update(0.1f);
                Invalidate();
            };

            mOnMouseDown = e =>
            {
                MassPoint mp = rope.massPoints[0];
                Vector2 ep = new Vector2(e.X, e.Y);
                Vector2 me = ep - mp.position;
                if (me.Length() < 10)
                {
                    mDragState = 1;
                }
            };

            mOnMouseUp = e =>
            {
                if (mDragState == 1) { mDragState = 0; }
            };

            mOnMouseMove = e =>
            {
                if (mDragState == 1)
                {
                    rope.massPoints[0].position = new Vector2(e.X, e.Y);
                    rope.Update(0.1f);
                }
            };
        }
    }
}
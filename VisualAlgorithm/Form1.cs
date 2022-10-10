using Algorithm;

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

        Action<Graphics> mDrawAction;
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
    }
}
namespace VisualAlgorithm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            menuStrip1 = new MenuStrip();
            sampleToolStripMenuItem = new ToolStripMenuItem();
            randomSampleToolStripMenuItem = new ToolStripMenuItem();
            michelSampleToolStripMenuItem = new ToolStripMenuItem();
            poissonSampleToolStripMenuItem = new ToolStripMenuItem();
            treeToolStripMenuItem = new ToolStripMenuItem();
            kdtreeToolStripMenuItem = new ToolStripMenuItem();
            quadtreeToolStripMenuItem = new ToolStripMenuItem();
            graphToolStripMenuItem = new ToolStripMenuItem();
            voronoiToolStripMenuItem = new ToolStripMenuItem();
            pathfinderToolStripMenuItem = new ToolStripMenuItem();
            timer1 = new System.Windows.Forms.Timer(components);
            bezierToolStripMenuItem = new ToolStripMenuItem();
            bezierCurveToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { sampleToolStripMenuItem, treeToolStripMenuItem, graphToolStripMenuItem, pathfinderToolStripMenuItem, bezierToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(622, 25);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // sampleToolStripMenuItem
            // 
            sampleToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { randomSampleToolStripMenuItem, michelSampleToolStripMenuItem, poissonSampleToolStripMenuItem });
            sampleToolStripMenuItem.Name = "sampleToolStripMenuItem";
            sampleToolStripMenuItem.Size = new Size(62, 21);
            sampleToolStripMenuItem.Text = "sample";
            // 
            // randomSampleToolStripMenuItem
            // 
            randomSampleToolStripMenuItem.Name = "randomSampleToolStripMenuItem";
            randomSampleToolStripMenuItem.Size = new Size(180, 22);
            randomSampleToolStripMenuItem.Text = "random sample";
            randomSampleToolStripMenuItem.Click += randomSampleToolStripMenuItem_Click;
            // 
            // michelSampleToolStripMenuItem
            // 
            michelSampleToolStripMenuItem.Name = "michelSampleToolStripMenuItem";
            michelSampleToolStripMenuItem.Size = new Size(180, 22);
            michelSampleToolStripMenuItem.Text = "michel sample";
            michelSampleToolStripMenuItem.Click += michelSampleToolStripMenuItem_Click;
            // 
            // poissonSampleToolStripMenuItem
            // 
            poissonSampleToolStripMenuItem.Name = "poissonSampleToolStripMenuItem";
            poissonSampleToolStripMenuItem.Size = new Size(180, 22);
            poissonSampleToolStripMenuItem.Text = "poisson sample";
            poissonSampleToolStripMenuItem.Click += poissonSampleToolStripMenuItem_Click;
            // 
            // treeToolStripMenuItem
            // 
            treeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { kdtreeToolStripMenuItem, quadtreeToolStripMenuItem });
            treeToolStripMenuItem.Name = "treeToolStripMenuItem";
            treeToolStripMenuItem.Size = new Size(43, 21);
            treeToolStripMenuItem.Text = "tree";
            // 
            // kdtreeToolStripMenuItem
            // 
            kdtreeToolStripMenuItem.Name = "kdtreeToolStripMenuItem";
            kdtreeToolStripMenuItem.Size = new Size(129, 22);
            kdtreeToolStripMenuItem.Text = "kdtree";
            kdtreeToolStripMenuItem.Click += kdtreeToolStripMenuItem_Click;
            // 
            // quadtreeToolStripMenuItem
            // 
            quadtreeToolStripMenuItem.Name = "quadtreeToolStripMenuItem";
            quadtreeToolStripMenuItem.Size = new Size(129, 22);
            quadtreeToolStripMenuItem.Text = "quadtree";
            quadtreeToolStripMenuItem.Click += quadtreeToolStripMenuItem_Click;
            // 
            // graphToolStripMenuItem
            // 
            graphToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { voronoiToolStripMenuItem });
            graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            graphToolStripMenuItem.Size = new Size(55, 21);
            graphToolStripMenuItem.Text = "graph";
            // 
            // voronoiToolStripMenuItem
            // 
            voronoiToolStripMenuItem.Name = "voronoiToolStripMenuItem";
            voronoiToolStripMenuItem.Size = new Size(180, 22);
            voronoiToolStripMenuItem.Text = "voronoi";
            voronoiToolStripMenuItem.Click += voronoiToolStripMenuItem_Click;
            // 
            // pathfinderToolStripMenuItem
            // 
            pathfinderToolStripMenuItem.Name = "pathfinderToolStripMenuItem";
            pathfinderToolStripMenuItem.Size = new Size(80, 21);
            pathfinderToolStripMenuItem.Text = "pathfinder";
            pathfinderToolStripMenuItem.Click += pathfinderToolStripMenuItem_Click;
            // 
            // bezierToolStripMenuItem
            // 
            bezierToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { bezierCurveToolStripMenuItem });
            bezierToolStripMenuItem.Name = "bezierToolStripMenuItem";
            bezierToolStripMenuItem.Size = new Size(56, 21);
            bezierToolStripMenuItem.Text = "bezier";
            // 
            // bezierCurveToolStripMenuItem
            // 
            bezierCurveToolStripMenuItem.Name = "bezierCurveToolStripMenuItem";
            bezierCurveToolStripMenuItem.Size = new Size(180, 22);
            bezierCurveToolStripMenuItem.Text = "bezier curve";
            bezierCurveToolStripMenuItem.Click += bezierCurveToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(622, 382);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2, 3, 2, 3);
            Name = "Form1";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem sampleToolStripMenuItem;
        private ToolStripMenuItem michelSampleToolStripMenuItem;
        private ToolStripMenuItem poissonSampleToolStripMenuItem;
        private ToolStripMenuItem randomSampleToolStripMenuItem;
        private ToolStripMenuItem treeToolStripMenuItem;
        private ToolStripMenuItem kdtreeToolStripMenuItem;
        private ToolStripMenuItem graphToolStripMenuItem;
        private ToolStripMenuItem pathfinderToolStripMenuItem;
        private ToolStripMenuItem quadtreeToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private ToolStripMenuItem voronoiToolStripMenuItem;
        private ToolStripMenuItem bezierToolStripMenuItem;
        private ToolStripMenuItem bezierCurveToolStripMenuItem;
    }
}
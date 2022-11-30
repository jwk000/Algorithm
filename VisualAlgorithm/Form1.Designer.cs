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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomSampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.michelSampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.poissonSampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kdtreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sampleToolStripMenuItem,
            this.treeToolStripMenuItem,
            this.graphToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sampleToolStripMenuItem
            // 
            this.sampleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.randomSampleToolStripMenuItem,
            this.michelSampleToolStripMenuItem,
            this.poissonSampleToolStripMenuItem});
            this.sampleToolStripMenuItem.Name = "sampleToolStripMenuItem";
            this.sampleToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
            this.sampleToolStripMenuItem.Text = "sample";
            // 
            // randomSampleToolStripMenuItem
            // 
            this.randomSampleToolStripMenuItem.Name = "randomSampleToolStripMenuItem";
            this.randomSampleToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.randomSampleToolStripMenuItem.Text = "random sample";
            this.randomSampleToolStripMenuItem.Click += new System.EventHandler(this.randomSampleToolStripMenuItem_Click);
            // 
            // michelSampleToolStripMenuItem
            // 
            this.michelSampleToolStripMenuItem.Name = "michelSampleToolStripMenuItem";
            this.michelSampleToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.michelSampleToolStripMenuItem.Text = "michel sample";
            this.michelSampleToolStripMenuItem.Click += new System.EventHandler(this.michelSampleToolStripMenuItem_Click);
            // 
            // poissonSampleToolStripMenuItem
            // 
            this.poissonSampleToolStripMenuItem.Name = "poissonSampleToolStripMenuItem";
            this.poissonSampleToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.poissonSampleToolStripMenuItem.Text = "poisson sample";
            this.poissonSampleToolStripMenuItem.Click += new System.EventHandler(this.poissonSampleToolStripMenuItem_Click);
            // 
            // treeToolStripMenuItem
            // 
            this.treeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kdtreeToolStripMenuItem});
            this.treeToolStripMenuItem.Name = "treeToolStripMenuItem";
            this.treeToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.treeToolStripMenuItem.Text = "tree";
            // 
            // kdtreeToolStripMenuItem
            // 
            this.kdtreeToolStripMenuItem.Name = "kdtreeToolStripMenuItem";
            this.kdtreeToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.kdtreeToolStripMenuItem.Text = "kdtree";
            this.kdtreeToolStripMenuItem.Click += new System.EventHandler(this.kdtreeToolStripMenuItem_Click);
            // 
            // graphToolStripMenuItem
            // 
            this.graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            this.graphToolStripMenuItem.Size = new System.Drawing.Size(66, 24);
            this.graphToolStripMenuItem.Text = "graph";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}
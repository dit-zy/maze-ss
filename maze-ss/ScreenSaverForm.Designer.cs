namespace maze_ss
{
    partial class ScreenSaverForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mazeView = new maze_ss.MazeView();
            ((System.ComponentModel.ISupportInitialize)(this.mazeView)).BeginInit();
            this.SuspendLayout();
            // 
            // mazeView
            // 
            this.mazeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mazeView.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.mazeView.Location = new System.Drawing.Point(0, 0);
            this.mazeView.Margin = new System.Windows.Forms.Padding(0);
            this.mazeView.Name = "mazeView";
            this.mazeView.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            this.mazeView.Size = new System.Drawing.Size(636, 552);
            this.mazeView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.mazeView.TabIndex = 0;
            this.mazeView.TabStop = false;
            this.mazeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ScreenSaverForm_KeyDown);
            this.mazeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ScreenSaverForm_MouseClick);
            this.mazeView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ScreenSaverForm_MouseMove);
            // 
            // ScreenSaverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(636, 552);
            this.Controls.Add(this.mazeView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ScreenSaverForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "maze-ss";
            this.Load += new System.EventHandler(this.ScreenSaverForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ScreenSaverForm_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ScreenSaverForm_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ScreenSaverForm_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.mazeView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private MazeView mazeView;
    }
}


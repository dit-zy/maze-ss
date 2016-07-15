using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace maze_ss
{
    public partial class PictureBoxWithInterpolationMode : System.Windows.Forms.PictureBox
    {
        public System.Drawing.Drawing2D.InterpolationMode InterpolationMode { get; set; }
        public System.Drawing.Drawing2D.PixelOffsetMode PixelOffsetMode { get; set; }

        public PictureBoxWithInterpolationMode()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs paintEventArgs)
        {
            paintEventArgs.Graphics.InterpolationMode = InterpolationMode;
            paintEventArgs.Graphics.PixelOffsetMode = PixelOffsetMode;
            base.OnPaint(paintEventArgs);
        }
    }
}
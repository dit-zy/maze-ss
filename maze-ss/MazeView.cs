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
    public partial class MazeView : PictureBox
    {
        public System.Drawing.Drawing2D.InterpolationMode InterpolationMode { get; set; }
        public System.Drawing.Drawing2D.PixelOffsetMode PixelOffsetMode { get; set; }

        public MazeView()
        {
            InitializeComponent();
        }

        public void addMazeGen(MazeGen maze_gen)
        {
            maze_gen.AddCell += new MazeGen.AddCellHandler(addCell);
        }

        public void addCell(Object sender, AddCellEventArgs e)
        {
            
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.InterpolationMode = InterpolationMode;
            pe.Graphics.PixelOffsetMode = PixelOffsetMode;
            base.OnPaint(pe);
        }
    }
}

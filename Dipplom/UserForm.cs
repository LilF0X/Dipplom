using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dipplom
{

    public partial class UserForm : Form
    {

        public UserForm()
        {
            InitializeComponent();
        }

        Image ZoomPicture(Image img, Size size)
        {
            Bitmap bm = new Bitmap(img, Convert.ToInt32(img.Width*size.Width), Convert.ToInt32(img.Height*size.Height));

            Graphics g = Graphics.FromImage(bm);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bm;
        }

        PictureBox org;

        private void UserForm_Load(object sender, EventArgs e)
        {
            trackBar1.Minimum = 1;
            trackBar1.Maximum = 3;
            trackBar1.LargeChange= 1;
            trackBar1.SmallChange= 1;

            this.DoubleBuffered= true;
            org = new PictureBox();
            org.Image = pictureBox1.Image;

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if(trackBar1.Value != 0)
            {
                pictureBox1.Image = null;
                pictureBox1.Image = ZoomPicture(org.Image, new Size(trackBar1.Value, trackBar1.Value));
            }
        }
    }
}

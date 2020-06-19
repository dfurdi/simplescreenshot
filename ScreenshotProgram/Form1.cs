using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ScreenshotProgram
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void captureToolStripMenuItem1_Click(object sender, EventArgs e)//capture primary
        {
            this.Hide();
            timer1.Start();
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)//capture all
        {
            this.Hide();
            timer2.Start();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)//clear picturebox
        {
            pictureBox1.Image = null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            pictureBox1.Image = bitmap;

            //resize form to screenshot size
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2);

            this.Show();
            timer1.Stop();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            // Determine the size of the "virtual screen", which includes all monitors.
            int screenLeft = SystemInformation.VirtualScreen.Left;
            int screenTop = SystemInformation.VirtualScreen.Top;
            int screenWidth = SystemInformation.VirtualScreen.Width;
            int screenHeight = SystemInformation.VirtualScreen.Height;

            // Create a bitmap of the appropriate size to receive the screenshot.
            using (Bitmap bmp = new Bitmap(screenWidth, screenHeight))
            {
                // Draw the screenshot into our bitmap.
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(screenLeft, screenTop, 0, 0, bmp.Size);
                }

                // Do something with the Bitmap here, like save it to a file:
                if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
                pictureBox1.Image = bmp.Clone(
                    new Rectangle(0, 0, bmp.Width, bmp.Height),
                    System.Drawing.Imaging.PixelFormat.DontCare);
            }

            //resize form to screenshot size
            this.Size = new Size(screenWidth / 2, screenHeight / 2);

            this.Show();
            timer2.Stop();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|Png Image|*.png";
                dialog.FileName = "Screenshot.png";
                dialog.Title = "Save Image";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image.Save(dialog.FileName, ImageFormat.Png);
                }
            }
            else
            {
                MessageBox.Show("No image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Delay
        private void noDelayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Interval = 180;
            timer2.Interval = 180;
        }

        private void secondToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer2.Interval = 1000;
        }

        private void secondsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Interval = 2000;
            timer2.Interval = 2000;
        }

        private void secondsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            timer1.Interval = 3000;
            timer2.Interval = 3000;
        }

        private void secondsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            timer1.Interval = 4000;
            timer2.Interval = 4000;
        }

        private void secondsToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            timer1.Interval = 5000;
            timer2.Interval = 5000;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace Deneme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public Bitmap TakeScreenShotFromPrimaryScreen()
        {
            Bitmap Screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                           Screen.PrimaryScreen.Bounds.Height);

            Graphics GFX = Graphics.FromImage(Screenshot);

            GFX.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                               Screen.PrimaryScreen.Bounds.Y,
                               0,
                               0,
                               Screen.PrimaryScreen.Bounds.Size
                               );
            return Screenshot;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Bitmap ss = TakeScreenShotFromPrimaryScreen();

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        void SaveImage(Bitmap bitmap)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Images|*.png;*.bmp;*.jpg";
            ImageFormat format = ImageFormat.Png;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(sfd.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }
                bitmap.Save(sfd.FileName, format);
            }
        }
    }
}

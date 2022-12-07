using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Ardiente_DIP_Activity
{
    public partial class SubtractionForm : Form
    {
        Bitmap image, background, processed;
        bool imageEnable = false;
        public SubtractionForm()
        {
            InitializeComponent();
        }

        private void openBackground_FileOk(object sender, CancelEventArgs e)
        {
            background = new Bitmap(openBackground.FileName);
            pictureBox2.Image = background;

            if (imageEnable)
                button3.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openBackground.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ImageProcessLib.Subtract(ref image, ref background, ref processed);
            pictureBox3.Image = processed;

            saveToolStripMenuItem.Enabled = true;
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox3.Image.Save(saveFileDialog.FileName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openImage.ShowDialog();
        }

        private void openImage_FileOk(object sender, CancelEventArgs e)
        {
            image = new Bitmap(openImage.FileName);
            pictureBox1.Image = image;

            imageEnable = true;
            button2.Enabled = true;
        }


        
        

    }
}

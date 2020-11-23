using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _01_FastRGB_Processing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        FastPixel f = new FastPixel();
        
        // Open Image File
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(openFileDialog1.FileName);
                f.bmp2RGB(bmp);
                pictureBox1.Image = bmp;
            }
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = f.grayImg(f.redArr);
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = f.grayImg(f.greenArr);
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = f.grayImg(f.blueArr);
        }

        private void rGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[,] rgbImg = new byte[f.imgX, f.imgY];
            for (int j = 0; j < f.imgY; j++)
            {
                for (int i = 0; i < f.imgX; i++)
                {
                    byte gray = (byte)((byte)(f.redArr[i, j] * 0.299 + f.greenArr[i, j] * 0.587) + f.blueArr[i, j] * 0.114);
                    rgbImg[i, j] = gray;
                }
            }
            pictureBox1.Image = f.grayImg(rgbImg);
        }

        private void rGLowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[,] rgLowImg = new byte[f.imgX, f.imgY];
            for (int j = 0; j < f.imgY; j++)
            {
                for (int i = 0; i < f.imgX; i++)
                {
                    if (f.redArr[i, j] > f.greenArr[i, j])
                    {
                        rgLowImg[i, j] = f.greenArr[i, j];
                    }
                    else
                    {
                        rgLowImg[i, j] = f.redArr[i, j];
                    }
                }
            }
            pictureBox1.Image = f.grayImg(rgLowImg);
        }

        private void binaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[,] binarizationImg = new byte[f.imgX, f.imgY];
            for (int j = 0; j < f.imgY; j++)
            {
                for (int i = 0; i < f.imgX; i++)
                {
                    if (f.greenArr[i, j] < 128)
                    {
                        binarizationImg[i, j] = 1;
                    }
                }
                pictureBox1.Image = f.binaryImg(binarizationImg);
            }
        }

        private void negativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[,] negativeImg = new byte[f.imgX, f.imgY];
            for (int j = 0; j < f.imgY; j++)
            {
                for (int i = 0; i < f.imgX; i++)
                {
                    if (f.greenArr[i, j] < 128)
                    {
                        negativeImg[i, j] = (byte)(255 - f.greenArr[i, j]);
                    }
                }
                pictureBox1.Image = f.grayImg(negativeImg);
            }
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog1.FileName);
            }
        }
    }
}

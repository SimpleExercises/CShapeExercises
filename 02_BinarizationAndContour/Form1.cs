using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _02_BinarizationAndContour
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        FastPixel f = new FastPixel();

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(openFileDialog1.FileName);
                f.bmp2RGB(bmp);
                pictureBox1.Image = bmp;
            }
        }

        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = f.grayImg(f.greenArr);
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog1.FileName);
            }
        }

        // Average indensity block 
        int Gdim = 40; // Divise indensity size 40*40 block
        int[,] Th; // Average indentsity of each block (threshold)
        private void averageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int blockX = f.imgX / Gdim, blockY = f.imgY / Gdim;
            Th = new int[blockX, blockY]; // block array           
            //accumulation intensity of each block
            for (int i = 0; i < f.imgX; i++)
            {
                int x = i / Gdim;
                for (int j = 0; j < f.imgY; j++)
                {
                    int y = j / Gdim;                  
                    Th[x, y] += f.greenArr[i, j];
                }
            }
            // Create intensity of each block
            byte[,] A = new byte[f.imgX, f.imgY];
            for (int i = 0; i < blockX; i++)
            {
                for (int j = 0; j < blockY; j++)
                {
                    Th[i, j] /= Gdim * Gdim;
                    for (int ii = 0; ii < Gdim; ii++)
                    {
                        for (int jj = 0; jj < Gdim; jj++)
                        {
                            A[i * Gdim + ii, j * Gdim + jj] = (byte)Th[i, j];
                        }
                    }
                }
            }
            pictureBox1.Image = f.grayImg(A);
        }
    }
}

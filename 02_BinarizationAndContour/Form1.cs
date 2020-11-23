﻿using System;
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
    }
}

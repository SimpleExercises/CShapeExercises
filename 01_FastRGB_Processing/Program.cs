using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace _01_FastRGB_Processing
{
    class FastPixel
    {
        public int imgX, imgY; // Image width and height
        public byte[,] redArr, greenArr, blueArr; // Create R,G,B  dim(2) Array
        byte[] rgb; // dim(1) Array
        System.Drawing.Imaging.BitmapData imgData; // Image Data
        IntPtr ptr;
        int allPixelBits, eachRowLenBits, eachPixelBits;

        //Using Lock & UnLock method to store bmp data in memory
        private void lockBMP(Bitmap bmp)
        {
            // Define rect object : Image range
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            // Lock the bitmap's bits in system memory.
            imgData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);

            ptr = imgData.Scan0;  // Get the address of the first line.
            eachRowLenBits = imgData.Stride;  // Get each row's length (bytes)
            eachPixelBits = (int)Math.Floor((double)eachRowLenBits / (double)bmp.Width); // Get each pixels (bytes)
            allPixelBits = eachRowLenBits * bmp.Height; // Get Image all pixels (bytes)

            rgb = new byte[allPixelBits]; // Declear rgb array
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgb, 0, allPixelBits); //Config bmp data to rgb array memory
        }
        private void unlockBMP(Bitmap bmp)
        {
            System.Runtime.InteropServices.Marshal.Copy(rgb, 0, ptr, allPixelBits); //Config rgb array data to bmp data
            bmp.UnlockBits(imgData); //unlock bmp bits
        }
    }

    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

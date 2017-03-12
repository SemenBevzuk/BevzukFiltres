using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BevzukFiltres
{
    class GrayWorldFilter : Filtres
    {
        private float Avg;
        private float avgR;
        private float avgG;
        private float avgB;

        private void FindAvg(Bitmap sourceImage)
        {
            int n = sourceImage.Width * sourceImage.Height;
            int sumR = 0;
            int sumG = 0;
            int sumB = 0;
            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    sumR += sourceImage.GetPixel(i, j).R;
                    sumG += sourceImage.GetPixel(i, j).G;
                    sumB += sourceImage.GetPixel(i, j).B;
                }
            }
            avgR = sumR / n;
            avgG = sumG / n;
            avgB = sumB / n;
            Avg = (avgR + avgB + avgG) /3;
        }

        protected override Color calculatePixelColor(Bitmap sourceImage, int x, int y)
        {
            int R = Clamp((int)(sourceImage.GetPixel(x, y).R*Avg/avgR),0,255);
            int G = Clamp((int)(sourceImage.GetPixel(x, y).G*Avg/avgG),0,255);
            int B = Clamp((int)(sourceImage.GetPixel(x, y).B*Avg/avgB),0,255);
            Color c = Color.FromArgb(R,G,B);
            return c;
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            FindAvg(sourceImage);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i/resultImage.Width*100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculatePixelColor(sourceImage, i, j));
                }
            }
            return resultImage;
        }
    }
}

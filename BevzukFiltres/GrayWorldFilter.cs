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

        protected override void BeforeProcessImage(Bitmap sourceImage)
        {
            FindAvg(sourceImage);
        }

        protected override Color CalculatePixelColor(Bitmap sourceImage, int x, int y)
        {
            int R = Clamp((int)(sourceImage.GetPixel(x, y).R*Avg/avgR),0,255);
            int G = Clamp((int)(sourceImage.GetPixel(x, y).G*Avg/avgG),0,255);
            int B = Clamp((int)(sourceImage.GetPixel(x, y).B*Avg/avgB),0,255);
            Color c = Color.FromArgb(R,G,B);
            return c;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Drawing;
using System.Text;

namespace BevzukFiltres
{
    class LinearStretchingFilter : Filtres
    {
        int maxR, minR,
            maxG, minG,
            maxB, minB;

        private void FindMaxMin(Bitmap sourceImage)
        {
            maxR = sourceImage.GetPixel(0, 0).R;
            minR = sourceImage.GetPixel(0, 0).R;
            maxG = sourceImage.GetPixel(0, 0).G;
            minG = sourceImage.GetPixel(0, 0).G;
            maxB = sourceImage.GetPixel(0, 0).B;
            minB = sourceImage.GetPixel(0, 0).B;


            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color c = sourceImage.GetPixel(i, j);

                    if (minR > c.R) minR = c.R;
                    if (maxR < c.R) maxR = c.R;
                    if (minG > c.G) minG = c.G;
                    if (maxG < c.G) maxG = c.G;
                    if (minB > c.B) minB = c.B;
                    if (maxB < c.B) maxB = c.B;

                }
            }
        }

        protected override Color CalculatePixelColor(Bitmap sourceImage, int x, int y)
        {
            int R = (sourceImage.GetPixel(x,y).R-minR) * (255 / (maxR - minR));
            int G = (sourceImage.GetPixel(x,y).G-minG) * (255 / (maxG - minG));
            int B = (sourceImage.GetPixel(x,y).B-minB) * (255 / (maxB - minB));
            Color c = Color.FromArgb(R,G,B);
            return c;
        }

        protected override void BeforeProcessImage(Bitmap sourceImage)
        {
            FindMaxMin(sourceImage);
        }
    }
}

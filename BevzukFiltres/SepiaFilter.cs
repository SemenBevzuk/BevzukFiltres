using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BevzukFiltres
{
    class SepiaFilter:Filtres
    {
        protected override Color CalculatePixelColor(Bitmap sourceImage, int x, int y)
        {
            int Intensity = (int)(0.36*sourceImage.GetPixel(x, y).R +
                            0.53*sourceImage.GetPixel(x, y).G +
                            0.11*sourceImage.GetPixel(x, y).B);
            //Intensity = Clamp(Intensity, 0, 255);
            int k = 100;
            int R = Clamp(Intensity+2*k, 0, 255);
            int G = Clamp((int)(Intensity+0.5*k), 0, 255);
            int B = Clamp(Intensity-k, 0, 255);
            Color c = Color.FromArgb(R,G,B);
            return c;
        }
    }
}

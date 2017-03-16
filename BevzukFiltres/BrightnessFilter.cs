using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BevzukFiltres
{
    class BrightnessFilter:Filtres
    {
        protected override Color CalculatePixelColor(Bitmap sourceImage, int x, int y)
        {
            int k = 20;
            int R = Clamp(sourceImage.GetPixel(x,y).R + k, 0, 255);
            int G = Clamp(sourceImage.GetPixel(x,y).G + k, 0, 255);
            int B = Clamp(sourceImage.GetPixel(x,y).B + k, 0, 255);
            Color c = Color.FromArgb(R,G,B);
            return c;
        }
    }
}

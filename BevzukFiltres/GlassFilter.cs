using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BevzukFiltres
{
    class GlassFilter:Filtres
    {
        protected override System.Drawing.Color calculatePixelColor(Bitmap sourceImage, int x, int y)
        {
            Random rand = new Random();
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = sourceColor;
            if (x>5 && x<sourceImage.Width-5 &&
                y>5 && y<sourceImage.Height-5)
            {
                int temp_x = (int)(x + (rand.NextDouble() - 0.5)*10);
                int temp_y = (int)(y + (rand.NextDouble() - 0.5)*10);
                resultColor = sourceImage.GetPixel(temp_x, temp_y);
            }
            return resultColor;
        }
    }
}

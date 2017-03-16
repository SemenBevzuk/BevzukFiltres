using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BevzukFiltres
{
    class WaveTwoFilter:Filtres
    {
        protected override Color CalculatePixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = sourceColor;
            int temp_x = (int)(x + 20 * Math.Sin(2 * Math.PI * x / 30.0));
            int temp_y = (int)(y);
            if (temp_x < 0 || temp_x >= sourceImage.Width)
            {
                temp_x = x;
            }
            resultColor = sourceImage.GetPixel(temp_x, temp_y);
            return resultColor;
        }
    }
}

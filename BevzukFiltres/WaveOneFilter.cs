using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BevzukFiltres
{
    class WaveOneFilter : Filtres
    {
        protected override Color CalculatePixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = sourceColor;
            int temp_x = (int)(x + 20 * Math.Sin(2 * Math.PI * y / 60.0));
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

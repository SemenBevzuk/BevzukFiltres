using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BevzukFiltres
{
    class TransferringFilter:Filtres
    {
        protected override Color CalculatePixelColor(Bitmap sourceImage, int x, int y)
        {
            int delta = 100;
            Color resultColor;
            if (x < sourceImage.Width - delta)
            {
                resultColor = sourceImage.GetPixel(x+delta, y);
            }
            else
            {
                resultColor = Color.Black;
            }
            return resultColor;
        }
    }
}

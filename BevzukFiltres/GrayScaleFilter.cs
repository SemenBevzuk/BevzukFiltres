using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BevzukFiltres
{
    class GrayScaleFilter: Filtres
    {
        protected override Color CalculatePixelColor(Bitmap sourceImage, int x, int y)
        {
            int Intensity = (int)(0.36*sourceImage.GetPixel(x, y).R +
                            0.53*sourceImage.GetPixel(x, y).G +
                            0.11*sourceImage.GetPixel(x, y).B);
            Intensity = Clamp(Intensity, 0, 255);
            Color c = Color.FromArgb(Intensity,Intensity,Intensity);
            return c;
        }
    }
}

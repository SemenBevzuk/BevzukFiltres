using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BevzukFiltres
{
    class PruittaFilter:MatrixFilter
    {
        public int[,] pruittX = new int[3, 3] { { -1, 0, 1 },
                                               { -1, 0, 1 },
                                               { -1, 0, 1 } };

        public int[,] pruittY = new int[3, 3] { { -1, -1, -1 },
                                               { 0, 0, 0 }, 
                                               { 1, 1, 1 } };

        private Color SumColor(Color a, Color b)
        {
            Color rescolor = Color.FromArgb(
                Clamp((int) Math.Sqrt(Math.Pow(a.R, 2) + Math.Pow(b.R, 2)), 0, 255),
                Clamp((int) Math.Sqrt(Math.Pow(a.G, 2) + Math.Pow(b.G, 2)), 0, 255),
                Clamp((int) Math.Sqrt(Math.Pow(a.B, 2) + Math.Pow(b.B, 2)), 0, 255)
                );
            return rescolor;
        }

        protected override Color CalculatePixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = 1;
            int radiusY = 1;
            float XresultR = 0;
            float XresultG = 0;
            float XresultB = 0;
            float YresultR = 0;
            float YresultG = 0;
            float YresultB = 0;
            for(int l = -radiusY; l<=radiusY; l++)
                for(int k = - radiusX; k<=radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + 1, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    XresultR += neighborColor.R * pruittX[k + radiusX, l + radiusY];
                    XresultG += neighborColor.G * pruittX[k + radiusX, l + radiusY];
                    XresultB += neighborColor.B * pruittX[k + radiusX, l + radiusY];
                    YresultR += neighborColor.R * pruittY[k + radiusX, l + radiusY];
                    YresultG += neighborColor.G * pruittY[k + radiusX, l + radiusY];
                    YresultB += neighborColor.B * pruittY[k + radiusX, l + radiusY];
                }
            Color cX = Color.FromArgb(
                Clamp((int)XresultR, 0, 255),
                Clamp((int)XresultG, 0, 255),
                Clamp((int)XresultB, 0, 255)
                );
            Color cY = Color.FromArgb(
                Clamp((int)YresultR, 0, 255),
                Clamp((int)YresultG, 0, 255),
                Clamp((int)YresultB, 0, 255)
                );
            Color res = SumColor(cX, cY);
            return res;
        }
    }
}

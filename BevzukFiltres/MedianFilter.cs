using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BevzukFiltres
{
    class MedianFilter : MatrixFilter
    {
        public MedianFilter(int n)
        {
            kernel = new float[n,n];
        }

        protected override Color calculatePixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            int n = kernel.GetLength(0)*kernel.GetLength(1);
            float resultR = 0;
            float resultG = 0;
            float resultB = 0;
            int[] cR = new int[n];
            int[] cB = new int[n];
            int[] cG = new int[n];
            int g = 0;
           
            for(int l = -radiusY; l<=radiusY; l++)
                for(int k = - radiusX; k<=radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color c = sourceImage.GetPixel(idX, idY);
                    cR[g] = c.R;
                    cG[g] = c.G;
                    cB[g] = c.B;
                    g++;
                }
            quickSort(cR, 0, n-1);
            quickSort(cG, 0, n-1);
            quickSort(cB, 0, n-1);
            int med = (int)(n / 2)+1;
            resultR = cR[med];
            resultG = cG[med];
            resultB = cB[med];
            return Color.FromArgb(
                Clamp((int)resultR, 0, 255),
                Clamp((int)resultG, 0, 255),
                Clamp((int)resultB, 0, 255)
                );
        }

        static void quickSort(int[] a, int l, int r)
        {
            int temp;
            int x = a[l + (r - l) / 2];
 
            int i = l;
            int j = r;
            while (i <= j)
            {
                while (a[i] < x) i++;
                while (a[j] > x) j--;
                if (i <= j)
                {
                    temp = a[i];
                    a[i] = a[j];
                    a[j] = temp;
                    i++;
                    j--;
                }
            }
            if (i < r)
                quickSort(a, i, r);

            if (l < j)
                quickSort(a, l, j);
        }
    }
}

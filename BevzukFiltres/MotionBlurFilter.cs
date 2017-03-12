using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BevzukFiltres
{
    class MotionBlurFilter : MatrixFilter
    {
        public MotionBlurFilter(int n)
        {
            int sizeX = n;
            int sizeY = n;
            kernel = new float[sizeX, sizeY];
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    if (i != j)
                    {
                        kernel[i, j] = 0;
                    }
                    else
                    {
                        kernel[i, j] = (float) (1.0/n);
                    }
                }
            }
        }
    }
}

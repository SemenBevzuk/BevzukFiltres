﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BevzukFiltres
{
    class BlackHatFilter:MatMorphology
    {
        public BlackHatFilter(MaskType maskType)
            : base(maskType)
        {
        }

        public override Bitmap ProcessImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            Bitmap tempImage_1 = new Bitmap(sourceImage.Width, sourceImage.Height);
            Bitmap tempImage_2 = new Bitmap(sourceImage.Width, sourceImage.Height);
            worker.ReportProgress((int) (1));
            tempImage_1 = Closing(sourceImage, worker);
            Color col;
            int R, G, B;
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i/resultImage.Width*100));
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    R = tempImage_1.GetPixel(i, j).R - sourceImage.GetPixel(i, j).R;
                    G = tempImage_1.GetPixel(i, j).G - sourceImage.GetPixel(i, j).G;
                    B = tempImage_1.GetPixel(i, j).B - sourceImage.GetPixel(i, j).B;
                    R = Clamp(R, 0, 255);
                    G = Clamp(G, 0, 255);
                    B = Clamp(B, 0, 255);
                    col = Color.FromArgb(R, G, B);
                    resultImage.SetPixel(i, j, col);
                }
            }
            return resultImage;
        }
    }
}

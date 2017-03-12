using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Drawing;
namespace BevzukFiltres
{
    public enum MaskType
    {
        Square,
        Cross
    }
    abstract class MatMorphology : Filtres
    {
        public bool[,] structElem;

        public MatMorphology(MaskType maskType)
        {
            switch (maskType)
            {
                case MaskType.Square:
                    {
                        structElem = new bool[,]
                    {
                        {true, true, true},
                        {true, true, true},
                        {true, true, true},
                    };
                        break;
                    }
                case MaskType.Cross:
                    {
                        structElem = new bool[,]
                    {
                        {false, true, false},
                        {true, true, true},
                        {false, true, false},
                    };
                        break;
                    }
            }
        }

        public Bitmap Dilation(Bitmap sourceImage, BackgroundWorker worker)//+
        {
            
            int width = sourceImage.Width;
            int height = sourceImage.Height;
            Bitmap res = new Bitmap(width,height);
            int structWidth = structElem.GetLength(0);
            int structHeight = structElem.GetLength(1);

            for (int y = structHeight/2; y < height - structHeight/2; y++)
            {
                worker.ReportProgress((int)((float)y/sourceImage.Width*100));
                for (int x = structWidth/2; x < width - structWidth/2; x++)
                {
                    int maxR = 0;
                    int maxG = 0;
                    int maxB = 0;
                    int k = 0;
                    for (int j = -structHeight/2; j <= structHeight/2; j++)
                    {
                        int l = 0;
                        for (int i = -structWidth/2; i <= structWidth/2; i++)
                        {
                            
                            if ((structElem[l, k]) && (sourceImage.GetPixel(x + i, y + j).R > maxR))
                                maxR = sourceImage.GetPixel(x + i, y + j).R;
                            if ((structElem[l, k]) && (sourceImage.GetPixel(x + i, y + j).G > maxG))
                                maxG = sourceImage.GetPixel(x + i, y + j).G;
                            if ((structElem[l, k]) && (sourceImage.GetPixel(x + i, y + j).B > maxB))
                                maxB = sourceImage.GetPixel(x + i, y + j).B;
                            l++;
                        }
                        k++;
                    }
                    Color col = Color.FromArgb(maxR,maxG,maxB);
                    res.SetPixel(x,y,col);
                }
            }
            return res;
        }

        public Bitmap Erosion(Bitmap sourceImage,BackgroundWorker worker)//-
        {
            int width = sourceImage.Width;
            int height = sourceImage.Height;
            Bitmap res = new Bitmap(width,height);
            int structWidth = structElem.GetLength(0);
            int structHeight = structElem.GetLength(1);

            for (int y = structHeight/2; y < height - structHeight/2; y++)
            {
                worker.ReportProgress((int)((float)y/sourceImage.Width*100));
                for (int x = structWidth/2; x < width - structWidth/2; x++)
                {

                    int minR = 255;
                    int minG = 255;
                    int minB = 255;

                    int k = 0;
                    for (int j = -structHeight/2; j <= structHeight/2; j++)
                    {
                        int l = 0;
                        for (int i = -structWidth/2; i <= structWidth/2; i++)
                        {
                            if ((structElem[l, k]) && (sourceImage.GetPixel(x + i, y + j).R < minR))
                                minR = sourceImage.GetPixel(x + i, y + j).R;
                            if ((structElem[l, k]) && (sourceImage.GetPixel(x + i, y + j).G < minG))
                                minG = sourceImage.GetPixel(x + i, y + j).G;
                            if ((structElem[l, k]) && (sourceImage.GetPixel(x + i, y + j).B < minB))
                                minB = sourceImage.GetPixel(x + i, y + j).B;
                            l++;
                        }
                        k++;
                    }
                    Color col = Color.FromArgb(minR, minB, minG);
                    res.SetPixel(x, y, col);
                }
            }
            return res;
        }

        public Bitmap Opening(Bitmap sourceImage, BackgroundWorker worker)
        {
            int width = sourceImage.Width;
            int height = sourceImage.Height;
            Bitmap res = new Bitmap(width,height);

            res = Erosion(res, worker);
            res = Dilation(res, worker);

            return res;
        }

        public Bitmap Сlosing(Bitmap sourceImage, BackgroundWorker worker)
        {
            int width = sourceImage.Width;
            int height = sourceImage.Height;
            Bitmap res = new Bitmap(width,height);

            res = Dilation(res, worker);
            res = Erosion(res, worker);

            return res;
        }
    }
}

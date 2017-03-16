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
        Square3,
        Square5,
        Cross3
    }
    abstract class MatMorphology : Filtres
    {
        public bool[,] structElem;

        public MatMorphology(MaskType maskType)
        {
            switch (maskType)
            {
                case MaskType.Square3:
                    {
                        structElem = new bool[,]
                    {
                        {true, true, true},
                        {true, true, true},
                        {true, true, true},
                    };
                        break;
                    }
                case MaskType.Cross3:
                    {
                        structElem = new bool[,]
                    {
                        {false, true, false},
                        {true, true, true},
                        {false, true, false},
                    };
                        break;
                    }
                case MaskType.Square5:
                    {
                        structElem = new bool[,]
                    {
                        {true, true, true, true, true},
                        {true, true, true, true, true},
                        {true, true, true, true, true},
                        {true, true, true, true, true},
                        {true, true, true, true, true}
                    };
                        break;
                    }
            }
        }

        public Bitmap Dilation(Bitmap sourceImage, BackgroundWorker worker)//+
        {

            int width = sourceImage.Width;
            int height = sourceImage.Height;
            Bitmap res = new Bitmap(sourceImage);
            int structWidth = structElem.GetLength(0);
            int structHeight = structElem.GetLength(1);

            for (int y = structHeight / 2; y < height - structHeight / 2; y++)
            {
                worker.ReportProgress((int)((float)y / sourceImage.Width * 100));
                for (int x = structWidth / 2; x < width - structWidth / 2; x++)
                {
                    int maxR = sourceImage.GetPixel(x, y).R;;
                    int maxG = sourceImage.GetPixel(x, y).G;;
                    int maxB = sourceImage.GetPixel(x, y).B;;
                    int k = 0;
                    for (int j = -structHeight / 2; j <= structHeight / 2; j++)
                    {
                        int l = 0;
                        for (int i = -structWidth / 2; i <= structWidth / 2; i++)
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
                    Color col = Color.FromArgb(maxR, maxG, maxB);
                    res.SetPixel(x, y, col);
                }
            }
            return res;
        }

        public Bitmap Erosion(Bitmap sourceImage, BackgroundWorker worker)//-
        {
            int width = sourceImage.Width;
            int height = sourceImage.Height;
            Bitmap res = new Bitmap(sourceImage);
            int structWidth = structElem.GetLength(0);
            int structHeight = structElem.GetLength(1);

            for (int y = structHeight / 2; y < height - structHeight / 2; y++)
            {
                worker.ReportProgress((int)((float)y / sourceImage.Width * 100));
                for (int x = structWidth / 2; x < width - structWidth / 2; x++)
                {

                    int minR = sourceImage.GetPixel(x, y).R;
                    int minG = sourceImage.GetPixel(x, y).G;
                    int minB = sourceImage.GetPixel(x, y).B;

                    int k = 0;
                    for (int j = -structHeight / 2; j <= structHeight / 2; j++)
                    {
                        int l = 0;
                        for (int i = -structWidth / 2; i <= structWidth / 2; i++)
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
                    Color col = Color.FromArgb(minR, minG, minB);
                    res.SetPixel(x, y, col);
                }
            }
            return res;
        }

        public Bitmap Opening(Bitmap sourceImage,  BackgroundWorker worker)
        {
            Bitmap res  = new Bitmap(sourceImage);
            res = Erosion(sourceImage, worker);
            res = Dilation(sourceImage, worker);
            return res;
        }

        public Bitmap Closing(Bitmap sourceImage,  BackgroundWorker worker)
        {
            Bitmap res = Dilation(sourceImage, worker);
            res = Erosion(res, worker);
            return res;
        }
    }
}

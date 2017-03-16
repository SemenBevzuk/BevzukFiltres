using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BevzukFiltres
{
    class OpeningFilter:MatMorphology
    {
         public OpeningFilter(MaskType maskType)
            : base(maskType)
        {
        }

        public override Bitmap ProcessImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            resultImage = Opening(sourceImage,worker);
            return resultImage;
        }
    }
}

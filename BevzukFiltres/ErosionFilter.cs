using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BevzukFiltres
{
    class ErosionFilter:MatMorphology
    {
         public ErosionFilter(MaskType maskType)
            : base(maskType)
        {
        }

        public override Bitmap ProcessImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            resultImage = Erosion(sourceImage,worker);
            return resultImage;
        }
    }
}

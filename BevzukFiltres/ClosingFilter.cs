using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BevzukFiltres
{
    class ClosingFilter:MatMorphology
    {
        public ClosingFilter(MaskType maskType)
            : base(maskType)
        {
        }

        public override Bitmap ProcessImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            resultImage = Closing(sourceImage,worker);
            return resultImage;
        }
    }
}

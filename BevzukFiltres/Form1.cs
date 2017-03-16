using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace BevzukFiltres
{
    public partial class Form1 : Form
    {
        Bitmap image;
        Stack<Bitmap> BitmapStack = new Stack<Bitmap>();
        public Form1()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "image files|*.jpg;*.jpeg;*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
        }

        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new InvertFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BitmapStack.Push((Bitmap)pictureBox1.Image);
                Bitmap newImage = ((Filtres)e.Argument).ProcessImage(image, backgroundWorker1);
                if (backgroundWorker1.CancellationPending != true)
                    image = newImage;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка. Нет изображения.");
            }

        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            progressBar1.Value = 0;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void гауссаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new GaussianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void чернобелоеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new GrayScaleFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void переносToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new TransferringFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void стеклоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new GlassFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void motionBlurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new MotionBlurFilter(9);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void крестикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new GradFilter(MaskType.Cross3);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void квадратToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new GradFilter(MaskType.Square3);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void медианныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new MedianFilter(5);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void линейноеРастяжениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new LinearStretchingFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void серыйМирToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new GrayWorldFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap saveImage = (Bitmap)pictureBox1.Image;
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.DefaultExt = "bmp";
                dialog.Filter = "image files|*.bmp;*.jpeg;*.jpg;*.png";
                if (dialog.ShowDialog() == DialogResult.OK)
                    saveImage.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка. Нет изображения.");
            }
        }

        private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap img = BitmapStack.Pop();
                pictureBox1.Image = img;
                pictureBox1.Refresh();
            }
            catch (Exception)
            {
                MessageBox.Show("Стек изображений пуст.");
            }
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new SepiaFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void яркостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new BrightnessFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void ядро5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new DefinitionFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void ядро9ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new AcutanceFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void собеляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new SobelFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void щарраToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new ShcharrFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void прюиттаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new PruittaFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void волна1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new WaveOneFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void волна2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new WaveTwoFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void квадрат5x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new GradFilter(MaskType.Square5);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void крест3х3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new TopHatFilter(MaskType.Cross3);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void квадрат3х3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new TopHatFilter(MaskType.Square3);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void квадрат5х5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtres filter = new TopHatFilter(MaskType.Square5);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void крест3х3ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Filtres filter = new BlackHatFilter(MaskType.Cross3);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void квадрат3х3ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Filtres filter = new BlackHatFilter(MaskType.Square3);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void квадрат5х5ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Filtres filter = new BlackHatFilter(MaskType.Square5);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void крест3х3ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Filtres filter = new DilationFilter(MaskType.Cross3);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void квадрат3х3ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Filtres filter = new DilationFilter(MaskType.Square3);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void квадрат5х5ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Filtres filter = new DilationFilter(MaskType.Square5);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void крест3х3ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Filtres filter = new ErosionFilter(MaskType.Cross3);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void квадрат3х3ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Filtres filter = new ErosionFilter(MaskType.Square3);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void квадрат5х5ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Filtres filter = new ErosionFilter(MaskType.Square5);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void крест3х3ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Filtres filter = new OpeningFilter(MaskType.Cross3);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void квадрат3х3ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Filtres filter = new OpeningFilter(MaskType.Square3);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void квадрат5х5ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Filtres filter = new OpeningFilter(MaskType.Square5);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void крест3х3ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Filtres filter = new ClosingFilter(MaskType.Cross3);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void квадрат3х3ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Filtres filter = new ClosingFilter(MaskType.Square3);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void квадрат5х5ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Filtres filter = new ClosingFilter(MaskType.Square5);
            backgroundWorker1.RunWorkerAsync(filter);
        }
    }
}

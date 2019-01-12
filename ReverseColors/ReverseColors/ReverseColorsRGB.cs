using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InterfaceBridge;

class ReverseColorsRGB : IPlugin
{
    private IImageOperation imageOperation;
    private Bitmap processedBitmap;
    private ComponentResourceManager cM;

    public Bitmap GetImage()
    {
        return new Bitmap("ReverseColors.png");
    }

    public void ProcessImage(object sender, EventArgs e)
    {
        if (imageOperation.IsBusy())
            return;
        imageOperation.ChangeState();
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        backgroundWorker.DoWork += DoWork;
        backgroundWorker.RunWorkerCompleted += Update;
        backgroundWorker.RunWorkerAsync();
    }

    private void DoWork(object sender, DoWorkEventArgs e)
    {
        Bitmap actualBitmap = imageOperation.GetActualImage();
        if (actualBitmap != null)
        {
            processedBitmap = (Bitmap)actualBitmap.Clone();
            for(int i = 0; i < processedBitmap.Width; i++)
                for(int j = 0; j < processedBitmap.Height; j++)
                {
                    Color color = processedBitmap.GetPixel(i, j);
                    int R = 255 - color.R;
                    int G = 255 - color.G;
                    int B = 255 - color.B;
                    color = Color.FromArgb(R, G, B);
                    processedBitmap.SetPixel(i, j, color);
                }
            Thread.Sleep(10000);
        }
    }

    private void Update(object sender, RunWorkerCompletedEventArgs e)
    {
        imageOperation.ChangeImage(processedBitmap);
        imageOperation.ChangeState();
    }


    public void SetImageOperation(IImageOperation imageOperation)
    {
        this.imageOperation = imageOperation;
    }

    public string GetName()
    {
        return "reverseColorsRGB";
    }

    public ComponentResourceManager GetResourceManager()
    {
        if (cM == null)
            cM = new ComponentResourceManager(this.GetType());
        return cM;
    }
}

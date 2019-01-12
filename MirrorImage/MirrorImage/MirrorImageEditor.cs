using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InterfaceBridge;

class MirrorImageEditor : IPlugin
{
    private IImageOperation imageOperation;
    private Bitmap processedBitmap;
    private ComponentResourceManager cM;

    public Bitmap GetImage()
    {
        return new Bitmap("MirrorImage.png");
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
            for (int i = 0; i < processedBitmap.Width; i++)
                for (int j = 0; j < processedBitmap.Height; j++)
                    processedBitmap.SetPixel(i, j, actualBitmap.GetPixel(actualBitmap.Width - 1 - i, j));
            Thread.Sleep(1000);
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
        return "mirrorImage";
    }

    public ComponentResourceManager GetResourceManager()
    {
        if (cM == null)
            cM = new ComponentResourceManager(this.GetType());
        return cM;
    }
}

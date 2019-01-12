using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InterfaceBridge;

class RotateLeft90 : IPlugin
{
    private IImageOperation imageOperation;

    private Bitmap processedBitmap;
    private ComponentResourceManager cM;

    public Bitmap GetImage()
    {
        return new Bitmap("RotateLeft.png");
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
            processedBitmap.SetResolution(actualBitmap.Height, actualBitmap.Width);
            processedBitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
            Thread.Sleep(100);
        }
        else
            processedBitmap = null;
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
        return "rotateLeft90";
    }

    public ComponentResourceManager GetResourceManager()
    {
        if (cM == null)
            cM = new ComponentResourceManager(this.GetType());
        return cM;
    }
}

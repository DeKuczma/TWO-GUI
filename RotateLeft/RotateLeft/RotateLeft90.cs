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

    public Bitmap GetImage()
    {
        return new Bitmap("RotateLeft.png");
    }

    public void ProcessImage(object sender, EventArgs e)
    {
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        backgroundWorker.DoWork += DoWork;
        backgroundWorker.RunWorkerCompleted += Update;
        backgroundWorker.RunWorkerAsync();
    }

    private void DoWork(object sender, DoWorkEventArgs e)
    {
        Bitmap actualBitmap = imageOperation.GetActualImage();
        processedBitmap = (Bitmap)actualBitmap.Clone();
        processedBitmap.SetResolution(actualBitmap.Height, actualBitmap.Width);
        processedBitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
        Thread.Sleep(100);
    }

    private void Update(object sender, RunWorkerCompletedEventArgs e)
    {
        imageOperation.ChangeImage(processedBitmap);
    }

    public void SetImageOperation(IImageOperation imageOperation)
    {
        this.imageOperation = imageOperation;
    }

    public string GetPolishName()
    {
        return "Obróc w lewo";
    }

    public string GetEnglishName()
    {
        return "Rotate left";
    }

    public string GetPolishTooltopName()
    {
        return "Obróć obrazek w lewo o 90 stopni";
    }

    public string GetEnglishTooltipName()
    {
        return "Rotate image left by 90 degrees";
    }
}

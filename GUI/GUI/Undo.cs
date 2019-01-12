using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceBridge;
using System.Threading;

namespace GUI
{
    class Undo : IPlugin
    {
        private IImageOperation imageOperation;

        public Bitmap GetImage()
        {
            return new Bitmap("Undo.png");
        }

        public void ProcessImage(object sender, EventArgs e)
        {
            if (imageOperation.IsBusy())
                return;
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += DoWork;
            backgroundWorker.RunWorkerCompleted += Update;
            backgroundWorker.RunWorkerAsync();
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(10);
        }

        private void Update(object sender, RunWorkerCompletedEventArgs e)
        {
            imageOperation.UndoImage();
        }

        public void SetImageOperation(IImageOperation newImageOperation)
        {
            imageOperation = newImageOperation;
        }

        public string GetName()
        {
            return "undo";
        }

        public ComponentResourceManager GetResourceManager()
        {
            return new ComponentResourceManager(this.GetType());
        }
    }
}

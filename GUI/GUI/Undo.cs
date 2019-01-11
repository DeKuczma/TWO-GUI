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
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += DoWork;
            backgroundWorker.RunWorkerCompleted += Update;
            backgroundWorker.RunWorkerAsync();
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(100);
        }

        private void Update(object sender, RunWorkerCompletedEventArgs e)
        {
            imageOperation.UndoImage();
        }

        public void SetImageOperation(IImageOperation newImageOperation)
        {
            imageOperation = newImageOperation;
        }

        public string GetPolishName()
        {
            return "Cofnij";
        }

        public string GetEnglishName()
        {
            return "Undo";
        }

        public string GetPolishTooltopName()
        {
            return "Cofnij";
        }

        public string GetEnglishTooltipName()
        {
            return "Undo";
        }
    }
}

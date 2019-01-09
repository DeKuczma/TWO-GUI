using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class Redo : IPlugin
    {
        private IImageOperation imageOperation;

        public Image GetImage()
        {
            return new Bitmap("Redo.png");
        }

        public void ProcessImage(object sender, EventArgs e)
        {
            imageOperation.RedoImage();
        }

        public void SetImageOperation(IImageOperation newImageOperation)
        {
            imageOperation = newImageOperation;
        }
    }
}

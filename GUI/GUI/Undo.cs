using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceBridge;

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
            imageOperation.UndoImage();
        }

        public void SetImageOperation(IImageOperation newImageOperation)
        {
            imageOperation = newImageOperation;
        }
    }
}

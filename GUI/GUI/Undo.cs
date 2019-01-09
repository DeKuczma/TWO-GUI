using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class Undo : IPlugin
    {
        private IImageOperation imageOperation;

        public Image GetImage()
        {
            return new Bitmap("Undo.png");
        }

        public void ProcessImage(object sender, EventArgs e)
        {

        }

        public void SetImageOperation(IImageOperation newImageOperation)
        {
            imageOperation = newImageOperation;
        }
    }
}

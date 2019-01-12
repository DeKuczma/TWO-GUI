using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceBridge;

namespace GUI
{
    class Redo : IPlugin
    {
        private IImageOperation imageOperation;

        public string GetEnglishName()
        {
            return "Redo";
        }

        public string GetEnglishTooltipName()
        {
            return "Redo";
        }

        public Bitmap GetImage()
        {
            return new Bitmap("Redo.png");
        }

        public string GetName()
        {
            return "redo";
        }

        public ComponentResourceManager GetResourceManager()
        {
            return new ComponentResourceManager(this.GetType());
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

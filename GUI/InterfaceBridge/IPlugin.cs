using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceBridge
{
    public interface IPlugin
    {
        void SetImageOperation(IImageOperation imageOperation);
        Bitmap GetImage();
        void ProcessImage(object sender, EventArgs e);
    }
}

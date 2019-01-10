using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterfaceBridge
{
    public interface IImageOperation
    {
        void UndoImage();
        void RedoImage();
        Bitmap GetActualImage();
        void SetPictureBox(PictureBox newPictureBox);
        void SetNewImage(Bitmap image);
        void ChangeImage(Bitmap newImage);
    }
}

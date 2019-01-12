using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InterfaceBridge;

namespace GUI
{
    class ImageOperation : IImageOperation
    {
        Bitmap actualImage;
        PictureBox pictureBox;
        List<Bitmap> undoImage = new List<Bitmap>();
        List<Bitmap> redoImage = new List<Bitmap>();
        private bool busy = false;

        public void ChangeImage(Bitmap newImage)
        {
            if (newImage == null)
                return;

            undoImage.Add(actualImage);
            actualImage = newImage;
            UpdateImage();
        }

        public void RedoImage()
        {
            if (redoImage.Count == 0 || actualImage == null)
                return;
            undoImage.Add((Bitmap)actualImage.Clone());
            actualImage = redoImage[redoImage.Count - 1];
            redoImage.RemoveAt(redoImage.Count - 1);
            UpdateImage();
        }

        public void SetPictureBox(PictureBox newPictureBox)
        {
            pictureBox = newPictureBox;
        }

        public void UndoImage()
        {
            if (undoImage.Count == 0 || actualImage == null)
                return;
            redoImage.Add((Bitmap)actualImage.Clone());
            actualImage = undoImage[undoImage.Count - 1];
            undoImage.RemoveAt(undoImage.Count - 1);
            UpdateImage();
        }

        public Bitmap GetActualImage() => actualImage;

        public void SetNewImage(Bitmap image)
        {
            undoImage = new List<Bitmap>();
            redoImage = new List<Bitmap>();
            actualImage = image;
            UpdateImage();
        }

        private void UpdateImage()
        {
            if (actualImage != null)
            {
                pictureBox.Width = actualImage.Width;
                pictureBox.Height = actualImage.Height;
                pictureBox.Image = actualImage;
            }
        }

        public void ChangeState()
        {
            busy = !busy;
        }

        public bool IsBusy()
        {
            return busy;
        }
    }
}

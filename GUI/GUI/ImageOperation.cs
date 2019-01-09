using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    class ImageOperation : IImageOperation
    {
        Bitmap actualImage;
        PictureBox pictureBox;
        List<Bitmap> undoImage = new List<Bitmap>();
        List<Bitmap> redoImage = new List<Bitmap>();

        public void ChangeImage(Bitmap newImage)
        {
            if (actualImage != newImage)
            {
                undoImage.Add(newImage);
            }
        }

        public Bitmap GetRedoImage()
        {
            if (undoImage.Count == 0)
                return actualImage;
            undoImage.Add((Bitmap)actualImage.Clone());
            actualImage = redoImage[redoImage.Count - 1];
            redoImage.RemoveAt(redoImage.Count - 1);
            return actualImage;
        }

        public void SetPictureBox(PictureBox newPictureBox)
        {
            pictureBox = newPictureBox;
        }

        public Bitmap GetUndoImage()
        {
            if (redoImage.Count == 0)
                return actualImage;
            redoImage.Add((Bitmap)actualImage.Clone());
            actualImage = redoImage[redoImage.Count - 1];
            undoImage.RemoveAt(undoImage.Count - 1);
            return actualImage;
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
            pictureBox.Width = actualImage.Width;
            pictureBox.Height = actualImage.Height;
            pictureBox.Image = actualImage;
        }
    }
}

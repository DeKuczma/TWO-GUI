﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    interface IImageOperation
    {
        Bitmap GetUndoImage();
        Bitmap GetRedoImage();
        Bitmap GetActualImage();
        void SetPictureBox(PictureBox newPictureBox);
        void SetNewImage(Bitmap image);
        void ChangeImage(Bitmap newImage);
    }
}

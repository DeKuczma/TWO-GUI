using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI
{
    class Undo : Plugin
    {
        Bitmap bmp;
        public void Click(object sender, RoutedEventArgs e)
        {
            if (bmp != null)
            {
                for (int i = 0; i < bmp.Width; i++)
                    for (int j = 0; j < bmp.Height; j++)
                        bmp.SetPixel(i, j, Color.BlanchedAlmond);
            }
        }

        public void SetImage(Bitmap image)
        {
            bmp = image;
        }

        public Bitmap GetImage()
        {
            return new Bitmap("Undo.bmp");
        }

        public string GetTooltipText()
        {
            return "Undo";
        }
    }
}

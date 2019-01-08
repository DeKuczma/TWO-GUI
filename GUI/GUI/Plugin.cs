using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI
{
    interface Plugin
    {
        void Click(object sender, RoutedEventArgs e);
        Bitmap GetImage();
        void SetImage(Bitmap image);
        string GetTooltipText();
    }
}

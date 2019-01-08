using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Bitmap image;
        private List<Bitmap> prevOperations;
        private List<Bitmap> nextOperations;
        private List<Plugin> plugins = new List<Plugin>();

        public MainWindow()
        {

            CultureResources.ChangeCulture(Properties.Settings.Default.DefaultCulture);
            InitializeComponent();
            ColumnDefinition customColumn = new ColumnDefinition();
            customColumn.Width = new GridLength(25);
            ToolsGrid.ColumnDefinitions.Add(customColumn);

            Undo undo = new Undo();
            Button myTool = new System.Windows.Controls.Button();
            myTool.Click += UpdateImageRef;
            myTool.Click += undo.Click;
            myTool.Click += PluginClick;
            myTool.Content = undo.GetImage();
            myTool.ToolTip = undo.GetTooltipText();
            ToolsGrid.Children.Add(myTool);
            plugins.Add(undo);
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OpenManuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image Files (*.bmp, *.jpg)|*.bmp;*.jpg";
            if (openFile.ShowDialog() == true)
            {
                image = new Bitmap(openFile.FileName);
                UpdateImage();
            }

        }

        private void ChangeLanguage_Click(object sender, RoutedEventArgs e)
        {
            bool polishClicked = (sender == PolishMenuItem);

            CultureResources.ChangeCulture(new CultureInfo(polishClicked ? "pl" : "en"));
            PolishMenuItem.IsChecked = polishClicked;
            EnglishMenuItem.IsChecked = !polishClicked;
        }
        private void RotateRight_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < image.Width; i++)
                for (int j = 0; j < image.Height; j++)
                    image.SetPixel(i, j, System.Drawing.Color.Aqua);
            UpdateImage();
        }

        private void RotateLeft_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateImageRef(object sender, RoutedEventArgs e)
        {
            foreach (Plugin plugin in plugins)
                plugin.SetImage(image);
        }

        public void PluginClick(object sender, RoutedEventArgs e)
        {            
            UpdateImage();
        }

        private void UpdateImage()
        {
            if (image != null)
            {
                using (var ms = new MemoryStream())
                {
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ms.Position = 0;

                    var bi = new BitmapImage();
                    bi.BeginInit();
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.StreamSource = ms;
                    bi.EndInit();
                    DisplayImage.Source = bi;
                }
            }
        }
    }
}

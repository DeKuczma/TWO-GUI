using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using InterfaceBridge;

namespace GUI
{
    public partial class ImageEditor : Form
    {
        private ImageOperation imageOperation;

        public ImageEditor()
        {
            InitializeComponent();
            englishToolStripMenuItem.Checked = true;
            imageOperation = new ImageOperation();
            imageOperation.SetPictureBox(pictureBox);
            Undo undo = new Undo();
            Redo redo = new Redo();
            AddToolItem(undo);
            AddToolItem(redo);
            LoadPlugins();
        }

        private void AddToolItem(IPlugin plugin)
        {
            plugin.SetImageOperation(imageOperation);
            ToolStripButton stripButton = new ToolStripButton();
            stripButton.Image = plugin.GetImage();
            stripButton.ToolTipText = "TODO";
            stripButton.Click += plugin.ProcessImage;
            stripButton.Click += ProcessToolClick;
            toolStrip.Items.Add(stripButton);
        }

        private void ProcessToolClick(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Localization
        private void ChangeCulture(CultureInfo culture)
        {
            
        }
        #endregion

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image Files (*.jpg)|*.jpg";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                imageOperation.SetNewImage(new Bitmap(openFile.FileName));
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Image Files (*.jpg)|*.jpg";
            if(saveFile.ShowDialog() == DialogResult.OK)
            {
                imageOperation.GetActualImage().Save(saveFile.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        static void EnumFiles(string path, ref List<string> files)
        {
            foreach (var file in Directory.EnumerateFiles(path, "*.dll"))
            {
                files.Add(file);
            }

            foreach (var subPath in Directory.EnumerateDirectories(path))
            {
                EnumFiles(subPath, ref files);
            }
        }

        private void LoadPlugins()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ImageEditor));
            List<string> dllFiles = new List<string>();

            EnumFiles(".", ref dllFiles);

            foreach (var file in dllFiles)
            {
                var pluginAssembly = Assembly.LoadFrom(file);

                var types = pluginAssembly.GetTypes();

                foreach (var type in types)
                {
                    Type contract = type.GetInterfaces().FirstOrDefault();
                    if (contract != null && !type.IsAbstract)
                    {
                        var o = Activator.CreateInstance(type);
                        var w = o as IPlugin;

                        if (w != null)
                        {
                            AddToolItem(w);
                        }
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
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
        List<Type> pluginTypes;

        public ImageEditor()
        {
            InitializeComponent();
            englishToolStripMenuItem.Checked = false;
            polishToolStripMenuItem.Checked = true;
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
            stripButton.Name = plugin.GetType().ToString();
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
            pluginTypes = new List<Type>();
            //ResourceReader enResources = new ResourceReader(".\\ImageEditor.resx");
            //ResourceReader plResources = new ResourceReader(".\\ImageEdit.pl.resx");

            ResourceManager rM = new ResourceManager(typeof(ImageEditor));

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
                            pluginTypes.Add(type);
                            //pluginAssembly.GetManifestResourceInfo("RotateLeft");
                            //// "test" is your image name
                            //ResourceManager resourcemanager = new ResourceManager("RotateLeft.Properties", pluginAssembly);
                            //object obj = resourcemanager.GetObject("RotateLeftText", new global::System.Globalization.CultureInfo("en-US"));
                            //string tekst = (string)obj;
                        }
                    }
                }
            }
        }


        #region Localization
        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender == polishToolStripMenuItem)
            {
                ChangeCulture(new CultureInfo("pl"));
            }
            else
            {
                ChangeCulture(new CultureInfo("en"));
            }
        }

        private void ChangeCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentUICulture = culture;
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ImageEditor));

            resources.ApplyResources(this, "$this", culture);

            UpdateControlsCulture(this, resources, culture);

            if (culture.Name == "pl")
            {
                polishToolStripMenuItem.Checked = true;
                englishToolStripMenuItem.Checked = false;
            }
            else
            {
                englishToolStripMenuItem.Checked = true;
                polishToolStripMenuItem.Checked = false;
            }
        }

        private void UpdateControlsCulture(Control control, ComponentResourceManager resourceProvider,
            CultureInfo culture)
        {
            control.SuspendLayout();
            resourceProvider.ApplyResources(control, control.Name, culture);

            foreach (Control ctrl in control.Controls)
            {
                //toolTip.SetToolTip(ctrl, resourceProvider.GetString(ctrl.Name + ".ToolTip"));
                UpdateControlsCulture(ctrl, resourceProvider, culture);
            }

            control.ResumeLayout(false);
        }

        private void UpdateToolStripItemsCulture(ToolStripItem item, ComponentResourceManager resourceProvider, CultureInfo culture)
        {
            resourceProvider.ApplyResources(item, item.Name, culture);

            if (item is ToolStripMenuItem)
            {
                foreach (ToolStripItem it in ((ToolStripMenuItem)item).DropDownItems)
                {
                    UpdateToolStripItemsCulture(it, resourceProvider, culture);
                }
            }
        }
        #endregion
    }
}

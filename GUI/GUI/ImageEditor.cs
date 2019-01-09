﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        }

        private void AddToolItem(IPlugin plugin)
        {
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
    }
}

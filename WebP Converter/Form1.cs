using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace WebP_Converter
{
   
    public partial class Form1 : Form
    {
        string exePath;
        string imgPath;
        string imgName;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonExe_Click(object sender, EventArgs e)
        {
            if (openFileDialogExe.ShowDialog() == DialogResult.OK)
            {
                textBoxExe.Text = openFileDialogExe.FileName;
                exePath = openFileDialogExe.FileName;
                Properties.Settings.Default.exePath = exePath;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            exePath = Properties.Settings.Default.exePath;
            textBoxExe.Text = exePath;
            openFileDialogExe.InitialDirectory = exePath;
            buttonExe.Focus();
        }

        private void buttonImg_Click(object sender, EventArgs e)
        {
            if (openFileDialogImg.ShowDialog() == DialogResult.OK)
            {
                textBoxImg.Text = openFileDialogImg.FileName;
                imgPath = openFileDialogImg.FileName;
                imgName = Path.GetFileNameWithoutExtension(openFileDialogImg.FileName);
                pictureBoxImg.ImageLocation = imgPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExecuteCommand($"-q 100 {imgPath} -o {imgName}.webp");
        }
        public void ExecuteCommand(string command)
        {
            try
            {
                Process.Start(exePath, command);
                var finalPath = Path.GetDirectoryName(exePath);
                File.Move($"{imgName}.webp", finalPath + $"\\{imgName}.webp");
                MessageBox.Show($".webp File saved in{finalPath}");

            }
            catch (Exception)
            {
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageAppTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Tworzenie okna otwierania nowego pliku
            OpenFileDialog opFile = new OpenFileDialog();
            opFile.Title = "Select a Image";
            opFile.Filter = "jpg files (*.jpg)|*.jpg|All Files (*.*)|*.*";

            // Tworzenie ścieżki i folderu na obrazy - jeżeli nie istnieje
            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\ProImages\";
            if (Directory.Exists(appPath) == false)
            {
                Directory.CreateDirectory(appPath);
            }

            // Otwieranie okna i zapisanie pliku w folderze
            if (opFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string iName = opFile.SafeFileName;
                    string filepath = opFile.FileName;
                    if (!File.Exists(filepath))
                        File.Copy(filepath, appPath + iName);
                    Bitmap Image = new Bitmap(appPath + iName);
                    pictureBox1.Image = Image;
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Unable to open file " + exp.Message);
                }
            }
            else
            {
                opFile.Dispose();
            }
        }
    }
}

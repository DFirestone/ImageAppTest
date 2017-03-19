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
            // Ustawienie nazwy okna
            opFile.Title = "Select a Image";
            // Ustawienie filtrów - w postaci stringa: "nazwa wyświetlana | format (np.: *.jpg)
            opFile.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";

            // Tworzenie ścieżki i folderu na obrazy - jeżeli nie istnieje
            string imageDirectoryPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Obrazy\";
            if (Directory.Exists(imageDirectoryPath) == false)
            {
                Directory.CreateDirectory(imageDirectoryPath);
            }

            // Otwieranie okna i zapisanie pliku w folderze
            if (opFile.ShowDialog() == DialogResult.OK)
            {
                // blok w którym może wystąpić wyjątek przy kopiowaniu pliku do naszego folderu Obrazy
                // wyjątek - błąd w czasie działania programu
                try
                {
                    // nazwa kopiowanego pliku - bez ścieżki
                    string imageName = opFile.SafeFileName;
                    // ścieżka kopiowanego pliku wraz z nazwą
                    string filepath = opFile.FileName;
                    // jeżeli plik plik nie istnieje (nie został już wcześniej skopiowany)
                    // kopiujemy go do folderu Obrazy pod nazwą kryjcą się w zmiennej imageName
                    if (File.Exists(imageDirectoryPath + imageName) == false)
                        File.Copy(filepath, imageDirectoryPath + imageName);

                    // tworzymy bitmapę na świeżo skopiowany obrazek - w konstruktorze podajemy ścieżkę do obrazu w folderze Obrazy
                    Bitmap Image = new Bitmap(imageDirectoryPath + imageName);
                    // ustawiamy obraz PictureBoxa na załadowaną Bitmapę
                    pictureBox1.Image = Image;
                }
                catch (Exception exp) // łapiemy wyjątek i obsługujemy go
                {
                    // obsługujemy wyjątek poprzez wyświetlenie MessageBoxa z komunikatem i informacją szczegółową o wyłapanym wyjątku
                    MessageBox.Show("Unable to open file " + exp.Message);
                }
            }
            else
            {
                // jeżeli nie zaakceptujemy obrazka zamykamy okno
                opFile.Dispose();
            }

        }
    }
}

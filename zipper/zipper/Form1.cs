using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;



namespace zipper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
               

        private void sourceButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog sourcePath = new FolderBrowserDialog();
            if(sourcePath.ShowDialog() == DialogResult.OK)
            {
               // MessageBox.Show(sourcePath.SelectedPath.Length.ToString());                         

                sourceLabel.Text = sourcePath.SelectedPath;

                string temp = sourcePath.SelectedPath;
                nameLabel.Text = temp.Substring(temp.LastIndexOf('\\') + 1);



            }
        }

        private void destinationButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog destinationPath = new FolderBrowserDialog();
           
            if (destinationPath.ShowDialog() == DialogResult.OK)
            {
                destinationLabel.Text = destinationPath.SelectedPath;
            }

        }


        
        private void zipButton_Click(object sender, EventArgs e)
        {
            string source = @sourceLabel.Text.ToString();
            string destination = "";            
          

            if (sameFolderCheck.Checked == true)
            {
                try
                {
                    string temp = source.Remove(source.LastIndexOf('\\')); // dajemy tutaj podwójny slash, poniewać pojedynczy jest odczytywany jako znak specjalny
                    destination = temp + "\\" + nameLabel.Text + ".zip";

                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Nie wybrałes folderu do spakowania");
                    return;
                }

            }
            else
                {
                   destination = @destinationLabel.Text.ToString() + "\\" + nameLabel.Text + ".zip" ;
                }

            if (String.IsNullOrEmpty(source) && destination.Equals("\\" + nameLabel.Text + ".zip")) //sprawdzamy, czy wybrano folder źródłowy i docelowy
            {
                MessageBox.Show("Nie wybrałeś folderu do spakowania ani miejsca docelowego!");
                return;

            }
            else if(String.IsNullOrEmpty(source)) //sprawdzamy tylko czy wybrano folder źródłowy
            {
                MessageBox.Show("Nie wybrałeś folderu do spakowania!");
                return;
            }
            else if (destination.Equals("\\" + nameLabel.Text + ".zip") && !sameFolderCheck.Checked) //sprawdzamy tylko czy wybrano folder docelowy, o ile nie zaznaczono checkboxa
            {
                MessageBox.Show("Nie wybrałeś miejsca docelowego!");
                return;
            }

            if(String.IsNullOrEmpty(nameLabel.Text)) //sprawdzamy, czy pole 'nazwa' jest wypełnione
            {
                MessageBox.Show("Nie wybrałeś nazwy archiwum!");
                return;
            }


            try
            {
                bool unique = !File.Exists(destination); //sprawdzamy, czy plik w podanej lokalizacji o podanej nazwie już istnieje
                if (unique)
                {
                    ZipFile.CreateFromDirectory(source, destination);
                    unique = false;
                    MessageBox.Show("Pomyślnie utworzono archiwum '" + nameLabel.Text + "' !");
                }
                else
                {
                    MessageBox.Show("Plik o podanej nazwie istnieje już w tej lokalizacji!");
                }

                      
            }

            catch (Exception ex)
            {
                MessageBox.Show("Błąd aplikacji nr ff36! ");
            }
        }

        private void sameFolderCheck_CheckedChanged(object sender, EventArgs e)
        {
            if(sameFolderCheck.Checked)
            {
                destinationLabel.Enabled = false;
                destinationButton.Enabled = false;
            }
            else
            {
                destinationLabel.Enabled = true;
                destinationButton.Enabled = true;
                // hejka marchewka
            }
        }
    }
}





 
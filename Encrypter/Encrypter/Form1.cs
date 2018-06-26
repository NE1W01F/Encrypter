using Encrypter.Core;
using System;
using System.IO;
using System.Windows.Forms;

namespace Encrypter
{
    public partial class Form1 : Form
    {
        private const string DIALOG_TITLE = "Select a File";
        private const string ENCRYPTER = "Encrypter";
        private const string DIALOG_FILTER = "Encrypted Files|*.cry";

        public Form1()
        {
            InitializeComponent();
            string username = Environment.UserName;
            if (File.Exists("C:\\Users\\" + username + "\\AppData\\accept.txt"))
            {

            }
            else
            {
                Form3 f2 = new Form3();
                f2.ShowDialog();
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void BtnDecrypt_Click(object sender, System.EventArgs e) // Decrypter
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() {
                Title = DIALOG_TITLE,
                Filter = DIALOG_FILTER
            };
            string applicationUser = Environment.UserName;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var success = new Core.Service.EncryptionService(new EncryptionDTO()
                    {
                        UserName = applicationUser,
                        Password = txtPassword.Text,
                        InputFileName = openFileDialog.FileName,
                        OutputFileName = $"C:\\users\\{applicationUser}\\desktop\\decrypted{txtFileType.Text}"
                    }).Decrypt();
                }
                catch(Exception exception)
                {
                   MessageBox.Show($"Error decrypting file.{exception.Message}", ENCRYPTER, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                MessageBox.Show("Your file has been decrypted.", ENCRYPTER, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnInformation_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }
        
        private void BtnEncrypt_Click(object sender, System.EventArgs e) // Encrypter
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog() { Title = DIALOG_TITLE};

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    new Core.Service.EncryptionService(new EncryptionDTO()
                    {
                        UserName = Environment.UserName,
                        Password = txtPassword.Text,
                        InputFileName = openFileDialog.FileName,
                        OutputFileName = openFileDialog.FileName + ".cry"
                    }).Encrypt();
                    
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error encrypting file.{exception.Message}", ENCRYPTER, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
             MessageBox.Show("Your file is now encrypted. Note: hide you password somewhere safe", "Encrypter", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
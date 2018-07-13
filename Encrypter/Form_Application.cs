using Encrypter.Core;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Encrypter
{
    public partial class Form_Application : Form
    {
        private const string DIALOG_TITLE = "Select a File";
        private const string ENCRYPTER = "Encrypter";
        private const string DIALOG_FILTER = "Encrypted Files|*.cry";

        public Form_Application()
        {
            InitializeComponent();
            string username = Environment.UserName;
            if (File.Exists("C:\\Users\\" + username + "\\AppData\\accept.txt"))
            {
                Process.Start("http://zipansion.com/qKCk");
            }
            else
            {
                Form_EULA f2 = new Form_EULA();
                f2.ShowDialog();
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void Decrypt_Click(object sender, System.EventArgs e) // Decrypter
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
                        OutputFileName = openFileDialog.FileName.Replace(".cry", string.Empty)
                    }).Decrypt();
                    File.Delete(openFileDialog.FileName);
                }
                catch(Exception exception)
                {
                   MessageBox.Show($"Error decrypting file.{exception.Message}", ENCRYPTER, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                MessageBox.Show("Your file has been decrypted.", ENCRYPTER, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Information_Click(object sender, EventArgs e)
        {
            Form_Information f2 = new Form_Information();
            f2.ShowDialog();
        }
        
        private void Encrypt_Click(object sender, System.EventArgs e) // Encrypter
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
                        OutputFileName = $"{openFileDialog.FileName}.cry"
                    }).Encrypt();
                    File.Delete(openFileDialog.FileName);
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
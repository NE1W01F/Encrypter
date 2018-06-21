using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Encrypter
{
    public partial class Form1 : Form
    {
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

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void button3_Click(object sender, System.EventArgs e) // Decrypter
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Select a File";
            openFileDialog1.Filter = "Encrypted Files|*.cry";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string username = Environment.UserName;
                string file1 = openFileDialog1.FileName;
                string inputFile = file1;
                string outputFile = "C:\\users\\" + username + "\\desktop\\decrypted" + textBox4.Text;
                string password = textBox2.Text;
                try
                {
                    UnicodeEncoding UE = new UnicodeEncoding();
                    byte[] key = UE.GetBytes(password);

                    FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                    RijndaelManaged RMCrypto = new RijndaelManaged();

                    CryptoStream cs = new CryptoStream(fsCrypt,
                       RMCrypto.CreateDecryptor(key, key),
                       CryptoStreamMode.Read);

                    FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                    int data;
                    while ((data = cs.ReadByte()) != -1)
                        fsOut.WriteByte((byte)data);

                    fsOut.Close();
                    cs.Close();
                    fsCrypt.Close();
                    MessageBox.Show("Your file has been decrypted.", "Encrypter", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    File.Delete(file1);
                }
                catch
                {
                   MessageBox.Show("The password is not right for that file.", "Encrypter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, System.EventArgs e) // Encrypter
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Select a File";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file1 = openFileDialog1.FileName;

                string inputFile = openFileDialog1.FileName;
                string outputFile = openFileDialog1.FileName + ".cry";
                string password = textBox2.Text;
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                   RMCrypto.CreateEncryptor(key, key),
                   CryptoStreamMode.Write);

                FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);


                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
                MessageBox.Show("Your file is now encrypted. Note: hide you password somewhere safe", "Encrypter", MessageBoxButtons.OK, MessageBoxIcon.Information);
                File.Delete(file1);
            }
        }
    }
}
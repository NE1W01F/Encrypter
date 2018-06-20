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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) // Encrypter
        {
            if (textBox1.Text == textBox1.Text + ".cry")
            {
                MessageBox.Show("You can't encrypt a file that has already been encrypted.", "Encrypter", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string inputFile = textBox1.Text;
                string outputFile = "Encrypted" + ".cry";
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
                File.Delete(textBox1.Text);
                MessageBox.Show("Your file is now encrypted. Note: hide you password somewhere safe", "Encrypter", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void button3_Click(object sender, EventArgs e) // Decrypter
        {
            string inputFile = textBox1.Text;
            string outputFile = "Decrypted" + textBox4.Text;
            string password = textBox2.Text;
            if (File.Exists(textBox1.Text))
            {
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
                    File.Delete(textBox1.Text);
                    MessageBox.Show("Your file has been decrypted.", "Encrypter", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("The password is not right for that file.", "Encrypter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("File " + textBox1.Text + " does not exists", "Encrypter", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
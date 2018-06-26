using System;
using System.IO;
using System.Windows.Forms;

namespace Encrypter
{
    public partial class Form_EULA : Form
    {
        public Form_EULA()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            string username = Environment.UserName;
            StreamWriter f2 = new StreamWriter("C:\\Users\\" + username + "\\AppData\\accept.txt");
            f2.WriteLine("accepted");
            f2.Close();
            Close();
        }
    }
}

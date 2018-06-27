using System;
using System.Windows.Forms;

namespace Encrypter
{
    public partial class Form_Information : Form
    {
        public Form_Information()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

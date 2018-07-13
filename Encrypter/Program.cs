using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Encrypter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Updates();
            Application.Run(new Form_Application());
        }

        public static void Updates()
        {
            // Update code coming soon.
        }
    }
}

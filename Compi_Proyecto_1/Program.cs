using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Compi_Proyecto_1
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            create_folder(Application.StartupPath + "\\Images");
            create_folder(Application.StartupPath + "\\Reports");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static int last_number = -1;

        static private void create_folder(string carpeta)
        {
            if (!(Directory.Exists(carpeta)))
            {
                Directory.CreateDirectory(carpeta);
            }
        }
    }
}

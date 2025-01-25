using System;
using System.Windows.Forms;

namespace CriptografiaApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());  // Inicializa o formulário
        }
    }
}

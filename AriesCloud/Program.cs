using AriesCloud.Forms;
using System;
using System.Windows.Forms;

namespace AriesCloud
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AuthorizationForm authorizationForm = new AuthorizationForm();
            Application.Run(new RegistrationForm());



            Application.Run(new MainForm());
        }
    }
}

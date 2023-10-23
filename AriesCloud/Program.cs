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

            while (true)
            {
                using (AuthorizationForm authorizationForm = new AuthorizationForm())
                {
                    Application.Run(authorizationForm);

                    if (authorizationForm.DialogResult != DialogResult.OK)
                    {
                        return;
                    }
                }

                using (MainForm mainForm = new MainForm())
                {
                    Application.Run(mainForm);

                    if (mainForm.DialogResult != DialogResult.OK)
                    {
                        return;
                    }
                }
            }
        }
    }
}

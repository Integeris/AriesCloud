using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AriesCloud.Classes
{
    /// <summary>
    /// Менеджер сообщений.
    /// </summary>
    public static class InfoViewer
    {
        /// <summary>
        /// Сообщение об ошибке.
        /// </summary>
        /// <param name="text">Текст ошибки.</param>
        public static void ShowError(string text)
        {
            MessageBox.Show(text, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Сообщение об ошибке.
        /// </summary>
        /// <param name="ex">Ошибка.</param>
        public static void ShowError(Exception ex)
        {
            List<string> exeptions = new List<string>();

            while (ex != null)
            {
                exeptions.Add(ex.Message);
                ex = ex.InnerException;
            }

            ShowError(String.Join("\n", exeptions));
        }

        /// <summary>
        /// Информативное сообщение.
        /// </summary>
        /// <param name="text">Информативный текст.</param>
        public static void ShowInformation(string text)
        {
            MessageBox.Show(text, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

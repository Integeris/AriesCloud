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
            Exception exception = ex;
            List<string> exceptions = new List<string>();

            for (int i = 0; exception != null; i++)
            {
                exceptions.Add($"{i}) {exception.Message};");
                exception = exception.InnerException;
            }

            ShowError(String.Join("\n", exceptions));
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

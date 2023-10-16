using System.Windows.Forms;

namespace AriesCloud.CLasses
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
        /// Информативное сообщение.
        /// </summary>
        /// <param name="text">Информативный текст.</param>
        public static void ShowInformation(string text)
        {
            MessageBox.Show(text, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

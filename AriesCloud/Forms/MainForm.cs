using System;
using System.Windows.Forms;

namespace AriesCloud.Forms
{
    /// <summary>
    /// Главная форма.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Создание главной формы.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            KeyPreview = true;
        }

        /// <summary>
        /// Обработчик кнопки "Загрузить".
        /// </summary>
        /// <param name="sender">Кнопка "Загрузить".</param>
        /// <param name="e">Данные события.</param>
        private void UploadToolStripMenuItemOnClick(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Обработчик кнопки "Скачать".
        /// </summary>
        /// <param name="sender">Кнопка "Скачать".</param>
        /// <param name="e">Данные события.</param>
        private void DownloadToolStripMenuItemOnClick(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Обработчик кнопки "Переименовать".
        /// </summary>
        /// <param name="sender">Кнопка "Переименовать".</param>
        /// <param name="e">Данные события.</param>
        private void RenameToolStripMenuItemOnClick(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Обработчик кнопки "Обновить".
        /// </summary>
        /// <param name="sender">Кнопка "Обновить".</param>
        /// <param name="e">Данные события.</param>
        private void UpdateToolStripMenuItemOnClick(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Обработчик кнопки "Удалить".
        /// </summary>
        /// <param name="sender">Кнопка "Удалить".</param>
        /// <param name="e">Данные события.</param>
        private void RemoveToolStripMenuItemOnClick(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Обработчик кнопки "Выход".
        /// </summary>
        /// <param name="sender">Кнопка "Выход".</param>
        /// <param name="e">Данные события.</param>
        private void ExitToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Обработчик кнопки "Настройки".
        /// </summary>
        /// <param name="sender">Кнопка "Настройки".</param>
        /// <param name="e">Данные события.</param>
        private void SettingsToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            using (SettingsForm settingsForm = new SettingsForm())
            {
                settingsForm.ShowDialog();
            }
        }

        /// <summary>
        /// Обработчик кнопки "О программе".
        /// </summary>
        /// <param name="sender">Кнопка "О программе".</param>
        /// <param name="e">Данные события.</param>
        private void AboutToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            using (AboutForm aboutForm = new AboutForm())
            {
                aboutForm.ShowDialog();
            }
        }
    }
}

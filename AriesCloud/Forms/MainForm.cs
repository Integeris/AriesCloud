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

            // TODO: Считать настройки из файла конфигурации.

            UpdateFiles();
        }

        /// <summary>
        /// Обработчик кнопки "Создать папку".
        /// </summary>
        /// <param name="sender">Кнопка "Создать папку".</param>
        /// <param name="e">Данные события.</param>
        private void CreateDirectoryToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            CreateDirectory();
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
            DownloadFiles();
        }

        /// <summary>
        /// Обработчик кнопки "Переименовать".
        /// </summary>
        /// <param name="sender">Кнопка "Переименовать".</param>
        /// <param name="e">Данные события.</param>
        private void RenameToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            RenameFile();
        }

        /// <summary>
        /// Обработчик кнопки "Обновить".
        /// </summary>
        /// <param name="sender">Кнопка "Обновить".</param>
        /// <param name="e">Данные события.</param>
        private void UpdateToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            UpdateFiles();
        }

        /// <summary>
        /// Обработчик кнопки "Удалить".
        /// </summary>
        /// <param name="sender">Кнопка "Удалить".</param>
        /// <param name="e">Данные события.</param>
        private void RemoveToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            DeleteFiles();
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

        /// <summary>
        /// Обработчик кнопки "Создать папку".
        /// </summary>
        /// <param name="sender">Кнопка "Создать папку".</param>
        /// <param name="e">Данные события.</param>
        private void CreateDirectoryContextToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            CreateDirectory();
        }

        /// <summary>
        /// Обработчик кнопки "Скачать".
        /// </summary>
        /// <param name="sender">Кнопка "Скачать".</param>
        /// <param name="e">Данные события.</param>
        private void DownloadContextToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            DownloadFiles();
        }

        /// <summary>
        /// Обработчик кнопки "Переименовать".
        /// </summary>
        /// <param name="sender">Кнопка "Переименовать".</param>
        /// <param name="e">Данные события.</param>
        private void RenameContextToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            RenameFile();
        }

        /// <summary>
        /// Обработчик кнопки "Удалить".
        /// </summary>
        /// <param name="sender">Кнопка "Удалить".</param>
        /// <param name="e">Данные события.</param>
        private void RemoveContextToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            DeleteFiles();
        }

        private void CreateDirectory()
        {
            // TODO: Добавить логику добавления папки.
        }

        /// <summary>
        /// Обновление списка файлов.
        /// </summary>
        private void UpdateFiles()
        {
            // TODO: Добавить обновление списка файлов.
        }

        /// <summary>
        /// Скачивание файлов.
        /// </summary>
        private void DownloadFiles()
        {
            // TODO: Добавить логику скачивания файлов.ы
        }

        /// <summary>
        /// Переименование файла.
        /// </summary>
        private void RenameFile()
        {
            // TODO: Добавить логику переименования файла.
        }
        
        /// <summary>
        /// Удаление файлов.
        /// </summary>
        private void DeleteFiles()
        {
            // TODO: Добавить логику удаления файлов.
        }
    }
}

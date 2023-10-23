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

        private void ExitToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

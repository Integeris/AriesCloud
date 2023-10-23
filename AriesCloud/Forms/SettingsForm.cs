using AriesCloud.Classes;
using System;
using System.IO;
using System.Windows.Forms;

namespace AriesCloud.Forms
{
    /// <summary>
    /// Форма настроек.
    /// </summary>
    public partial class SettingsForm : Form
    {
        /// <summary>
        /// Создание формы настроек.
        /// </summary>
        public SettingsForm()
        {
            InitializeComponent();

            keyPathTextBox.Text = UserData.KeyPath;
        }

        /// <summary>
        /// Обработчик кнопки "Применить".
        /// </summary>
        /// <param name="sender">Кнопка "Применить".</param>
        /// <param name="e">Данные события.</param>
        private void ApplyButtonOnClick(object sender, EventArgs e)
        {
            // TODO: Сделать провеку всех полей.

            UserData.KeyPath = keyPathTextBox.Text;
            Close();
        }

        /// <summary>
        /// Обработчик кнопки "Сгенерировать новый".
        /// </summary>
        /// <param name="sender">Кнопка "Сгенерировать новый".</param>
        /// <param name="e">Данные события.</param>
        private void GenerateKeyButtonOnClick(object sender, EventArgs e)
        {
            string savePath;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Файл сохранения ключа";
                saveFileDialog.Filter = "Файл ключа (*.key)|*.key";

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                savePath = saveFileDialog.FileName;
            }

            try
            {
                using (FileStream fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write))
                {
                    using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                    {
                        byte[] buffer = new byte[128];
                        Random random = new Random();

                        random.NextBytes(buffer);

                        foreach (byte item in buffer)
                        {
                            binaryWriter.Write(item);
                        }
                    }
                }

                keyPathTextBox.Text = savePath;
            }
            catch (Exception ex)
            {
                InfoViewer.ShowError(ex.Message);
                return;
            }

            InfoViewer.ShowInformation("Новый ключ сгенерирован.");
        }

        /// <summary>
        /// Обработчик кнопки "...".
        /// </summary>
        /// <param name="sender">Кнопка "...".</param>
        /// <param name="e">Данные события.</param>
        private void ChangeKeyButtonOnClick(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Выберите файл ключа";
                openFileDialog.Filter = "Файл ключа (*.key)|*.key";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    keyPathTextBox.Text = openFileDialog.FileName;
                }
            }
        }
    }
}

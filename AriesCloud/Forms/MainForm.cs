using AriesCloud.Classes;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace AriesCloud.Forms
{
    /// <summary>
    /// Главная форма.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Файловый менеджер.
        /// </summary>
        private readonly FileManager fileManager;

        /// <summary>
        /// Создание главной формы.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            typeof(ListView)
                .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(mainListView, true);

            KeyPreview = true;

            try
            {
                fileManager = new FileManager();
            }
            catch (Exception ex)
            {
                InfoViewer.ShowError(ex);
            }
            
            fileManager.ChangeDirectory += FileManagerOnChangeDirectory;
            fileManager.UpdateItems += UpdateItems;
            pathTextBox.Text = fileManager.CurrentDirectory;
            UpdateItems(fileManager);
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
        /// Обработчик кнопки "Загрузить файл".
        /// </summary>
        /// <param name="sender">Кнопка "Загрузить файл".</param>
        /// <param name="e">Данные события.</param>
        private void UploadFileToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Выберите файлы для загрузки.";
                    openFileDialog.Multiselect = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        foreach (string filePath in openFileDialog.FileNames)
                        {
                            fileManager.UploadFile(filePath);
                        }

                        InfoViewer.ShowInformation("Загрузка произошла успешно.");
                    }
                }
            }
            catch (Exception ex)
            {
                InfoViewer.ShowError(ex);
            }
        }

        /// <summary>
        /// Обработчик кнопки "Загрузить папку".
        /// </summary>
        /// <param name="sender">Кнопка "Загрузить папку".</param>
        /// <param name="e">Данные события.</param>
        private void UploadDirectoryToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            try
            {
                using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                {
                    folderBrowserDialog.Description = "Выберите папку для загрузки.";

                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        fileManager.UploadDirectory(folderBrowserDialog.SelectedPath);
                        InfoViewer.ShowInformation("Загрузка произошла успешно.");
                    }
                }
            }
            catch (Exception ex)
            {
                InfoViewer.ShowError(ex);
            }
        }

        /// <summary>
        /// Обработчик кнопки "Скачать".
        /// </summary>
        /// <param name="sender">Кнопка "Скачать".</param>
        /// <param name="e">Данные события.</param>
        private void DownloadToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            DownloadItems();
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
            try
            {
                fileManager.GetDirectoryItems();
                UpdateItems(fileManager);
            }
            catch (Exception ex)
            {
                InfoViewer.ShowError(ex);
            }
        }

        /// <summary>
        /// Обработчик кнопки "Переместить".
        /// </summary>
        /// <param name="sender">Кнопка "Переместить".</param>
        /// <param name="e">Данные события.</param>
        private void MoveToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            MoveItems();
        }

        /// <summary>
        /// Обработчик кнопки "Удалить".
        /// </summary>
        /// <param name="sender">Кнопка "Удалить".</param>
        /// <param name="e">Данные события.</param>
        private void RemoveToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            DeleteItems();
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
            DownloadItems();
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
        /// Обработчик кнопки "Переместить".
        /// </summary>
        /// <param name="sender">Кнопка "Переместить".</param>
        /// <param name="e">Данные события.</param>
        private void MoveContextToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            MoveItems();
        }

        /// <summary>
        /// Обработчик кнопки "Удалить".
        /// </summary>
        /// <param name="sender">Кнопка "Удалить".</param>
        /// <param name="e">Данные события.</param>
        private void RemoveContextToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            DeleteItems();
        }

        /// <summary>
        /// Обработчик кнопок файлов и папок.
        /// </summary>
        /// <param name="sender">Главный ListView.</param>
        /// <param name="e">Данные события.</param>
        private void MainListViewOnDoubleClick(object sender, EventArgs e)
        {
            try
            {
                DirectoryItem item = (DirectoryItem)mainListView.SelectedItems[0].Tag;

                if (item is Directory)
                {
                    fileManager.OpenDirectory((Directory)item);
                }
            }
            catch (Exception ex)
            {
                InfoViewer.ShowError(ex);
            }
        }

        /// <summary>
        /// Обработчик изменения имени файла или папки.
        /// </summary>
        /// <param name="sender">Главный ListView.</param>
        /// <param name="e">Данные события.</param>
        private void MainListViewOnAfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                return;
            }

            try
            {
                DirectoryItem item = (DirectoryItem)mainListView.Items[e.Item].Tag;

                if (item is Directory)
                {
                    fileManager.RenameDirectory((Directory)item, e.Label);
                }
                else
                {
                    fileManager.RenameFile((File)item, e.Label);
                }
            }
            catch (Exception ex)
            {
                InfoViewer.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Обработчик кнопки "Вверх".
        /// </summary>
        /// <param name="sender">Кнопка "Вверх".</param>
        /// <param name="e">Данные события.</param>
        private void UpButtonOnClick(object sender, EventArgs e)
        {
            try
            {
                fileManager.CloseDirectory();
            }
            catch (Exception ex)
            {
                InfoViewer.ShowError(ex);
            }
        }

        /// <summary>
        /// Создать папку.
        /// </summary>
        private void CreateDirectory()
        {
            try
            {
                Directory directory = fileManager.AddDirectory();
                mainListView.Items[directory.Name].BeginEdit();
            }
            catch (Exception ex)
            {
                InfoViewer.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Скачивание файлов.
        /// </summary>
        private void DownloadItems()
        {
            if (mainListView.SelectedItems.Count == 0)
            {
                InfoViewer.ShowError("Выберите хотябы один элемент.");
                return;
            }

            try
            {
                using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                {
                    folderBrowserDialog.Description = "Укажите папку для сохранения.";

                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        foreach (ListViewItem item in mainListView.SelectedItems)
                        {
                            DirectoryItem directoryItem = (DirectoryItem)item.Tag;

                            if (directoryItem is File)
                            {
                                fileManager.DownloadFile((File)directoryItem, $"{folderBrowserDialog.SelectedPath}\\{directoryItem.Name}");
                            }
                            else
                            {
                                fileManager.DownloadDirectory((Directory)directoryItem, folderBrowserDialog.SelectedPath);
                            }
                        }

                        InfoViewer.ShowInformation("Скачивание произошло успешно.");
                    }
                }
            }
            catch (Exception ex)
            {
                InfoViewer.ShowError(ex);
            }
        }

        /// <summary>
        /// Переименование файла.
        /// </summary>
        private void RenameFile()
        {
            if (mainListView.SelectedItems.Count > 0)
            {
                mainListView.SelectedItems[0].BeginEdit();
            }
        }

        /// <summary>
        /// Перемещение файлов и папок.
        /// </summary>
        private void MoveItems()
        {
            try
            {
                using (OpenServerFolderDialog openServerFolderDialog = new OpenServerFolderDialog())
                {
                    if (openServerFolderDialog.ShowDialog() == DialogResult.OK)
                    {
                        foreach (ListViewItem item in mainListView.SelectedItems)
                        {
                            DirectoryItem directoryItem = ((DirectoryItem)item.Tag);

                            if (directoryItem is Directory)
                            {
                                fileManager.MoveDirectory((Directory)directoryItem, openServerFolderDialog.Path);
                            }
                            else
                            {
                                fileManager.MoveFile((File)directoryItem, openServerFolderDialog.Path);
                            }
                        }

                        InfoViewer.ShowInformation("Элементы успешно перенесены.");
                    }
                }
            }
            catch (Exception ex)
            {
                InfoViewer.ShowError(ex);
            }
        }

        /// <summary>
        /// Удаление файлов и папок.
        /// </summary>
        private void DeleteItems()
        {
            try
            {
                foreach (ListViewItem item in mainListView.SelectedItems)
                {
                    DirectoryItem directoryItem = (DirectoryItem)item.Tag;

                    if (directoryItem is File)
                    {
                        fileManager.RemoveFile((File)directoryItem);
                    }
                    else
                    {
                        fileManager.RemoveDirectory((Directory)directoryItem);
                    }
                }
            }
            catch (Exception ex) 
            { 
                InfoViewer.ShowError(ex); 
            }
        }

        /// <summary>
        /// Изменение открытой папки.
        /// </summary>
        /// <param name="sender">Файловый менеджер вызвавший событие.</param>
        private void FileManagerOnChangeDirectory(FileManager sender)
        {
            pathTextBox.Text = fileManager.CurrentDirectory;
        }

        /// <summary>
        /// Обновление списка файлов.
        /// </summary>
        private void UpdateItems(FileManager fileManager)
        {
            mainListView.BeginUpdate();

            try
            {
                mainListView.Items.Clear();

                foreach (Directory directory in fileManager.Directories)
                {
                    ListViewItem listViewItem = new ListViewItem(directory.Name, 1)
                    {
                        Name = directory.Name,
                        Tag = directory
                    };

                    mainListView.Items.Add(listViewItem);
                }

                foreach (File file in fileManager.Files)
                {
                    ListViewItem listViewItem = new ListViewItem(file.Name, 0)
                    {
                        Name = file.Name,
                        Tag = file
                    };

                    mainListView.Items.Add(listViewItem);
                }
            }
            catch (Exception ex)
            {
                InfoViewer.ShowError(ex.Message);
            }
            finally
            {
                mainListView.EndUpdate();
            }
        }
    }
}

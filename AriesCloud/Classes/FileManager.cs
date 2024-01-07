using System;
using System.Collections.Generic;
using System.IO;

namespace AriesCloud.Classes
{
    /// <summary>
    /// Менеджер удалённого хранилища.
    /// </summary>
    public class FileManager
    {
        /// <summary>
        /// Менеджер пути.
        /// </summary>
        private readonly PathManager pathManager;

        /// <summary>
        /// Папки.
        /// </summary>
        private readonly List<Directory> directories;

        /// <summary>
        /// Файлы.
        /// </summary>
        private readonly List<File> files;

        /// <summary>
        /// Изменение папки.
        /// </summary>
        /// <param name="sender">Файловый менеджер вызвавший событие.</param>
        public delegate void ChangeDirectoryHandler(FileManager sender);

        /// <summary>
        /// Обновление списка файлов и папок в директории.
        /// </summary>
        /// <param name="sender">Файловый менеджер вызвавший событие.</param>
        public delegate void UpdateItemsHandler(FileManager sender);

        /// <summary>
        /// Событие изменения папки.
        /// </summary>
        public event ChangeDirectoryHandler ChangeDirectory;

        /// <summary>
        /// Событие обновления файлов и папок в текущей директории.
        /// </summary>
        public event UpdateItemsHandler UpdateItems;

        /// <summary>
        /// Текущая папка.
        /// </summary>
        public string CurrentDirectory
        {
            get => pathManager.ToString();
        }

        /// <summary>
        /// Папки.
        /// </summary>
        public List<Directory> Directories
        {
            get => directories;
        }

        /// <summary>
        /// Файлы.
        /// </summary>
        public List<File> Files
        {
            get => files;
        }

        /// <summary>
        /// Создание менеджера удалённого хранилища.
        /// </summary>
        public FileManager()
        {
            pathManager = new PathManager();
            directories = new List<Directory>();
            files = new List<File>();

            GetDirectoryItems();
        }

        /// <summary>
        /// Открытие папки.
        /// </summary>
        /// <param name="directory">Папка.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        public void OpenDirectory(Directory directory)
        {
            if (!directories.Contains(directory))
            {
                throw new KeyNotFoundException("Папка не найдена.");
            }

            pathManager.Add(directory.Name);
            GetDirectoryItems();
            ChangeDirectory.Invoke(this);
            UpdateItems.Invoke(this);
        }

        /// <summary>
        /// Закрыть текущую папку.
        /// </summary>
        public void CloseDirectory()
        {
            pathManager.RemoveAt(-1);
            GetDirectoryItems();
            ChangeDirectory.Invoke(this);
            UpdateItems.Invoke(this);
        }

        /// <summary>
        /// Загрузить файл.
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        /// <exception cref="Exception"></exception>
        public void UploadFile(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
            {
                throw new Exception("Указанного файла не существует.");
            }

            Core.UploadFile(fileInfo, pathManager.ToString(), UserData.Key);
            files.Add(new File(fileInfo.Name));
            UpdateItems.Invoke(this);
        }

        /// <summary>
        /// Скачать файл.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="savePath">Полный путь сохранения для файла.</param>
        public void DownloadFile(File file, string savePath)
        {
            Core.DownloadFile(pathManager.ToString(), file.Name, savePath, UserData.Key);
        }

        /// <summary>
        /// Переименовать файл.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="newFileName">Новое имя файла.</param>
        public void RenameFile(File file, string newFileName)
        {
            try
            {
                Core.MoveDirecoryItem($"{pathManager}/{file.Name}", $"{pathManager}/{newFileName}");
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось переименовать файл.", ex);
            }

            file.Name = newFileName;
            UpdateItems.Invoke(this);
        }

        /// <summary>
        /// Удалить файл.
        /// </summary>
        /// <param name="file">Файл.</param>
        public void RemoveFile(File file)
        {
            try
            {
                Core.RemoveDirecoryItem(pathManager.ToString(), file.Name);
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось удалить файл.", ex);
            }

            files.Remove(file);
            UpdateItems.Invoke(this);
        }

        /// <summary>
        /// Добавить папку.
        /// </summary>
        /// <returns>Новая папка.</returns>
        public Directory AddDirectory()
        {
            string directoryName = String.Empty;

            for (int i = 1; i < Int32.MaxValue; i++)
            {
                directoryName = $"Новая папка {i}";

                if (directories.FindIndex(dir => dir.Name == directoryName) == -1)
                {
                    break;
                }
            }

            try
            {
                Core.CreateDirectory(pathManager.ToString(), directoryName);
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось создать папку.", ex);
            }

            Directory directory = new Directory(directoryName);

            directories.Add(directory);
            UpdateItems.Invoke(this);

            return directory;
        }

        /// <summary>
        /// Загрузить папку.
        /// </summary>
        /// <param name="directoryPath">Полный путь к загружаемой папке.</param>
        public void UploadDirectory(string directoryPath)
        {
            try
            {
                UploadDirectory(directoryPath, pathManager.ToString());
                directories.Add(new Directory(Path.GetFileName(directoryPath)));
                UpdateItems.Invoke(this);
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось загрузить папку.", ex);
            }
        }

        /// <summary>
        /// Скачать папку.
        /// </summary>
        /// <param name="directory">Папка.</param>
        /// <param name="saveDirectory">Путь к папке сохранения.</param>
        public void DownloadDirectory(Directory directory, string saveDirectory)
        {
            DownloadDirectory(directory.Name, saveDirectory);
        }

        /// <summary>
        /// Переименовать папку.
        /// </summary>
        /// <param name="directory">Папка.</param>
        /// <param name="directoryNewName">Новое имя папки.</param>
        public void RenameDirectory(Directory directory, string directoryNewName)
        {
            try
            {
                Core.MoveDirecoryItem($"{pathManager}/{directory.Name}", $"{pathManager}/{directoryNewName}");
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось переименовать папку.", ex);
            }

            directory.Name = directoryNewName;
            UpdateItems.Invoke(this);
        }

        /// <summary>
        /// Удаление папки.
        /// </summary>
        /// <param name="directory">Папка.</param>
        public void RemoveDirectory(Directory directory)
        {
            Core.RemoveDirecoryItem(pathManager.ToString(), directory.Name);
            directories.Remove(directory);
            UpdateItems.Invoke(this);
        }

        /// <summary>
        /// Получение папок и файлов текущей папки.
        /// </summary>
        public void GetDirectoryItems()
        {
            directories.Clear();
            files.Clear();

            foreach (DirectoryItem item in Core.GetDirecoryItems(pathManager.ToString()))
            {
                if (item is File)
                {
                    files.Add((File)item);
                }
                else
                {
                    directories.Add((Directory)item);
                }
            }
        }

        /// <summary>
        /// Скачать папку.
        /// </summary>
        /// <param name="directoryName">Имя папки.</param>
        /// <param name="saveDirectory">Путь сохранения папки.</param>
        private void DownloadDirectory(string directoryName, string saveDirectory)
        {
            string serverPath = $"{pathManager}/{directoryName}";
            string savePath = $"{saveDirectory}\\{directoryName}";
            System.IO.Directory.CreateDirectory(savePath);

            foreach (DirectoryItem item in Core.GetDirecoryItems(serverPath))
            {
                if (item is File)
                {
                    Core.DownloadFile(serverPath, item.Name, $"{savePath}\\{item.Name}", UserData.Key);
                }
                else
                {
                    DownloadDirectory($"{directoryName}/{item.Name}", saveDirectory);
                }
            }
        }

        /// <summary>
        /// Загрузка директории.
        /// </summary>
        /// <param name="directoryPath">Путь к папке.</param>
        /// <param name="serverPath">Путь к папке на сервере в которую надо загрузить папку.</param>
        private void UploadDirectory(string directoryPath, string serverPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            string serverDirectoryPath = $"{serverPath}/{directoryInfo.Name}";

            Core.CreateDirectory(serverPath, directoryInfo.Name);
            
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                Core.UploadFile(file, serverDirectoryPath, UserData.Key);
            }

            foreach (DirectoryInfo subDirectory in directoryInfo.GetDirectories())
            {
                UploadDirectory(subDirectory.FullName, serverDirectoryPath);
            }
        }
    }
}

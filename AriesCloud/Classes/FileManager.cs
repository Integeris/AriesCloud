﻿using System;
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

            try
            {
                GetDirectoryItems();
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось получить элементы папки.", ex);
            }
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

            try
            {
                pathManager.Add(directory.Name);
                GetDirectoryItems();
                ChangeDirectory.Invoke(this);
                UpdateItems.Invoke(this);
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось открыть папку.", ex);
            }
        }

        /// <summary>
        /// Закрыть текущую папку.
        /// </summary>
        public void CloseDirectory()
        {
            try
            {
                pathManager.RemoveAt(-1);
                GetDirectoryItems();
                ChangeDirectory.Invoke(this);
                UpdateItems.Invoke(this);
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось перейти на папку выше.", ex);
            }
        }

        /// <summary>
        /// Загрузить файл.
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        /// <exception cref="Exception"></exception>
        public void UploadFile(string filePath)
        {
            KeyCheck();

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
            KeyCheck();

            try
            {
                Core.DownloadFile(pathManager.ToString(), file.Name, savePath, UserData.Key);
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось скачать файл.", ex);
            }
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
        /// Перенести файл в другую папку.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="newDirectoryPath">Новая папка.</param>
        public void MoveFile(File file, string newDirectoryPath)
        {
            try
            {
                if (pathManager.ToString() == newDirectoryPath)
                {
                    throw new Exception("Текущий путь к файлу совпадает с конечным.");
                }

                Core.MoveDirecoryItem($"{pathManager}/{file.Name}", $"{newDirectoryPath}/{file.Name}");
                files.Remove(file);
                UpdateItems.Invoke(this);
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось переместить файл.", ex);
            }
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
            KeyCheck();

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
            KeyCheck();

            try
            {
                DownloadDirectory(directory.Name, saveDirectory);
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось скачать папку.", ex);
            }
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
        /// Перенести папку в другую папку.
        /// </summary>
        /// <param name="directory">Папка.</param>
        /// <param name="newDirectoryPath">Новая папка.</param>
        public void MoveDirectory(Directory directory, string newDirectoryPath)
        {
            try
            {
                string directoryFullName = $"{pathManager}/{directory.Name}";
                string directoryNewFullName = $"{newDirectoryPath}/{directory.Name}";

                if (pathManager.ToString() == newDirectoryPath)
                {
                    throw new Exception("Текущий путь к папке совпадает с конечным.");
                }
                else if (directoryNewFullName.StartsWith(directoryFullName))
                {
                    throw new Exception("Невозможно перенести папку в саму себя.");
                }

                Core.MoveDirecoryItem(directoryFullName, directoryNewFullName);
                directories.Remove(directory);
                UpdateItems.Invoke(this);
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось переместить папку.", ex);
            }
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
            try
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
            catch (Exception ex)
            {
                throw new Exception("Не удалось получить элементы папки.", ex);
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

        /// <summary>
        /// Проверка наличия ключа.
        /// </summary>
        private static void KeyCheck()
        {
            if (!UserData.KeyLoad)
            {
                throw new Exception("Не указан ключ шифрования.");
            }
        }
    }
}

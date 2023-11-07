using System;
using System.Collections.Generic;
using System.Net.Http;

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

        // <summary>
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
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="Exception"></exception>
        public void OpenDirectory(Directory directory)
        {
            if (!directories.Contains(directory))
            {
                throw new KeyNotFoundException("Папка не найдена.");
            }

            pathManager.Add(directory.Name);
            GetDirectoryItems();
        }

        /// <summary>
        /// Закрыть текущую папку.
        /// </summary>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="Exception"></exception>
        public void CloseDirectory()
        {
            pathManager.RemoveAt(-1);
            GetDirectoryItems();
        }

        /// <summary>
        /// Добавить файл в текущую папку.
        /// </summary>
        /// <param name="fullFileName">Полный путь к файлу.</param>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="Exception"></exception>
        public void AddFile(string fullFileName)
        {

        }

        /// <summary>
        /// Скачать файл.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="fullFileName">Полный путь сохранения для файла.</param>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="Exception"></exception>
        public void DownloadFile(File file, string fullFileName)
        {

        }

        /// <summary>
        /// Переименовать файл.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="Exception"></exception>
        public void RenameFile(File file)
        {

        }

        /// <summary>
        /// Удалить файл.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="Exception"></exception>
        public void RemoveFile(File file)
        {

        }

        /// <summary>
        /// Добавить папку.
        /// </summary>
        /// <param name="fullDirectoryName">Полный путь к папке.</param>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="Exception"></exception>
        public void AddDirectory(string fullDirectoryName)
        {

        }

        /// <summary>
        /// Скачать папку.
        /// </summary>
        /// <param name="directory">Папка</param>
        /// <param name="fullDirectoryName">Путь сохранения папки.</param>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="Exception"></exception>
        public void DownloadDirectory(Directory directory, string fullDirectoryName)
        {

        }

        /// <summary>
        /// Переименовать папку.
        /// </summary>
        /// <param name="directory">Папка.</param>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="Exception"></exception>
        public void RenameDirectory(Directory directory)
        {

        }

        /// <summary>
        /// Удаление папки.
        /// </summary>
        /// <param name="directory">Папка.ы</param>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="Exception"></exception>
        public void RemoveDirectory(Directory directory)
        {

        }

        /// <summary>
        /// Получение папок и файлов текущей папки.
        /// </summary>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="Exception"></exception>
        public void GetDirectoryItems()
        {
            directories.Clear();
            files.Clear();

            foreach (DirectoryItem item in Core.GetDirecoryItems(""))
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
    }
}

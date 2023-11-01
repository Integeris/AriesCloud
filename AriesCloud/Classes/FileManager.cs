using System.Collections.Generic;

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
        }

        /// <summary>
        /// Закрыть текущую папку.
        /// </summary>
        public void CloseDirectory()
        {
            pathManager.RemoveAt(-1);
        }

        /// <summary>
        /// Добавить файл в текущую папку.
        /// </summary>
        /// <param name="fullFileName">Полный путь к файлу.</param>
        public void AddFile(string fullFileName)
        {

        }

        /// <summary>
        /// Скачать файл.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="fullFileName">Полный путь сохранения для файла.</param>
        public void DownloadFile(File file, string fullFileName)
        {

        }

        /// <summary>
        /// Переименовать файл.
        /// </summary>
        /// <param name="file">Файл.</param>
        public void RenameFile(File file)
        {

        }

        /// <summary>
        /// Удалить файл.
        /// </summary>
        /// <param name="file">Файл.</param>
        public void RemoveFile(File file)
        {

        }

        /// <summary>
        /// Добавить папку.
        /// </summary>
        /// <param name="fullDirectoryName">Полный путь к папке.</param>
        public void AddDirectory(string fullDirectoryName)
        {

        }

        /// <summary>
        /// Скачать папку.
        /// </summary>
        /// <param name="directory">Папка</param>
        /// <param name="fullDirectoryName">Путь сохранения папки.</param>
        public void DownloadDirectory(Directory directory, string fullDirectoryName)
        {

        }

        /// <summary>
        /// Переименовать папку.
        /// </summary>
        /// <param name="directory">Папка.</param>
        public void RenameDirectory(Directory directory)
        {

        }

        /// <summary>
        /// Удаление папки.
        /// </summary>
        /// <param name="directory">Папка.ы</param>
        public void RemoveDirectory(Directory directory)
        {

        }

        /// <summary>
        /// Получение папок и файлов текущей папки.
        /// </summary>
        private void GetDirectoryItems()
        {
            directories.Clear();
            files.Clear();
        }
    }
}

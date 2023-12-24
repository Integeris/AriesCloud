namespace AriesCloud.Classes
{
    /// <summary>
    /// Файл.
    /// </summary>
    public class File : DirectoryItem
    {
        /// <summary>
        /// Создать файл.
        /// </summary>
        public File() { }

        /// <summary>
        /// Создать файл.
        /// </summary>
        /// <param name="name">Имя файла.</param>
        public File(string name)
        {
            Name = name;
        }
    }
}

namespace AriesCloud.Classes
{
    /// <summary>
    /// Папка.
    /// </summary>
    public class Directory : DirectoryItem
    {
        /// <summary>
        /// Создать папку.
        /// </summary>
        public Directory() { }

        /// <summary>
        /// Создать папку.
        /// </summary>
        /// <param name="name">Имя папки.</param>
        public Directory(string name)
        {
            Name = name;
        }
    }
}

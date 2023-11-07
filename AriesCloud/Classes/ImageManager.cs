using AriesCloud.Properties;
using System.Drawing;

namespace AriesCloud.Classes
{
    /// <summary>
    /// Менеджер иконок файлов и папок.
    /// </summary>
    public static class ImageManager
    {
        /// <summary>
        /// Изображение файла.
        /// </summary>
        public static Bitmap FileImage { get; private set; } = Resources.File;

        /// <summary>
        /// Изображение папки.
        /// </summary>
        public static Bitmap FolderImage { get; private set; } = Resources.Folder;
    }
}

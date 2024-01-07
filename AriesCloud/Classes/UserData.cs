using System;

namespace AriesCloud.Classes
{
    /// <summary>
    /// Данные пользователя.
    /// </summary>
    public static class UserData
    {
        /// <summary>
        /// Ключ.
        /// </summary>
        private static byte[] key = new byte[Scrambler.KeySize];

        /// <summary>
        /// Хеш.
        /// </summary>
        public static string Hash { get; set; }

        /// <summary>
        /// Загружен ли ключ.
        /// </summary>
        public static bool KeyLoad { get; set; }

        /// <summary>
        /// Путь к ключу.
        /// </summary>
        public static string KeyPath { get; set; }

        /// <summary>
        /// Ключ.
        /// </summary>
        public static byte[] Key
        {
            get
            {
                return key;
            }

            set
            {
                key = value;
                KeyLoad = true;
            }
        }
    }
}

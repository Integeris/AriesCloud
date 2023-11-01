using System;
using System.Collections.Generic;

namespace AriesCloud.Classes
{
    /// <summary>
    /// Менеджер пути.
    /// </summary>
    public class PathManager
    {
        /// <summary>
        /// Список папок в пути.
        /// </summary>
        private readonly List<string> path;

        /// <summary>
        /// Количество папок в пути.
        /// </summary>
        public int Count
        {
            get => path.Count;
        }

        /// <summary>
        /// Создание менеджера пути.
        /// </summary>
        public PathManager()
        {
            path = new List<string>();
        }

        /// <summary>
        /// Добавление папки в путь.
        /// </summary>
        /// <param name="directoryName">Имя папки.</param>
        public void Add(string directoryName)
        {
            path.Add(directoryName);
        }

        /// <summary>
        /// Вставить папку в путь.
        /// </summary>
        /// <param name="index">Индекс для вставки элемента.</param>
        /// <param name="directoryName">Название папки.</param>
        public void Insert(int index, string directoryName)
        {
            path.Insert(index, directoryName);
        }

        /// <summary>
        /// Удаление папки из пути.
        /// </summary>
        /// <param name="index">Индекс удаляемого элемента.</param>
        public void RemoveAt(int index)
        {
            if (index < 0)
            {
                index += path.Count;
            }

            path.RemoveAt(index);
        }

        /// <summary>
        /// Возврат пути в текстовом виде.
        /// </summary>
        /// <returns>Путь в текстовом виде.</returns>
        public override string ToString()
        {
            return String.Join("\\", path);
        }

        /// <summary>
        /// Получение папки по индексу.
        /// </summary>
        /// <param name="index">Индекс папки.</param>
        /// <returns>Папка по индексу.</returns>
        public string this[int index]
        {
            get => path[index];
            set => path[index] = value;
        }
    }
}

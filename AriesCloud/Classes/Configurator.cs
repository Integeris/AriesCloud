using System;
using System.IO;
using System.Threading;
using System.Xml;

namespace AriesCloud.Classes
{
    /// <summary>
    /// Менеджер кофигурации.
    /// </summary>
    public static class Configurator
    {
        /// <summary>
        /// Полное имя файла.
        /// </summary>
        private const string fileName = "config.xml";

        /// <summary>
        /// Загрузка данных пользователя.
        /// </summary>
        public static void Load()
        {
            XmlDocument document = new XmlDocument();

            try
            {
                document.Load(fileName);
            }
            catch (Exception)
            {
                CreateFile();
                document.Load(fileName);
            }

            XmlElement users = document.DocumentElement;

            foreach (XmlNode user in users)
            {
                string login = user.Attributes.GetNamedItem("Hash").Value;

                if (login == UserData.Hash)
                {
                    UserData.KeyPath = user.Attributes.GetNamedItem("KeyPath").Value;
                    return;
                }
            }

            XmlElement newUser = CreateUser(document, UserData.Hash);
            users.AppendChild(newUser);
            document.Save(fileName);
        }

        /// <summary>
        /// Сохранение данных пользователя.
        /// </summary>
        public static void Save()
        {
            XmlDocument document = new XmlDocument();

            try
            {
                document.Load(fileName);
            }
            catch (Exception)
            {
                CreateFile();
                document.Load(fileName);
            }

            document.Load(fileName);
            XmlElement users = document.DocumentElement;

            foreach (XmlNode user in users)
            {
                string login = user.Attributes.GetNamedItem("Hash").Value;

                if (login == UserData.Hash)
                {
                    user.Attributes.GetNamedItem("KeyPath").Value = UserData.KeyPath;
                    document.Save(fileName);
                    return;
                }
            }

            XmlElement newUser = CreateUser(document, UserData.Hash);
            newUser.Value = UserData.KeyPath;
            users.AppendChild(newUser);
            document.Save(fileName);
        }

        /// <summary>
        /// Создание нового пользователя в файле конфигурации.
        /// </summary>
        /// <param name="document">Объект документа.</param>
        /// <param name="hash">Хеш нового пользователя.</param>
        /// <returns>Новый пользователь.</returns>
        private static XmlElement CreateUser(XmlDocument document, string hash)
        {
            XmlElement newUser = document.CreateElement("User");
            XmlAttribute tmp = document.CreateAttribute("Hash");
            tmp.Value = hash;
            newUser.Attributes.Append(tmp);
            tmp = document.CreateAttribute("KeyPath");
            newUser.Attributes.Append(tmp);

            return newUser;
        }
        
        /// <summary>
        /// Создание файла конфигураций.
        /// </summary>
        private static void CreateFile()
        {
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write("""
                            <Users>
                            </Users>
                            """);
                }
            }
        }
    }
}

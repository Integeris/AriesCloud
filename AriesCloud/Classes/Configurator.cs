using System;
using System.IO;
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
                File.Create(fileName);
                document.Load(fileName);
            }

            XmlElement users = document.DocumentElement;

            foreach (XmlNode user in users)
            {
                string login = user.Attributes.GetNamedItem("Login").Value;

                if (login == UserData.Login)
                {
                    UserData.KeyPath = user.Attributes.GetNamedItem("KeyPath").Value;
                    return;
                }
            }

            XmlElement newUser = CreateUser(document, UserData.Login);
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
                File.Create(fileName);
                document.Load(fileName);
            }

            document.Load(fileName);
            XmlElement users = document.DocumentElement;

            foreach (XmlNode user in users)
            {
                string login = user.Attributes.GetNamedItem("Login").Value;

                if (login == UserData.Login)
                {
                    user.Attributes.GetNamedItem("KeyPath").Value = UserData.KeyPath;
                    return;
                }
            }

            XmlElement newUser = CreateUser(document, UserData.Login);
            newUser.Value = UserData.KeyPath;
            users.AppendChild(newUser);
            document.Save(fileName);
        }

        /// <summary>
        /// Создание нового пользователя в файле конфигурации.
        /// </summary>
        /// <param name="document">Объект документа.</param>
        /// <param name="login">Логин нового пользователя.</param>
        /// <returns>Новый пользователь.</returns>
        private static XmlElement CreateUser(XmlDocument document, string login)
        {
            XmlElement newUser = document.CreateElement("User");
            XmlAttribute tmp = document.CreateAttribute("Login");
            tmp.Value = login;
            newUser.Attributes.Append(tmp);
            tmp = document.CreateAttribute("KeyPath");
            newUser.Attributes.Append(tmp);

            return newUser;
        }
    }
}

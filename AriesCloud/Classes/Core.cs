using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AriesCloud.Classes
{
    /// <summary>
    /// Менеджер доступа к серверу.
    /// </summary>
    public static class Core
    {
        /// <summary>
        /// Адрес сервера.
        /// </summary>
        private const string server = "http://localhost";

        /// <summary>
        /// Авторизация.
        /// </summary>
        /// <param name="token">Токен.</param>
        /// <returns>Пройдена ли аунтификация.</returns>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="Exception"></exception>
        public static bool Authorization(string token)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "hash", token}
            };

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "main/autorizationHash")
            {
                Content = new FormUrlEncodedContent(parameters)
            };

            bool result = SendMessage<bool>(message);
            return result;
        }

        /// <summary>
        /// Регистрация.
        /// </summary>
        /// <param name="email">Почта.</param>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        /// <exception cref="Exception"></exception>
        public static void Registration(string email, string login, string password)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "email", email},
                { "login", login},
                { "password", password}
            };

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "main/autorizationHash")
            {
                Content = new FormUrlEncodedContent(parameters)
            };

            string result = SendMessage<string>(message);

            if (result != "Регистрация прошла успешно, проверьте почту")
            {
                throw new Exception($"Не удалось зарегистрироваться\n{result}");
            }
        }

        /// <summary>
        /// Получение содержимого папки.
        /// </summary>
        /// <param name="directoryPath">Путь к папке.</param>
        /// <returns>Содержимое папки.</returns>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="Exception"></exception>
        public static List<DirectoryItem> GetDirecoryItems(string directoryPath)
        {
            List<DirectoryItem> directoryItems = new List<DirectoryItem>();
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "main/getFiles");
            string result = SendMessage<string>(message);

            Dictionary<string, string> dictValues = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(result);

            foreach (KeyValuePair<string, string> item in dictValues)
            {
                File file = new File()
                {
                    Name = item.Value
                };

                directoryItems.Add(file);
            }

            return directoryItems;
        }

        /// <summary>
        /// Отправка сообщения серверу.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <returns>Содержание сообщения.</returns>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="Exception"></exception>
        private static T SendMessage<T>(HttpRequestMessage message)
        {
            T result;

            using (HttpClient client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 0, 5);
                client.BaseAddress = new Uri(server);

                using (Task<HttpResponseMessage> response = client.SendAsync(message))
                {
                    response.Wait();

                    using (HttpResponseMessage httpResponseMessage = response.Result)
                    {
                        if (!httpResponseMessage.IsSuccessStatusCode)
                        {
                            throw new Exception($"Сервер вернул ошибку {httpResponseMessage.StatusCode}: {httpResponseMessage.RequestMessage}");
                        }

                        result = (T)Convert.ChangeType(response.Result.Content.ReadAsStringAsync().Result, typeof(T));
                    }
                }
            }

            return result;
        }
    }
}

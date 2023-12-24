using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
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
        private const string server = "http://site";

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
        /// Загрузка файла на сервер.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="directory">Папка назначения.</param>
        /// <param name="key">Ключ шифрования.</param>
        /// <returns>Результат операции.</returns>
        public static bool UploadFile(FileInfo file, string directory, byte[] key)
        {
            HttpWebRequest httpWebRequest = HttpWebRequest.CreateHttp($"{server}/file/uploadAPI");
            httpWebRequest.Method = "Post";
            string boundary = $"_Upload_{Guid.NewGuid()}";
            httpWebRequest.ContentType = $"multipart/form-data; boundary=\"{boundary}\"";
            boundary = $"--{boundary}";
            httpWebRequest.KeepAlive = true;

            using (Stream requestStream = httpWebRequest.GetRequestStream())
            {
                using (FileStream fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine(boundary);
                    stringBuilder.AppendLine($"Content-Disposition: form-data; name=\"hash\"");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine(UserData.Hash);
                    stringBuilder.AppendLine(boundary);
                    stringBuilder.AppendLine($"Content-Disposition: form-data; name=\"dir\"");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine(directory);
                    stringBuilder.AppendLine(boundary);
                    stringBuilder.AppendLine($"Content-Disposition: form-data; name=\"file\"; filename=\"{file.Name}\"");
                    stringBuilder.AppendLine($"Content-Type: application/octet-stream");
                    stringBuilder.AppendLine();

                    WriteStringBuffer(requestStream, stringBuilder.ToString());

                    Scrambler scrambler = new Scrambler(key);
                    byte[] buffer = new byte[scrambler.BlockSize];

                    long blockCount = fileStream.Length / scrambler.BlockSize;

                    for (int i = 0; i < blockCount; i++)
                    {
                        fileStream.Read(buffer, 0, buffer.Length);
                        scrambler.EncriptBlock(buffer);
                        requestStream.Write(buffer, 0, buffer.Length);
                    }

                    // Последний блок должен содержать количество нулей + 1 в конце.
                    Array.Clear(buffer, 0, buffer.Length);
                    fileStream.Read(buffer, 0, buffer.Length);
                    buffer[buffer.Length - 1] = (byte)(scrambler.BlockSize - fileStream.Length % scrambler.BlockSize);
                    scrambler.EncriptBlock(buffer);
                    requestStream.Write(buffer, 0, buffer.Length);

                    stringBuilder.Clear();
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine($"{boundary}--");
                    WriteStringBuffer(requestStream, stringBuilder.ToString());
                }
            }

            using (WebResponse response = httpWebRequest.GetResponse())
            {
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                {
                    return Convert.ToBoolean(streamReader.ReadToEnd());
                }
            }
        }

        /// <summary>
        /// Скачивание файла.
        /// </summary>
        /// <param name="directory">Папка с файлом на удалённом сервере.</param>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="savePath">Путь сохранения файла.</param>
        /// <param name="key">Ключ шифрования.</param>
        /// <returns>Удалось ли скачать файл.</returns>
        public static bool DownloadFile(string directory, string fileName, string savePath, byte[] key)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "hash", UserData.Hash },
                { "dir", directory },
                { "fileName", fileName }
            };

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "file/downloadAPI")
            {
                Content = new FormUrlEncodedContent(parameters)
            };

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(server);

                    using (FileStream fileStream = new FileStream(savePath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        using (Task<HttpResponseMessage> responseTask = httpClient.SendAsync(message))
                        {
                            responseTask.Wait();

                            using (HttpResponseMessage responseMessage = responseTask.Result)
                            {
                                using (Task<Stream> streamTask = responseMessage.Content.ReadAsStreamAsync())
                                {
                                    streamTask.Wait();

                                    using (Stream readStream = streamTask.Result)
                                    {
                                        Scrambler scrambler = new Scrambler(key);
                                        long blockCount = readStream.Length / scrambler.BlockSize - 1;
                                        byte[] buffer = new byte[scrambler.BlockSize];

                                        for (int i = 0; i < blockCount; i++)
                                        {
                                            readStream.Read(buffer, 0, buffer.Length);
                                            scrambler.DecriptBlock(buffer);
                                            fileStream.Write(buffer, 0, buffer.Length);
                                        }

                                        readStream.Read(buffer, 0, buffer.Length);
                                        scrambler.DecriptBlock(buffer);
                                        byte zeroCount = buffer[buffer.Length - 1];
                                        fileStream.Write(buffer, 0, buffer.Length - zeroCount);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
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
            // TODO: Добавить получение файлов и папок из конкретной директории.
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

        /// <summary>
        /// Записывает строку в поток.
        /// </summary>
        /// <param name="stream">Поток.</param>
        /// <param name="str">Строка.</param>
        private static void WriteStringBuffer(Stream stream, string str)
        {
            byte[] buffer = Encoding.Default.GetBytes(str);
            stream.Write(buffer, 0, buffer.Length);
        }
    }
}

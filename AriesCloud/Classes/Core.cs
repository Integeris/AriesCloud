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
        private const string server = "http://ariescloud.ru";

        /// <summary>
        /// Размер буфера по умолчанию.
        /// </summary>
        private const int bufferSize = 1024;

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
                { "hash", token }
            };

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "main/autorizationHash")
            {
                Content = new FormUrlEncodedContent(parameters)
            };

            bool result;

            try
            {
                result = SendMessage<bool>(message);
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось авторизоваться", ex);
            }

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

            string result;

            try
            {
                result = SendMessage<string>(message);
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось зарегестрироваться", ex);
            }

            if (result != "Регистрация прошла успешно, проверьте почту")
            {
                throw new Exception($"Не удалось зарегистрироваться\n{result}");
            }
        }

        /// <summary>
        /// Изменение пароля.
        /// </summary>
        /// <param name="newPassword">Новый пароль.</param>
        public static void ChangePassword(string newPassword)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "hash", UserData.Hash },
                { "newPassword", newPassword }
            };

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "files/changePasswordAPI")
            {
                Content = new FormUrlEncodedContent(parameters)
            };

            try
            {
                UserData.Hash = SendMessage<string>(message);
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось изменить пароль", ex);
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
            HttpWebRequest httpWebRequest = HttpWebRequest.CreateHttp($"{server}/files/uploadAPI");
            httpWebRequest.Method = "Post";
            string boundary = $"_Upload_{Guid.NewGuid()}";
            httpWebRequest.ContentType = $"multipart/form-data; boundary=\"{boundary}\"";
            boundary = $"--{boundary}";
            httpWebRequest.KeepAlive = true;

            try
            {
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
                        byte[] buffer = new byte[bufferSize];
                        long bufferCount = fileStream.Length / buffer.Length;
                        int lastBlockCount = (int)(fileStream.Length % bufferCount / Scrambler.BlockSize);

                        for (int i = 0; i < bufferCount; i++)
                        {
                            fileStream.Read(buffer, 0, buffer.Length);
                            scrambler.Encript(buffer);
                            requestStream.Write(buffer, 0, buffer.Length);
                        }

                        // Последние целые блоки не вошедшие в целый пакет.
                        Array.Resize(ref buffer, lastBlockCount * Scrambler.BlockSize);
                        fileStream.Read(buffer, 0, buffer.Length);
                        scrambler.Encript(buffer);
                        requestStream.Write(buffer, 0, buffer.Length);

                        // Последний блок должен содержать количество нулей + 1 в конце.
                        Array.Resize(ref buffer, Scrambler.BlockSize);
                        Array.Clear(buffer, 0, buffer.Length);
                        fileStream.Read(buffer, 0, buffer.Length);
                        buffer[buffer.Length - 1] = (byte)(Scrambler.BlockSize - fileStream.Length % Scrambler.BlockSize);
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
            catch (Exception ex)
            {
                throw new Exception("Не удалось загрузить файл.", ex);
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
        public static void DownloadFile(string directory, string fileName, string savePath, byte[] key)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "hash", UserData.Hash },
                { "dir", directory },
                { "fileName", fileName }
            };

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "files/downloadAPI")
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
                                if (responseMessage.StatusCode == HttpStatusCode.NotFound)
                                {
                                    throw new Exception($"На сервере отсутствует файл {fileName}.");
                                }

                                using (Task<Stream> streamTask = responseMessage.Content.ReadAsStreamAsync())
                                {
                                    streamTask.Wait();

                                    using (Stream readStream = streamTask.Result)
                                    {
                                        Scrambler scrambler = new Scrambler(key);
                                        byte[] buffer = new byte[bufferSize];
                                        long bufferCount = readStream.Length / buffer.Length;
                                        int lastBlockCount = (int)(readStream.Length % buffer.Length / Scrambler.BlockSize);

                                        for (int i = 0; i < bufferCount; i++)
                                        {
                                            readStream.Read(buffer, 0, buffer.Length);
                                            scrambler.Decript(buffer);
                                            fileStream.Write(buffer, 0, buffer.Length);
                                        }

                                        // Последние целые блоки не вошедшие в целый пакет.
                                        Array.Resize(ref buffer, lastBlockCount * Scrambler.BlockSize);
                                        readStream.Read(buffer, 0, buffer.Length);
                                        scrambler.Decript(buffer);
                                        fileStream.Write(buffer, 0, buffer.Length);

                                        byte zeroCount = buffer[buffer.Length - 1];
                                        fileStream.SetLength(fileStream.Length - zeroCount);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось скачать файл.", ex);
            }
        }

        /// <summary>
        /// Переименование файла или папки.
        /// </summary>
        /// <param name="directory">Папка с файлом или папкой.</param>
        /// <param name="itemName">Имя файла или папки.</param>
        /// <param name="newName">Новое имя файла или папки.</param>
        /// <returns>Удалось ли переименовать файл или папки.</returns>
        public static bool RenameDirecoryItem(string directory, string itemName, string newName)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "hash", UserData.Hash },
                { "dir", directory },
                { "oldName", itemName },
                { "newName", newName }
            };

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "files/rename")
            {
                Content = new FormUrlEncodedContent(parameters)
            };

            try
            {
                return SendMessage<bool>(message);
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось переименовать элемент.", ex);
            }
        }

        /// <summary>
        /// Удаление файла или папке.
        /// </summary>
        /// <param name="directory">Путь к папке с файлом или папкой.</param>
        /// <param name="itemName">Имя папки или файла.</param>
        /// <returns>Удалось ли удалить файл или папку.</returns>
        public static bool RemoveDirecoryItem(string directory, string itemName)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "hash", UserData.Hash },
                { "dir", directory },
                { "dataFiles", itemName }
            };

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "files/delFiles")
            {
                Content = new FormUrlEncodedContent(parameters)
            };

            try
            {
                return SendMessage<bool>(message);
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось переименовать элемент.", ex);
            }
        }

        /// <summary>
        /// Перемещение папки или файла.
        /// </summary>
        /// <param name="itemPath">Путь к папке или файлу.</param>
        /// <param name="newItemPath">Новый путь к папке или файлу.</param>
        /// <returns>Удалось ли переместить папку или файл.</returns>
        public static bool MoveDirecoryItem(string itemPath, string newItemPath)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "hash", UserData.Hash },
                { "oldPath", itemPath },
                { "newPath", newItemPath }
            };

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "files/move")
            {
                Content = new FormUrlEncodedContent(parameters)
            };

            try
            {
                return SendMessage<bool>(message);
            }
            catch (Exception ex) 
            { 
                throw new Exception("Не удалось переместить (переименовать) элемент.", ex); 
            }
        }

        /// <summary>
        /// Создание папки.
        /// </summary>
        /// <param name="directory">Путь к папке где будет создана папки.</param>
        /// <param name="directoryName">Название новой папки.</param>
        /// <returns>Создана ли папка.</returns>
        public static bool CreateDirectory(string directory, string directoryName)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "hash", UserData.Hash },
                { "dir", directory },
                { "nameFolder", directoryName }
            };

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "files/createFolder")
            {
                Content = new FormUrlEncodedContent(parameters)
            };

            try
            {
                return SendMessage<bool>(message);
            }
            catch (Exception ex) 
            { 
                throw new Exception("Не удалось создать папку.", ex); 
            }
        }

        /// <summary>
        /// Получение содержимого папки.
        /// </summary>
        /// <param name="directory">Путь к папке.</param>
        /// <returns>Содержимое папки.</returns>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="Exception"></exception>
        public static List<DirectoryItem> GetDirecoryItems(string directory)
        {
            List<DirectoryItem> directoryItems = new List<DirectoryItem>();

            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "hash", UserData.Hash },
                { "dir", directory }
            };

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "files/getFiles")
            {
                Content = new FormUrlEncodedContent(parameters)
            };

            try
            {
                string result = SendMessage<string>(message);
                List<SerializeItem> items = System.Text.Json.JsonSerializer.Deserialize<List<SerializeItem>>(result);

                foreach (SerializeItem item in items)
                {
                    DirectoryItem directoryItem = item.Type == "f" ? new File(item.Name) : new Directory(item.Name);
                    directoryItems.Add(directoryItem);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось получить элементы папкки.", ex);
            }

            return directoryItems;
        }

        /// <summary>
        /// Получение всех папок.
        /// </summary>
        /// <returns>Список всех папок.</returns>
        public static List<string> GetDirecories()
        {
            List<string> directories;

            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "hash", UserData.Hash }
            };

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "files/getAllDir")
            {
                Content = new FormUrlEncodedContent(parameters)
            };

            try
            {
                string result = SendMessage<string>(message);
                directories = System.Text.Json.JsonSerializer.Deserialize<List<string>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось получить список папок.", ex);
            }

            return directories;
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

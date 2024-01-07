using System.Text.Json.Serialization;

namespace AriesCloud.Classes
{
    /// <summary>
    /// Файл или папка после сериализации.
    /// </summary>
    public class SerializeItem
    {
        /// <summary>
        /// Имя.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Тип.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}

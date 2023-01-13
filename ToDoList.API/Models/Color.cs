using System.Text.Json.Serialization;

namespace ToDoList.API.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Color
    {
        Magenta = 0,
        Yellow = 1,
        Blue = 2,
        Red = 3,
        Cyan = 4,
        White = 5
    }
}

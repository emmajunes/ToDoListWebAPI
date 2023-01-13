using System.Text.Json.Serialization;

namespace ToDoList.API.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortList
    {
        Ascendning = 0,
        Descending = 1,
        Alphabetic = 2,
        Color = 3,
    }
}
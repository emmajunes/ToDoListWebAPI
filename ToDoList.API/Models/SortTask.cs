using System.Text.Json.Serialization;

namespace ToDoList.API.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortTask
    {
        Priority = 0,
        Completed = 1
    }
}
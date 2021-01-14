using System.Text.Json.Serialization;

namespace Homework4.Task1
{
    public class Settings
    {
        [JsonPropertyName("primesFrom")]
        public int LowerBound { get; set; }

        [JsonPropertyName("primesTo")]
        public int UpperBound { get; set; }
    }
}

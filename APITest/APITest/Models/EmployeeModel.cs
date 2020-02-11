using Newtonsoft.Json;

namespace APITest.Models
{
    public class EmployeeModel
    {
       // [JsonProperty("id")]
        public int? id { get; set; }

        // [JsonProperty("name")]
        public string? name { get; set; }

       // [JsonProperty("salary")]
        public int? salary { get; set; }

        // [JsonProperty("age")]
        public int? age { get; set; }

        // [JsonProperty("image")]
        public string? image { get; set; }
    }
}
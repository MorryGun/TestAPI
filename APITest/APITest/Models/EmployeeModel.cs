using Newtonsoft.Json;

namespace APITest.Models
{
    public class EmployeeModel
    {
       [JsonProperty(PropertyName = "id")]
       public int? Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }

        [JsonProperty(PropertyName = "salary")]
        public int? Salary { get; set; }

        [JsonProperty(PropertyName = "age")]
        public int? Age { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string? Image { get; set; }
    }
}
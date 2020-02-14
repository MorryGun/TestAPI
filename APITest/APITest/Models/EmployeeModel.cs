using Newtonsoft.Json;

namespace APITest.Models
{
    public class EmployeeModelRequest
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
        public string Image { get; set; } = "";
    }

    public class EmployeeModelResponse
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "employee_name")]
        public string? Name { get; set; }

        [JsonProperty(PropertyName = "employee_salary")]
        public int? Salary { get; set; }

        [JsonProperty(PropertyName = "employee_age")]
        public int? Age { get; set; }

        [JsonProperty(PropertyName = "profile_image")]

        public string? Image { get; set; }
    }

}
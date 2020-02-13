using APITest.Models;
using Newtonsoft.Json;
using RestSharp;

namespace APITest.TestsSupports
{
    public static class TestsSupport
    {
        public static string StatusValidation(IRestResponse response)
        {
            string status;

            try
            {
                status = JsonConvert.DeserializeObject<RecordDoesNotFound>(response.Content).status;
            }
            catch
            {
                try
                {
                    status = JsonConvert.DeserializeObject<Employee>(response.Content).status;
                }
                catch
                {
                    status = JsonConvert.DeserializeObject<Employees>(response.Content).status;
                }
            }

            return status;
        }
    }
}

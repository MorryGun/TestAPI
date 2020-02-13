using APITest.Models;
using Newtonsoft.Json;
using RestSharp;
using APITest.Controllers;
using System.Threading.Tasks;
using System;

namespace APITest.TestsSupports
{
    public class TestsSupport : EmployeeController
    {
        public static string StatusValidation(IRestResponse response)
        {
            string status;

            try
            {
                status = JsonConvert.DeserializeObject<RecordDoesNotFoundModel>(response.Content).status;
            }
            catch
            {
                try
                {
                    status = JsonConvert.DeserializeObject<EmployeeModel>(response.Content).status;
                }
                catch
                {
                    status = JsonConvert.DeserializeObject<EmployeesModel>(response.Content).status;
                }
            }

            return status;
        }

        public static string StatusException(string status)
        {
            return string.Format("Status of request is {0}, but it is wrong status", status);
        }

        public async Task<uint> GetRandomExistingId()
        {
            var response = await GetAllEmployeesAsync();

            var employees = JsonConvert.DeserializeObject<EmployeesModel>(response.Content);

            var index = new Random().Next(1, employees.data.Length);

            return await Task.Run(() => employees.data[index].id);
        }

        public static string GetRandomNewName()
        {
            string result = "";
            var rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                result += (char)(rand.Next(1040, 1104));
            }

            return result;
        }

        public static uint GetRandomNewSalary()
        {
            return (uint) new Random().Next(1, 5000);
        }

        public static byte GetRandomNewAge()
        {
            return (byte) new Random().Next(18, 80);
        }
    }
}

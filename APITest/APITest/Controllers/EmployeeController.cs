using APITest.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITest.Controllers
{
    public class EmployeeController : BaseController
    {
        private const string GetEmployeeUrl = "/employees";
        private const string GetEmployeeByIdUrl = "/employee/{0}";
        private const string CreateEmployeeByIdUrl = "/create";
        private const string UpdateEmployeeByIdUrl = "/update/{0}";
        private const string DeleteEmployeeByIdUrl = "/delete/{0}";

        protected async Task<List<EmployeeModel>> GetEmployeeAsync()
        {
            var resource = string.Join(this.BaseUrl, GetEmployeeUrl);
            var response = await this.GetAsync(resource);
            string jsonRespose = string.Join("", JsonConvert.SerializeObject(response).Split("employee_").Distinct());
            jsonRespose  = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonRespose).Values.ToList()[1].ToString();
            return JsonConvert.DeserializeObject<List<EmployeeModel>>(jsonRespose);
        }

        protected async Task<IRestResponse> GetEmployeeByIdAsync(int employeeId)
        {
            var resource = string.Join(this.BaseUrl, string.Format(GetEmployeeByIdUrl, employeeId));
            return (IRestResponse)await this.ExecuteAsync(resource, Method.GET);            
        }

        protected async Task<EmployeeModel> PostAsync(EmployeeModel employee)
        {
            var resource = string.Join(this.BaseUrl, string.Format(CreateEmployeeByIdUrl));
            string jsonEmployee = JsonConvert.SerializeObject(employee);
            Dictionary<string, object> response = (Dictionary<string, object>)((Dictionary<string, object>)await this.PostAsync(resource, jsonEmployee))["data"];
            string responseJson = JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented);
            return JsonConvert.DeserializeObject<EmployeeModel>(responseJson);
        }

        protected async Task<string> DeleteAsync(int employeeId)
        {
            var resource = string.Join(this.BaseUrl, string.Format(DeleteEmployeeByIdUrl, employeeId));
            return ((Dictionary<string, object>)await this.DeleteAsync(resource))["status"].ToString();
        }

        protected async Task<string> PutAsync(int employeeId, EmployeeModel employee)
        {
            var resource = string.Join(this.BaseUrl, string.Format(UpdateEmployeeByIdUrl, employeeId));
            string jsonEmployee = JsonConvert.SerializeObject(employee);
            return ((Dictionary<string, object>)await this.PutAsync(resource, jsonEmployee))["status"].ToString();
        }
    }
}
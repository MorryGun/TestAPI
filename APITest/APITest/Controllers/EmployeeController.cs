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

        protected async Task<List<EmployeeModelResponse>> GetEmployeeAsync()
        {
            var resource = string.Join(this.BaseUrl, GetEmployeeUrl);
            var response = await this.GetAsync(resource);
            string jsonRespose = JsonConvert.SerializeObject(GetValueFromResponsByKey(response, "data"));
            return JsonConvert.DeserializeObject<List<EmployeeModelResponse>>(jsonRespose);
        }

        protected async Task<EmployeeModelRequest> PostAsync(EmployeeModelRequest employee)
        {
            var resource = string.Join(this.BaseUrl, string.Format(CreateEmployeeByIdUrl));
            object response = await this.PostAsync(resource, JsonConvert.SerializeObject(employee));
            string jsonRespose = JsonConvert.SerializeObject(GetValueFromResponsByKey(response, "data"));
            return JsonConvert.DeserializeObject<EmployeeModelRequest>(jsonRespose);
        }

        protected async Task<IRestResponse> GetEmployeeByIdAsync(int employeeId)
        {
            var resource = string.Join(this.BaseUrl, string.Format(GetEmployeeByIdUrl, employeeId));
            return (IRestResponse)await this.ExecuteAsync(resource, Method.GET);            
        }

        protected async Task<string> DeleteAsync(int employeeId)
        {
            var resource = string.Join(this.BaseUrl, string.Format(DeleteEmployeeByIdUrl, employeeId));
            object response = await this.DeleteAsync(resource);
            return GetValueFromResponsByKey(response, "status").ToString();
        }

        protected async Task<string> PutAsync(int employeeId, EmployeeModelRequest employee)
        {
            var resource = string.Join(this.BaseUrl, string.Format(UpdateEmployeeByIdUrl, employeeId));
            object response = await this.PutAsync(resource, JsonConvert.SerializeObject(employee));
            return GetValueFromResponsByKey(response, "status").ToString();
        }

        protected object GetValueFromResponsByKey(object response, string key)
        {
            Dictionary<string, object> values = (Dictionary<string, object>)response;
            if (values.TryGetValue(key, out object value))
            {
                return value;
            }

            return default(object);
        }
    }
}
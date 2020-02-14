using System.Threading.Tasks;
using RestSharp;
using APITest.Constants;
using APITest.Models;
using Newtonsoft.Json;

namespace APITest.Controllers
{
    public class EmployeeController
    {
        private async Task AddCookiesInRequest(RestRequest request)
        {
            var response = await GetAllEmployeesAsync();

            request.AddCookie("PHPSESSID", response.Cookies[0].Value);
        }

        protected async Task<IRestResponse> GetAllEmployeesAsync()
        {
            var client = new RestClient(ConfigConstants.GetAllEmployeesURL);

            var request = new RestRequest(ConfigConstants.GetAllEmployeesURL, Method.GET);

            return await Task.Run(() => client.Get(request));
        }

        protected async Task<IRestResponse> GetEmployeeByIdAsync(uint employeeId)
        {
            var client = new RestClient(string.Concat(ConfigConstants.GetEmployeeURL, employeeId));

            var request = new RestRequest(string.Concat(ConfigConstants.GetEmployeeURL, employeeId), Method.GET);

            await AddCookiesInRequest(request);

            return await Task.Run(() => client.Get<RestResponse>(request));
        }

        protected async Task<IRestResponse> CreateEmployeeAsync(string employeeName, uint employeeSalary, byte employeeAge)
        {
            var client = new RestClient(ConfigConstants.CreateEmployeeURL);

            var request = new RestRequest(ConfigConstants.CreateEmployeeURL, Method.POST);

            await AddCookiesInRequest(request);

            RequestBodyModel body = new RequestBodyModel();

            body.name = employeeName;

            body.salary = employeeSalary;

            body.age = employeeAge;

            request.AddJsonBody(JsonConvert.SerializeObject(body));

            return await Task.Run(() => client.Post<RestResponse>(request));
        }

        protected async Task<IRestResponse> UpdateEmployeeAsync(uint employeeId, string employeeName, uint employeeSalary, byte employeeAge)
        {
            var client = new RestClient(string.Concat(ConfigConstants.UpdateEmployeeURL, employeeId));

            var request = new RestRequest(string.Concat(ConfigConstants.UpdateEmployeeURL, employeeId), Method.PUT);

            await AddCookiesInRequest(request);

            RequestBodyModel body = new RequestBodyModel();

            body.name = employeeName;

            body.salary = employeeSalary;

            body.age = employeeAge;

            request.AddJsonBody(JsonConvert.SerializeObject(body));

            return await Task.Run(() => client.Put<RestResponse>(request));
        }

        protected async Task<IRestResponse> DeleteEmployeeAsync(uint employeeId)
        {
            var client = new RestClient(string.Concat(ConfigConstants.DeleteEmployeeURL, employeeId));

            var request = new RestRequest(string.Concat(ConfigConstants.DeleteEmployeeURL, employeeId), Method.DELETE);

            await AddCookiesInRequest(request);

            return await Task.Run(() => client.Delete<RestResponse>(request));
        }
    }
}

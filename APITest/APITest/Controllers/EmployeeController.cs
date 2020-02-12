using System.Threading.Tasks;
using RestSharp;
using APITest.Constants;

namespace APITest.Controllers
{
    public class EmployeeController
    {
        public async Task<IRestResponse> GetAllEmployeesAsync()
        {
            var client = new RestClient(ConfigConstants.GetAllEmployeesURL);

            var request = new RestRequest(ConfigConstants.GetAllEmployeesURL, Method.GET);

            return await Task.Run(() => client.Get(request));
        }

        public async Task<IRestResponse> GetEmployeeByIdAsync(int employeeId)
        {
            var response = await GetAllEmployeesAsync();

            var client = new RestClient(string.Concat(ConfigConstants.GetEmployeeURL, employeeId));

            var request = new RestRequest(string.Concat(ConfigConstants.GetEmployeeURL, employeeId), Method.GET);

            request.AddCookie("PHPSESSID", response.Cookies[0].Value);
            
            return await Task.Run(() => client.Get<RestResponse>(request));
        }

        public async Task<IRestResponse> CreateEmployeeAsync(string employeeName, int employeeSalary, int employeeAge)
        {
            var client = new RestClient(ConfigConstants.CreateEmployeeURL);

            var request = new RestRequest(ConfigConstants.CreateEmployeeURL, Method.POST);

            var body = string.Format("{{\"name\":\"{0}\",\"salary\":\"{1}\",\"age\":\"{2}\"}}", employeeName, employeeSalary, employeeAge);

            request.AddJsonBody(body);

            return await Task.Run(() => client.Post<RestResponse>(request));
        }

        public async Task<IRestResponse> UpdateEmployeeAsync(int employeeId, string employeeName, int employeeSalary, int employeeAge)
        {
            var client = new RestClient(string.Concat(ConfigConstants.UpdateEmployeeURL, employeeId));

            var request = new RestRequest(string.Concat(ConfigConstants.UpdateEmployeeURL, employeeId), Method.PUT);

            var body = string.Format("{{\"name\":\"{0}\",\"salary\":\"{1}\",\"age\":\"{2}\"}}", employeeName, employeeSalary, employeeAge);

            request.AddJsonBody(body);

            return await Task.Run(() => client.Put<RestResponse>(request));
        }

        public async Task<IRestResponse> DeleteEmployeeAsync(int employeeId)
        {
            var client = new RestClient(string.Concat(ConfigConstants.DeleteEmployeeURL, employeeId));

            var request = new RestRequest(string.Concat(ConfigConstants.DeleteEmployeeURL, employeeId), Method.DELETE);

            return await Task.Run(() => client.Delete<RestResponse>(request));
        }
    }
}

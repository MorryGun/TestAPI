using APITest.Models;
using RestSharp;
using System.Threading.Tasks;

namespace APITest.Controllers
{
    public class EmployeeController : BaseController
    {
        
        private const string GetEmployeeUrl = "/employees";
        private const string GetEmployeeByIdUrl = "/employees/{0}";
        private const string CreateNewEmployeeUrl = "/create";
        private const string DeleteEmployeeByIdUrl = "/delete/{0}";
        private const string UpdateEmployeeByIdUrl = "/update/{0}";

        protected async Task<object> GetEmployeeAsync()
        {
            var resource = string.Join(this.BaseUrl, GetEmployeeUrl);
            return await this.GetAsync(resource);
        }

        protected async Task<object> GetEmployeeByIdAsync(int employeeId)
        {
            var resource = string.Join(this.BaseUrl, string.Format(GetEmployeeByIdUrl, employeeId));
            return await this.GetAsync(resource);
        }

        protected async Task<object> CreateNewEmplpoyeeAsync()
        {
            var resource = string.Join(this.BaseUrl, CreateNewEmployeeUrl);
            return await this.PostAsync(resource);
        }

        protected async Task<object> UpdateEmployeeByIdAsync(int employeeId)
        {
            var resource = string.Join(this.BaseUrl, string.Format(UpdateEmployeeByIdUrl, employeeId));
            return await this.PutAsync(resource);
        }
        protected async Task<object> DeleteEmployeeByIdAsync(int employeeId)
        {
            var resource = string.Join(this.BaseUrl, string.Format(DeleteEmployeeByIdUrl, employeeId));
            return await this.GetAsync(resource);
        }

       
    }
}

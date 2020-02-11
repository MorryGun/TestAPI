using APITest.Models;
using RestSharp;
using System.Collections.Generic;
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


        protected async Task<object> PostAsync(EmployeeModel employee)
        {
            var resource = string.Join(this.BaseUrl, string.Format(CreateEmployeeByIdUrl));
            return await this.PostAsync(resource, employee);

        }

        protected async Task<object> DeleteAsync(int employeeId)
        {
            var resource = string.Join(this.BaseUrl, string.Format(DeleteEmployeeByIdUrl, employeeId));
            return await this.DeleteAsync(resource);
        }

        protected async Task<object> PutAsync(int employeeId, EmployeeModel employee)
        {
            var resource = string.Join(this.BaseUrl, string.Format(UpdateEmployeeByIdUrl, employeeId));
            return await this.PutAsync(resource, employee);
        }
    }
}
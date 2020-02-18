using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System;
using APITest.Models;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace APITest.Controllers
{
    public class EmployeeController : BaseController
    {
        private const string GetEmployeeUrl = "/employees";
        private const string GetEmployeeByIdUrl = "/employee/{0}";
        private const string PostEmployeeUrl = "/create";
        private const string DeleteEmployeeUrl = "/delete/{0}";
        private const string PutEmployeeUrl = "update/{0}";

        protected async Task<RestResponse<string>> GetEmployeeAsync()
        {
            var resource = string.Join(this.BaseUrl, GetEmployeeUrl);
            return await this.GetAsync(resource);
        }


        protected async Task<RestResponse<string>> GetEmployeeByIdAsync(string employeeId)
        {
            var resource = string.Join(this.BaseUrl, string.Format(GetEmployeeByIdUrl, employeeId));
            return await this.GetAsync(resource);
        }

        protected async Task <RestResponse<string>> PostEmployeeAsync(EmployeeModel employeeModel)
        {
            string body = JsonSerializer.Serialize<EmployeeModel>(employeeModel);
            var resource = string.Join(this.BaseUrl, PostEmployeeUrl);
            return await this.PostAsync(resource, body);
        }

        protected async Task<RestResponse<string>> DeleteEmployeeAsync(string employeeId)
        {
            var f = string.Format(DeleteEmployeeUrl, employeeId);
            var resource = string.Join(this.BaseUrl, f);
            return await this.DeleteAsync(resource);
        }

        protected async Task<RestResponse<string>> PutEmployeeAsync(string employeeId, EmployeeModel employeeModel)
        {
            string body = JsonSerializer.Serialize<EmployeeModel>(employeeModel);
            var resource = string.Join(this.BaseUrl, string.Format(PutEmployeeUrl, employeeId));
            return await this.PutAsync(resource, body);
        }
    }
}

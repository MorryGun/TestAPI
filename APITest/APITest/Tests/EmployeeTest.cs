using APITest.Controllers;
using APITest.Models;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace APITest.Tests
{
    [TestFixture]
    public class EmployeeTest : EmployeeController
    {
        [Test]
        public async Task CheckThatEmployeeControllerReturnsResponse()
        {
            object response = await this.GetEmployeeAsync();
            List<EmployeeModel> employees = JsonConvert.DeserializeObject<List<EmployeeModel>>(string.Join("", JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(response)).Values.ToList()[1].ToString().Split("employee_").Distinct()));
            employees.Should().HaveCount(24);
        }

        [Test]
        public async Task CheckThatEmployeeByIdControllerReturnsResponse()
        {
            IRestResponse response = (IRestResponse)await this.GetEmployeeByIdAsync(2);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task CheckThatPutEmployeeControllerReturnsResponse()
        {
            EmployeeModel employee = new EmployeeModel { name = "foo", salary = 30000, age = 30, id = 18, image = "" };
            string success = ((Dictionary<string, object>)await this.PutAsync(18, employee))["status"].ToString();
            success.Should().BeEquivalentTo("success");
        }

        [Test]
        public async Task CheckThatPostEmployeeControllerReturnsResponse()
        {
            EmployeeModel employee = new EmployeeModel { name = "foo", salary = 30000, age = 30, image = "" };
            Dictionary<string, object> response = (Dictionary<string, object>)((Dictionary<string, object>)await this.PostAsync(employee))["data"];
            string responseJson = JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented);
            EmployeeModel employeeResponse = JsonConvert.DeserializeObject<EmployeeModel>(responseJson);
            employeeResponse.id.Should().NotBe(null);
            employeeResponse.name.Should().BeEquivalentTo(employee.name);
            employeeResponse.salary.Should().Equals(employee.salary);
            employeeResponse.age.Should().Equals(employee.age);
            employeeResponse.name.Should().BeEquivalentTo(employee.name);
        }
    }
}

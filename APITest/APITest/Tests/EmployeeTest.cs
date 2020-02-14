using APITest.Controllers;
using APITest.Models;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
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
            List<EmployeeModelResponse> response = await this.GetEmployeeAsync();
            response.Should().HaveCount(24);
        }

        [Test]
        public async Task CheckThatEmployeeByIdControllerReturnsResponse()
        {
            IRestResponse response = await this.GetEmployeeByIdAsync(5);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task CheckThatPutEmployeeControllerReturnsResponse()
        {
            EmployeeModelRequest employee = new EmployeeModelRequest { Name = "foo", Salary = 30000, Age = 30, Id = 18, Image = "" };
            string response = await this.PutAsync(18, employee);
            response.Should().BeEquivalentTo("success");
        }

        [Test]
        public async Task CheckThatPostEmployeeControllerReturnsResponse()
        {
            EmployeeModelRequest employee = new EmployeeModelRequest { Name = "foo", Salary = 30000, Age = 30, Image = "" };
            EmployeeModelRequest employeeResponse = await this.PostAsync(employee);
            employeeResponse.Id.Should().NotBe(null);
            employeeResponse.Name.Should().BeEquivalentTo(employee.Name);
            employeeResponse.Salary.Should().Equals(employee.Salary);
            employeeResponse.Age.Should().Equals(employee.Age);
            employeeResponse.Name.Should().BeEquivalentTo(employee.Name);
        }

        [Test]
        public async Task CheckThatDeleteEmployeeControllerReturnsResponse()
        {
            string response = await this.DeleteAsync(5);
            response.Should().BeEquivalentTo("failed");
        }
    }
}

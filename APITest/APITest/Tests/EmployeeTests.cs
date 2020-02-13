using APITest.Controllers;
using APITest.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using APITest.TestsSupports;

namespace APITest.Tests
{
    [TestFixture]
    public class EmployeeTests : EmployeeController
    {
        [Test]
        public async Task GetAllEmployeesAsync_StatusIsSuccessTest()
        {
            var response = await GetAllEmployeesAsync();

            var status = TestsSupport.StatusValidation(response);

            Assert.That(status == "success", TestsSupport.StatusIsNotSuccessException(status));
        }

        [Test]
        public async Task GetAllEmployeesAsync_EmployeesFieldsWithoutNullValuesTest()
        {
            var response = await GetAllEmployeesAsync();

            var employees = JsonConvert.DeserializeObject<Employees>(response.Content);

            var result = employees.data.Where(item => item.employee_name == null || item.employee_salary == null || item.employee_age == null).Select(item => item.id).ToList();

            Assert.That(result.Count == 0, "One or more employees fields contain values null");
        }

        [Test]
        public async Task GetEmployeeByIdAsync_StatusIsSuccessTest()
        {
            var response = await GetEmployeeByIdAsync(2);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "success", TestsSupport.StatusIsNotSuccessException(status));
        }

        [Test]
        public async Task GetEmployeeByIdAsync_StatusIsNotSuccessTest()
        {
            var response = await GetEmployeeByIdAsync(9999);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "failed", TestsSupport.StatusIsSuccessException(status));
        }

        [Test]
        public async Task GetEmployeeByIdAsync_NegativeIdValueTest()
        {
            var response = await GetEmployeeByIdAsync(-1);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "failed", TestsSupport.StatusIsSuccessException(status));
        }


        [Test]
        public async Task CreateEmployeeAsync_StatusIsSuccessTest()
        {
            var response = await CreateEmployeeAsync("Test employee name", 1000, 99);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "success", TestsSupport.StatusIsNotSuccessException(status));
        }
    }
}

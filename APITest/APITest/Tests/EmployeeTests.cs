using APITest.Controllers;
using APITest.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace APITest.Tests
{
    [TestFixture]
    public class EmployeeTests : EmployeeController
    {
        [Test]
        public async Task GetAllEmployeesAsync_StatusIsSuccessTest()
        {
            //var response1 = await GetEmployeeByIdAsync(4);
            //var employee1 = JsonConvert.DeserializeObject<Employee>(response1.Content);

            var response = await GetAllEmployeesAsync();
            var employees = JsonConvert.DeserializeObject<Employees>(response.Content);

            Assert.That(employees.status == "success", "GetAllEmployeesAsync request is not success");
        }

        [Test]
        public async Task GetAllEmployeesAsync_EmployeesFieldsWithoutNullValuesTest()
        {
            var response = await GetAllEmployeesAsync();
            var employees = JsonConvert.DeserializeObject<Employees>(response.Content);

            var result = employees.data.Where(item => item.employee_name == null || item.employee_salary == null || item.employee_age == null).Select(item => item.id).ToList();

            Assert.That(result.Count == 0, "One or more employees fields contain values null");
        }
    }
}

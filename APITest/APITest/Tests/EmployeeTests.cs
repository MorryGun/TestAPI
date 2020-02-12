using APITest.Controllers;
using APITest.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APITest.Tests
{
    [TestFixture]
    public class EmployeeTests : EmployeeController
    {
        [Test]
        public async Task MyTest()
        {
            var response1 = await GetEmployeeByIdAsync(4);
            var employee1 = JsonConvert.DeserializeObject<Employee>(response1.Content);

            var response2 = await GetAllEmployeesAsync();
            var employee2 = JsonConvert.DeserializeObject<Employees>(response2.Content);
        }
    }
}

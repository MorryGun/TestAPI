using APITest.Controllers;
using NUnit.Framework;
using System.Threading.Tasks;

namespace APITest.Tests
{
    [TestFixture]
    public class EmployeeTests : EmployeeController
    {
        [Test]
        public async Task MyTest()
        {
            var response = await GetAllEmployeesAsync();
            var response2 = await GetEmployeeByIdAsync(4);
            var response3 = await CreateEmployeeAsync("Hello", 123456, 55);
            var response4 = await UpdateEmployeeAsync(4, "Hello", 123456, 55);
            var response5 = await DeleteEmployeeAsync(4);
        }
    }
}

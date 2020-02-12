using APITest.Controllers;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace APITest.Tests
{
    [TestFixture]
    public class EmployeeTest : EmployeeController
    {
        [Test]
        public async Task MyTest()
        {
            //var response = await this.GetAllEmployeesAsync();
            //var response = await this.GetEmployeeByIdAsync(4);
            var response = await this.CreateEmployeeAsync("Hhhhhhh", 33333, 66);
        }
    }
}

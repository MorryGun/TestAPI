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
        public async Task CheckThatEmployeeControllerReturnsResponse()
        {
            var response = await this.GetEmployeeAsync();

            response.Should().NotBeNull("Response is null");
        }
    }
}

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

            Assert.That(status == "success", TestsSupport.StatusException(status));
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

            Assert.That(status == "success", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task GetEmployeeByIdAsync_StatusIsNotSuccessTest()
        {
            var response = await GetEmployeeByIdAsync(9999);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "failed", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task CreateEmployeeAsync_StatusIsSuccessTest()
        {
            var response = await CreateEmployeeAsync(Consts.CommonName, Consts.CommonSalary, Consts.CommonAge);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "success", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task CreateEmployeeAsync_EmptyNameTest()
        {
            var response = await CreateEmployeeAsync("", Consts.CommonSalary, Consts.CommonAge);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "failed", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task CreateEmployeeAsync_OnlySpacesInNameTest()
        {
            var response = await CreateEmployeeAsync(" ", Consts.CommonSalary, Consts.CommonAge);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "failed", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task CreateEmployeeAsync_CorrectRussianNameTest()
        {
            var response = await CreateEmployeeAsync(Consts.CorrectRussianName, Consts.CommonSalary, Consts.CommonAge);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "success", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task CreateEmployeeAsync_CorrectEnglishNameTest()
        {
            var response = await CreateEmployeeAsync(Consts.CorrectEnglishName, Consts.CommonSalary, Consts.CommonAge);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "success", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task CreateEmployeeAsync_HugeNameTest()
        {
            var response = await CreateEmployeeAsync(Consts.HugeName, Consts.CommonSalary, Consts.CommonAge);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "failed", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task CreateEmployeeAsync_HugeSalaryTest()
        {
            var response = await CreateEmployeeAsync(Consts.CommonName, Consts.HugeSallary, Consts.CommonAge);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "failed", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task CreateEmployeeAsync_VerySmallAgeTest()
        {
            var response = await CreateEmployeeAsync(Consts.CommonName, Consts.CommonSalary, Consts.VerySmallAge);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "failed", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task CreateEmployeeAsync_NotBornYetAgeTest()
        {
            var response = await CreateEmployeeAsync(Consts.CommonName, Consts.CommonSalary, Consts.NotBornYetAge);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "failed", TestsSupport.StatusException(status));
        }
    }
}

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
        [OneTimeTearDown]
        public async Task OneTimeTearDownMethod()
        {
            await CreateEmployeeAsync(TestsSupport.GetRandomNewName(), TestsSupport.GetRandomNewSalary(), TestsSupport.GetRandomNewAge());
            await CreateEmployeeAsync(TestsSupport.GetRandomNewName(), TestsSupport.GetRandomNewSalary(), TestsSupport.GetRandomNewAge());
        }

        #region GET Tests
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

            var employees = JsonConvert.DeserializeObject<EmployeesModel>(response.Content);

            var result = employees.data.Where(item => item.employee_name == null || item.employee_salary == null || item.employee_age == null).Select(item => item.id).ToList();

            Assert.That(result.Count == 0, "One or more employees fields contain values null");
        }

        [Test]
        public async Task GetEmployeeByIdAsync_StatusIsSuccessTest()
        {
            var randomExistingId = await new TestsSupport().GetRandomExistingId();

            var response = await GetEmployeeByIdAsync(randomExistingId);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "success", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task GetEmployeeByIdAsync_EmployeeFieldsWithoutNullValuesTest()
        {
            var commonId = await new TestsSupport().GetRandomExistingId();

            var response = await GetEmployeeByIdAsync(commonId);

            var employee = JsonConvert.DeserializeObject<EmployeeModel>(response.Content);

            var result = employee.data.id != null && employee.data.employee_name != null && employee.data.employee_salary != null && employee.data.employee_age != null;

            Assert.That(result, "One or more employees fields contain values null");
        }

        [Test]
        public async Task GetEmployeeByIdAsync_EmployeeIdValueIsCorrectTest()
        {
            var commonId = await new TestsSupport().GetRandomExistingId();

            var response = await GetEmployeeByIdAsync(commonId);

            var employee = JsonConvert.DeserializeObject<EmployeeModel>(response.Content);

            var result = employee.data.id == commonId;

            Assert.That(result, "Field Id contains wrong value");
        }

        [Test]
        public async Task GetEmployeeByIdAsync_StatusIsNotSuccessTest()
        {
            var response = await GetEmployeeByIdAsync(Consts.HugeId);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "failed", TestsSupport.StatusException(status));
        }
        #endregion

        #region POST Tests
        [Test]
        public async Task CreateEmployeeAsync_StatusIsSuccessTest()
        {
            var response = await CreateEmployeeAsync(Consts.CommonName, Consts.CommonSalary, Consts.CommonAge);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "success", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task CreateEmployeeAsync_CorrectAllFieldsOfEmployeeTest()
        {
            var response = await CreateEmployeeAsync(Consts.CommonName, Consts.CommonSalary, Consts.CommonAge);

            var employee = JsonConvert.DeserializeObject<EmployeeModel>(response.Content);

            var result = employee.data.id != null &&
                employee.data.employee_name == Consts.CommonName &&
                employee.data.employee_salary == Consts.CommonSalary &&
                employee.data.employee_age == Consts.CommonAge;

            Assert.That(result, "Employee was created incorrectly");
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
        public async Task CreateEmployeeAsync_OneWordHugeNameTest()
        {
            var response = await CreateEmployeeAsync(Consts.OneWordHugeName, Consts.CommonSalary, Consts.CommonAge);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "failed", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task CreateEmployeeAsync_ManyWordsHugeNameTest()
        {
            var response = await CreateEmployeeAsync(Consts.ManyWordsHugeName, Consts.CommonSalary, Consts.CommonAge);

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
        #endregion

        #region PUT Tests
        [Test]
        public async Task UpdateEmployeeAsync_StatusIsSuccessTest()
        {
            var commonId = await new TestsSupport().GetRandomExistingId();

            var response = await UpdateEmployeeAsync(commonId, Consts.CommonName, Consts.CommonSalary, Consts.CommonAge);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "success", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task UpdateEmployeeAsync_CorrectAllFieldsOfEmployeeTest()
        {
            var commonId = await new TestsSupport().GetRandomExistingId();

            var response = await UpdateEmployeeAsync(commonId, Consts.CommonName, Consts.CommonSalary, Consts.CommonAge);

            var employee = JsonConvert.DeserializeObject<EmployeeModel>(response.Content);

            var result = employee.data.id == commonId &&
                employee.data.employee_name == Consts.CommonName &&
                employee.data.employee_salary == Consts.CommonSalary &&
                employee.data.employee_age == Consts.CommonAge;

            Assert.That(result, "Employee was updated incorrectly");
        }

        [Test]
        public async Task UpdateEmployeeAsync_NonExistentEmployeeTest()
        {
            var response = await UpdateEmployeeAsync(Consts.HugeId, Consts.CommonName, Consts.CommonSalary, Consts.CommonAge);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "failed", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task UpdateEmployeeAsync_EmptyNameTest()
        {
            var commonId = await new TestsSupport().GetRandomExistingId();

            var response = await UpdateEmployeeAsync(commonId, "", Consts.CommonSalary, Consts.CommonAge);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "failed", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task UpdateEmployeeAsync_OnlySpacesInNameTest()
        {
            var commonId = await new TestsSupport().GetRandomExistingId();

            var response = await UpdateEmployeeAsync(commonId, " ", Consts.CommonSalary, Consts.CommonAge);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "failed", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task UpdateEmployeeAsync_CorrectRussianNameTest()
        {
            var commonId = await new TestsSupport().GetRandomExistingId();

            var response = await UpdateEmployeeAsync(commonId, Consts.CorrectRussianName, Consts.CommonSalary, Consts.CommonAge);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "success", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task UpdateEmployeeAsync_CorrectEnglishNameTest()
        {
            var commonId = await new TestsSupport().GetRandomExistingId();

            var response = await UpdateEmployeeAsync(commonId, Consts.CorrectEnglishName, Consts.CommonSalary, Consts.CommonAge);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "success", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task UpdateEmployeeAsync_OneWordHugeNameTest()
        {
            var commonId = await new TestsSupport().GetRandomExistingId();

            var response = await UpdateEmployeeAsync(commonId, Consts.OneWordHugeName, Consts.CommonSalary, Consts.CommonAge);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "failed", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task UpdateEmployeeAsync_ManyWordsHugeNameTest()
        {
            var commonId = await new TestsSupport().GetRandomExistingId();

            var response = await UpdateEmployeeAsync(commonId, Consts.ManyWordsHugeName, Consts.CommonSalary, Consts.CommonAge);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "failed", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task UpdateEmployeeAsync_HugeSalaryTest()
        {
            var commonId = await new TestsSupport().GetRandomExistingId();

            var response = await UpdateEmployeeAsync(commonId, Consts.CommonName, Consts.HugeSallary, Consts.CommonAge);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "failed", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task UpdateEmployeeAsync_VerySmallAgeTest()
        {
            var commonId = await new TestsSupport().GetRandomExistingId();

            var response = await UpdateEmployeeAsync(commonId, Consts.CommonName, Consts.CommonSalary, Consts.VerySmallAge);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "failed", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task UpdateEmployeeAsync_NotBornYetAgeTest()
        {
            var commonId = await new TestsSupport().GetRandomExistingId();

            var response = await UpdateEmployeeAsync(commonId, Consts.CommonName, Consts.CommonSalary, Consts.NotBornYetAge);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "failed", TestsSupport.StatusException(status));
        }
        #endregion

        #region DELETE Tests
        [Test]
        public async Task DeleteEmployeeAsync_StatusIsSuccessTest()
        {
            var commonId = await new TestsSupport().GetRandomExistingId();

            var response = await DeleteEmployeeAsync(commonId);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "success", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task DeleteEmployeeAsync_EmployeeWasDeletedSuccessTest()
        {
            var commonId = await new TestsSupport().GetRandomExistingId();

            var deleteResponse = await DeleteEmployeeAsync(commonId);

            var getResponse = await GetEmployeeByIdAsync(commonId);

            string status = TestsSupport.StatusValidation(getResponse);

            Assert.That(status == "failed", TestsSupport.StatusException(status));
        }

        [Test]
        public async Task DeleteEmployeeAsync_NonExistentEmployeeTest()
        {
            var response = await DeleteEmployeeAsync(Consts.HugeId);

            string status = TestsSupport.StatusValidation(response);

            Assert.That(status == "failed", TestsSupport.StatusException(status));
        }
        #endregion
    }
}

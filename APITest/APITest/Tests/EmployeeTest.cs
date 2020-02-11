using APITest.Controllers;
using APITest.Models;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using System;
using APITest.Extenstions;
using APITest.Constants;

namespace APITest.Tests
{
    [TestFixture]
    public class EmployeeTest : EmployeeController
    {
        public Convertation convert = new Convertation();
        FaultModel faultData = new FaultModel();

        [Test]
        public async Task CheckThatEmployeeControllerReturnsResponse()
        {
            var response = await this.GetEmployeeAsync();

            //ResponseModel responseData = convert.ConvertDictionaryToJson(response, responseModel) as ResponseModel;

            var responseData = convert.ResponseConvertDictionaryToJson(response);

            responseData.Should().NotBeNull("Response is null");
        }

        [Test]
        public async Task CheckThatEmployeeControllerReturnsResponseFail()
        {
            var response = await this.GetEmployeeAsync();

            faultData = convert.FaultConvertDictionaryToJson(response);

            Console.WriteLine(faultData.status);
            Console.WriteLine(faultData.message);

            faultData.status.Should().Be(Datas.success);/*Not found*/
            //response.Should().Be("{[status, success], [data, [{\"id\":\"1\",\"employee_name\":\"Tiger Nixon\",\"employee_salary\":\"320800\",\"employee_age\":\"61\",\"profile_image\":\"\"},{\"id\":\"2\",\"employee_name\":\"Garrett Winters\",\"employee_salary\":\"170750\",\"employee_age\":\"63\",\"profile_image\":\"\"},{\"id\":\"3\",\"employee_name\":\"Ashton Cox\",\"employee_salary\":\"86000\",\"employee_age\":\"66\",\"profile_image\":\"\"},{\"id\":\"4\",\"employee_name\":\"Cedric Kelly\",\"employee_salary\":\"433060\",\"employee_age\":\"22\",\"profile_image\":\"\"},{\"id\":\"5\",\"employee_name\":\"Airi Satou\",\"employee_salary\":\"162700\",\"employee_age\":\"33\",\"profile_image\":\"\"},{\"id\":\"6\",\"employee_name\":\"Brielle Williamson\",\"employee_salary\":\"372000\",\"employee_age\":\"61\",\"profile_image\":\"\"},{\"id\":\"7\",\"employee_name\":\"Herrod Chandler\",\"employee_salary\":\"137500\",\"employee_age\":\"59\",\"profile_image\":\"\"},{\"id\":\"8\",\"employee_name\":\"Rhona Davidson\",\"employee_salary\":\"327900\",\"employee_age\":\"55\",\"profile_image\":\"\"},{\"id\":\"9\",\"employee_name\":\"Colleen Hurst\",\"employee_salary\":\"205500\",\"employee_age\":\"39\",\"profile_image\":\"\"},{\"id\":\"10\",\"employee_name\":\"Sonya Frost\",\"employee_salary\":\"103600\",\"employee_age\":\"23\",\"profile_image\":\"\"},{\"id\":\"11\",\"employee_name\":\"Jena Gaines\",\"employee_salary\":\"90560\",\"employee_age\":\"30\",\"profile_image\":\"\"},{\"id\":\"12\",\"employee_name\":\"Quinn Flynn\",\"employee_salary\":\"342000\",\"employee_age\":\"22\",\"profile_image\":\"\"},{\"id\":\"13\",\"employee_name\":\"Charde Marshall\",\"employee_salary\":\"470600\",\"employee_age\":\"36\",\"profile_image\":\"\"},{\"id\":\"14\",\"employee_name\":\"Haley Kennedy\",\"employee_salary\":\"313500\",\"employee_age\":\"43\",\"profile_image\":\"\"},{\"id\":\"15\",\"employee_name\":\"Tatyana Fitzpatrick\",\"employee_salary\":\"385750\",\"employee_age\":\"19\",\"profile_image\":\"\"},{\"id\":\"16\",\"employee_name\":\"Michael Silva\",\"employee_salary\":\"198500\",\"employee_age\":\"66\",\"profile_image\":\"\"},{\"id\":\"17\",\"employee_name\":\"Paul Byrd\",\"employee_salary\":\"725000\",\"employee_age\":\"64\",\"profile_image\":\"\"},{\"id\":\"18\",\"employee_name\":\"Gloria Little\",\"employee_salary\":\"237500\",\"employee_age\":\"59\",\"profile_image\":\"\"},{\"id\":\"19\",\"employee_name\":\"Bradley Greer\",\"employee_salary\":\"132000\",\"employee_age\":\"41\",\"profile_image\":\"\"},{\"id\":\"20\",\"employee_name\":\"Dai Rios\",\"employee_salary\":\"217500\",\"employee_age\":\"35\",\"profile_image\":\"\"},{\"id\":\"21\",\"employee_name\":\"Jenette Caldwell\",\"employee_salary\":\"345000\",\"employee_age\":\"30\",\"profile_image\":\"\"},{\"id\":\"22\",\"employee_name\":\"Yuri Berry\",\"employee_salary\":\"675000\",\"employee_age\":\"40\",\"profile_image\":\"\"},{\"id\":\"23\",\"employee_name\":\"Caesar Vance\",\"employee_salary\":\"106450\",\"employee_age\":\"21\",\"profile_image\":\"\"},{\"id\":\"24\",\"employee_name\":\"Doris Wilder\",\"employee_salary\":\"85600\",\"employee_age\":\"23\",\"profile_image\":\"\"}]]}");
            //((System.Collections.Generic.Dictionary<string, object>)response)["status"].Should().Be("Not found");
        }


        [Test]
        public async Task CheckThatEmployeeControllerReturnFivesEmployeeFailTest()
        {
            var response = await this.GetEmployeeByIdAsync(5);

            var responseData = convert.ResponseConvertDictionaryToJson(response);

            //foreach (var employee in responseData.data)
            //{
            //    employee.id.Should().Be(7);
            //}
            responseData.data.Should().BeNull("Data is present in the collection");
        }

        [Test]
        public async Task CheckThatEmployeeControllerReturnSeventhEmployeePassTest()
        {
            var response = await this.GetEmployeeByIdAsync(7);

            faultData = convert.FaultConvertDictionaryToJson(response);

            Console.WriteLine(faultData.status);
            Console.WriteLine(faultData.message);

            faultData.message.Should().Be(Datas.fail);

        }

        [Test]
        public async Task CheckThatEmployeeControlerCreateNewEmployee()
        {
            var response = await this.CreateNewEmplpoyeeAsync();

            //EmployeeModel employee = new EmployeeModel() { name = "New user", salary = "11111", age = "33" };
            ((System.Collections.Generic.Dictionary<string, object>)response)["status"]
            .Should().Be("success");
        }


        [Test]
        public async Task CheckThatEmployeeControlerCreateNewEmployeeFailTest()
        {
            var response = await this.CreateNewEmplpoyeeAsync();

            var responseData = convert.EmployeeConvertDictionaryToJson(response);

            responseData.Should().NotBeNull("Response is null");

        }

        [Test]
        public async Task CheckThatEmployeeControlerDeleteEmployeeByIdFailTest()
        {
            var response = await this.DeleteEmployeeByIdAsync(7);

            var responseData = convert.ResponseConvertDictionaryToJson(response);

            responseData.data.Should().BeNull("Data is present");

            //response.Should().BeNull("Is present in the collection");
        }

        [Test]
        public async Task CheckThatEmployeeControlerDeleteEmployeeById()
        {
            var response = await this.DeleteEmployeeByIdAsync(22);

            faultData = convert.FaultConvertDictionaryToJson(response);

            Console.WriteLine(faultData.status);
            Console.WriteLine(faultData.message);

            faultData.message.Should().Be(Datas.delete);

            //((System.Collections.Generic.Dictionary<string, object>)response)["message"].Should().NotBeNull("Is not present in the collection");
        }

        [Test]
        public async Task CheckThatEmployeeControlerUpdateEmployeeById()
        {
            var response = await this.UpdateEmployeeByIdAsync(7);

            var responseData = convert.ResponseConvertDictionaryToJson(response);

            responseData.status.Should().Be("success");
        }

        [Test]
        public async Task CheckThatEmployeeControlerUpdateEmployeeByIdFailTest()
        {
            var response = await this.UpdateEmployeeByIdAsync(7);

            var responseData = convert.ResponseConvertDictionaryToJson(response);

            responseData.data.Should().BeNull("Data is not null ");
        }

    }
}































//response = await this.GetEmployeeByIdAsync(41);
//((System.Collections.Generic.Dictionary<string, object>)response)["data"]
// ((System.Collections.Generic.Dictionary<string, object>)response)["message"].Should().Be("Not found");


//[Test]
//public async Task CheckThatEmployeeControllerReturnsResponse()
//{
//    var response = await this.GetEmployeeAsync();

//    string jsonResult = JsonConvert.SerializeObject(response, Formatting.Indented);
//    ResponseModel responseData = SimpleJson.DeserializeObject<ResponseModel>(jsonResult);

//    Console.WriteLine(responseData.status);

//    foreach (var res in responseData.data)
//    {
//        string jsonEmployee = JsonConvert.SerializeObject(res, Formatting.Indented);
//        EmployeeModel employee = SimpleJson.DeserializeObject<EmployeeModel>(jsonEmployee);


//        Console.WriteLine(employee.id);
//        Console.WriteLine(employee.employee_name);
//        Console.WriteLine(employee.employee_salary);
//        Console.WriteLine(employee.employee_age);
//        Console.WriteLine(employee.profile_image);
//    }

//    //((System.Collections.Generic.Dictionary<string, object>)response)["data"].Should().NotBeNull();
//    response.Should().NotBeNull("Response is null");
//}


//[Test]
//public async Task CheckThatEmployeeControllerReturnsResponseFail()
//{
//    var response = await this.GetEmployeeAsync();

//    faultData = convert.FaultConvertDictionaryToJson(response);

//    Console.WriteLine(faultData.status);
//    Console.WriteLine(faultData.message);

//    faultData.status.Should().Be("Not found");
//    //response.Should().Be("{[status, success], [data, [{\"id\":\"1\",\"employee_name\":\"Tiger Nixon\",\"employee_salary\":\"320800\",\"employee_age\":\"61\",\"profile_image\":\"\"},{\"id\":\"2\",\"employee_name\":\"Garrett Winters\",\"employee_salary\":\"170750\",\"employee_age\":\"63\",\"profile_image\":\"\"},{\"id\":\"3\",\"employee_name\":\"Ashton Cox\",\"employee_salary\":\"86000\",\"employee_age\":\"66\",\"profile_image\":\"\"},{\"id\":\"4\",\"employee_name\":\"Cedric Kelly\",\"employee_salary\":\"433060\",\"employee_age\":\"22\",\"profile_image\":\"\"},{\"id\":\"5\",\"employee_name\":\"Airi Satou\",\"employee_salary\":\"162700\",\"employee_age\":\"33\",\"profile_image\":\"\"},{\"id\":\"6\",\"employee_name\":\"Brielle Williamson\",\"employee_salary\":\"372000\",\"employee_age\":\"61\",\"profile_image\":\"\"},{\"id\":\"7\",\"employee_name\":\"Herrod Chandler\",\"employee_salary\":\"137500\",\"employee_age\":\"59\",\"profile_image\":\"\"},{\"id\":\"8\",\"employee_name\":\"Rhona Davidson\",\"employee_salary\":\"327900\",\"employee_age\":\"55\",\"profile_image\":\"\"},{\"id\":\"9\",\"employee_name\":\"Colleen Hurst\",\"employee_salary\":\"205500\",\"employee_age\":\"39\",\"profile_image\":\"\"},{\"id\":\"10\",\"employee_name\":\"Sonya Frost\",\"employee_salary\":\"103600\",\"employee_age\":\"23\",\"profile_image\":\"\"},{\"id\":\"11\",\"employee_name\":\"Jena Gaines\",\"employee_salary\":\"90560\",\"employee_age\":\"30\",\"profile_image\":\"\"},{\"id\":\"12\",\"employee_name\":\"Quinn Flynn\",\"employee_salary\":\"342000\",\"employee_age\":\"22\",\"profile_image\":\"\"},{\"id\":\"13\",\"employee_name\":\"Charde Marshall\",\"employee_salary\":\"470600\",\"employee_age\":\"36\",\"profile_image\":\"\"},{\"id\":\"14\",\"employee_name\":\"Haley Kennedy\",\"employee_salary\":\"313500\",\"employee_age\":\"43\",\"profile_image\":\"\"},{\"id\":\"15\",\"employee_name\":\"Tatyana Fitzpatrick\",\"employee_salary\":\"385750\",\"employee_age\":\"19\",\"profile_image\":\"\"},{\"id\":\"16\",\"employee_name\":\"Michael Silva\",\"employee_salary\":\"198500\",\"employee_age\":\"66\",\"profile_image\":\"\"},{\"id\":\"17\",\"employee_name\":\"Paul Byrd\",\"employee_salary\":\"725000\",\"employee_age\":\"64\",\"profile_image\":\"\"},{\"id\":\"18\",\"employee_name\":\"Gloria Little\",\"employee_salary\":\"237500\",\"employee_age\":\"59\",\"profile_image\":\"\"},{\"id\":\"19\",\"employee_name\":\"Bradley Greer\",\"employee_salary\":\"132000\",\"employee_age\":\"41\",\"profile_image\":\"\"},{\"id\":\"20\",\"employee_name\":\"Dai Rios\",\"employee_salary\":\"217500\",\"employee_age\":\"35\",\"profile_image\":\"\"},{\"id\":\"21\",\"employee_name\":\"Jenette Caldwell\",\"employee_salary\":\"345000\",\"employee_age\":\"30\",\"profile_image\":\"\"},{\"id\":\"22\",\"employee_name\":\"Yuri Berry\",\"employee_salary\":\"675000\",\"employee_age\":\"40\",\"profile_image\":\"\"},{\"id\":\"23\",\"employee_name\":\"Caesar Vance\",\"employee_salary\":\"106450\",\"employee_age\":\"21\",\"profile_image\":\"\"},{\"id\":\"24\",\"employee_name\":\"Doris Wilder\",\"employee_salary\":\"85600\",\"employee_age\":\"23\",\"profile_image\":\"\"}]]}");
//    //((System.Collections.Generic.Dictionary<string, object>)response)["status"].Should().Be("Not found");
//}



//[Test]
//public async Task CheckThatEmployeeControllerReturnSeventhEmployeePassTestFailTest()
//{
//    var response = await this.GetEmployeeByIdAsync(7);
//    ((System.Collections.Generic.Dictionary<string, object>)response)["data"].Should().Be("Not found");
//    //((System.Collections.Generic.Dictionary<string, object>)response)["message"].Should().Be("Not found");//"{\"id\":\"5\",\"name\":\"Airi Satou\",\"salary\":\"162700\",\"employee_age\":\"33\",\"profile_image\":\"\"}"
//}

//[Test]
//public async Task CheckThatEmployeeControlerReturnFirstEmployeeFailTest()
//{
//    var response = await this.GetEmployeeByIdAsync(1);

//    //response.Should().Be("{\"id\":\"1\",\"employee_name\":\"Tiger Nixon\",\"employee_salary\":\"320800\",\"employee_age\":\"61\",\"profile_image\":\"\"");/*{[message, Not found]}*/
//    ((System.Collections.Generic.Dictionary<string, object>)response)["message"].Should().Be("Not found");
//}

//[Test]
//public async Task CheckThatEmployeeControlerCreateNewEmployee()
//{
//    var response = await this.CreateNewEmplpoyeeAsync();

//    //EmployeeModel employee = new EmployeeModel() { name = "New user", salary = "11111", age = "33" };
//    ((System.Collections.Generic.Dictionary<string, object>)response)["status"]
//    /*response*/.Should().Be("success"); //{[status, success], [data, [{\"id\":\"27\",\"employee_name\":\"test1212\",\"employee_salary\":\"123\",\"employee_age\":\"23\",\"profile_image\":\"\"}]
//}



//[Test]
//public async Task CheckThatEmployeeControllerReturnFivesEmployeePassTest()
//{
//    var response = await this.GetEmployeeByIdAsync(5);

//    response.Should().Be("Id = 22, Status = WaitingForActivation, Method = \"{ null}\", Result = \"{ Not yet computed}\"");//"{\"id\":\"5\",\"name\":\"Airi Satou\",\"salary\":\"162700\",\"employee_age\":\"33\",\"profile_image\":\"\"}"
//                                                                                                                           //((System.Collections.Generic.Dictionary<string, object>)response)["message"].Should().Be("Not found");
//}




//[Test]
//public void CheckThatEmployeeControllerReturnFivesEmployeePassTestWithoutAsync()
//{
//    var response = this.GetEmployeeById(5);

//    response.Should().Be("Id = 22, Status = WaitingForActivation, Method = \"{ null}\", Result = \"{ Not yet computed}\"");
//}




//string fullRoute = "http://dummy.restapiexample.com/api/v1/employees/7";
//RestClient client = new RestClient(fullRoute);
//var request = new RestRequest(fullRoute, Method.GET);
//var result = await client.GetAsync<object>(request);






//string result = string.Join(",", response/*.Select(",", m => m.Key + " = " + m.Value).ToArray()*/);
/*result*//*(System.Collections.Generic.Dictionary<string, object>)response*/






//[Test]
//public void CheckThatEmployeeControllerReturnFivesEmployeeFailTestNotAsync()
//{
//    var response = GetById.PullDataFromListSingleId(4); // await this.GetEmployeeByIdAsync(5);

//    responseData = convert.ResponseConvertDictionaryToJson(response);

//    foreach (var res in responseData.data)
//    {
//        employee = convert.EmployeeConvertDictionaryToJson(res);
//    }

//    employee.id.Should().Be("22");

//}
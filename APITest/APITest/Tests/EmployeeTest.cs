using APITest.Controllers;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System;
using Microsoft.Extensions.Configuration.Json;
using APITest.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Microsoft.Extensions.Options;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using RestSharp.Serialization.Json;
using System.Linq;
using System.Collections;

namespace APITest.Tests
{
    public class EmployeeTest : EmployeeController
    {

        [Test]
        public async Task GetEmployee()
        {
            var responseGetEmp = await this.GetEmployeeAsync();
            JArray jsonParseGetEmp = JArray.Parse(responseGetEmp.Data);
            var jsonGetEmpData = JsonConvert.DeserializeObject<List<GetEmployeeModel>>(jsonParseGetEmp.ToString());

            var getOneEmp = jsonGetEmpData.Find(x => x.Employee_name == "Doris Wilder");
            var countOfList = jsonGetEmpData.Count();

            var employeeModelGet = new GetEmployeeModel {
                Id = "24",
                Employee_name = "Doris Wilder",
                Employee_salary = "85600",
                Employee_age = "23",
                Profile_image = ""
            };
            
            Assert.AreEqual(0, (int)responseGetEmp.StatusCode);
            Assert.IsNotNull(responseGetEmp.Content);
            Assert.AreEqual(24, countOfList);
            Assert.AreEqual("24", getOneEmp.Id);
            Assert.AreEqual("85600", getOneEmp.Employee_salary);
            Assert.AreEqual("Doris Wilder", getOneEmp.Employee_name);
            Assert.AreEqual("23", getOneEmp.Employee_age);
            CollectionAssert.AreEqual(employeeModelGet.ToString(), getOneEmp.ToString());
        }
        [Test]
        public async Task GetEmployeeById()
        {
            var responseGetEmpById = await this.GetEmployeeByIdAsync("1");
            var jsonGetEmpById = JsonConvert.DeserializeObject<GetEmployeeModel>(responseGetEmpById.Data);

            Assert.AreEqual(0, (int)responseGetEmpById.StatusCode);
            Assert.IsNotNull(responseGetEmpById.Content);
            Assert.AreEqual("1", jsonGetEmpById.Id);
            Assert.AreEqual("320800", jsonGetEmpById.Employee_salary);
            Assert.AreEqual("Tiger Nixon", jsonGetEmpById.Employee_name);
            Assert.AreEqual("61", jsonGetEmpById.Employee_age);
        }
        [Test]
        public async Task PostEmployee()
        {
            EmployeeModel employeeModelPost = new EmployeeModel { Name = "Eva", Salary = "1230", Age = "23" };
            var responsePostEmp = await this.PostEmployeeAsync(employeeModelPost);
            var jsonAddEmployee = JsonConvert.DeserializeObject<EmployeeModel>(responsePostEmp.Data);

            var responseGetEmpByIdForPost = await this.GetEmployeeByIdAsync(jsonAddEmployee.Id);
            var jsonCheckEmpForPost = JsonConvert.DeserializeObject<GetEmployeeModel>(responseGetEmpByIdForPost.Data);

            Assert.AreEqual(0, (int)responsePostEmp.StatusCode);
            Assert.IsNotNull(responsePostEmp.Content);
            Assert.IsNotEmpty(jsonAddEmployee.Id.ToString());
            Assert.AreEqual(employeeModelPost.Salary, jsonAddEmployee.Salary);
            Assert.AreEqual(employeeModelPost.Name, jsonAddEmployee.Name);
            Assert.AreEqual(employeeModelPost.Age, jsonAddEmployee.Age);
            CollectionAssert.AreEqual(employeeModelPost.ToString(), jsonAddEmployee.ToString());
            Assert.AreEqual(jsonAddEmployee.Id, jsonCheckEmpForPost.Id);
            Assert.AreEqual(jsonAddEmployee.Salary, jsonCheckEmpForPost.Employee_salary);
            Assert.AreEqual(jsonAddEmployee.Name, jsonCheckEmpForPost.Employee_name);
            Assert.AreEqual(jsonAddEmployee.Age, jsonCheckEmpForPost.Employee_age);
        }
        [Test]
        public async Task DeleteEmployee()
        {
            EmployeeModel employeeModelDelete = new EmployeeModel { Name = "Dana", Salary = "780", Age = "12" };
            var responsePostEmpFotDeleteEmp = await this.PostEmployeeAsync(employeeModelDelete);
            var jsonAddEmpForDelete = JsonConvert.DeserializeObject<EmployeeModel>(responsePostEmpFotDeleteEmp.Data);

            var responseDeleteEmp = await this.DeleteEmployeeAsync(jsonAddEmpForDelete.Id);
            var jsonDeleteEmp = JsonConvert.DeserializeObject<DeleteModel>(responseDeleteEmp.Data);

            var responseGetEmpByIdForDelete = await this.GetEmployeeByIdAsync(jsonAddEmpForDelete.Id);
            var jsonCheckEmpForDelete = JsonConvert.DeserializeObject<CheckEmployeeDelete>(responseGetEmpByIdForDelete.Data);

            Assert.AreEqual(0, (int)responseGetEmpByIdForDelete.StatusCode);
            Assert.IsNotNull(responseGetEmpByIdForDelete.Content);
            Assert.AreEqual("successfully! deleted Records", jsonDeleteEmp.Message);
            Assert.AreEqual("Record does not found.", jsonCheckEmpForDelete.Data);

        }
        [Test]
        public async Task PutEpmloyeeAsync()
        {
            EmployeeModel employeeModelPost = new EmployeeModel { Name = "Kevin", Salary = "78780", Age = "54" };
            var responsePostEmp = await this.PostEmployeeAsync(employeeModelPost);
            var dataResponsePostEmp = responsePostEmp.Data;
            var jsonPostEmp = JsonConvert.DeserializeObject<EmployeeModel>(dataResponsePostEmp);

            EmployeeModel employeeModelPut = new EmployeeModel { Name = "Maks", Salary = "321", Age = "54"};
            var responsePutEmp = await this.PutEmployeeAsync(jsonPostEmp.Id, employeeModelPut);
            var jsonUpdateEmp = JsonConvert.DeserializeObject<EmployeeModel>(responsePutEmp.Data);
            
            var responseGetById = await this.GetEmployeeByIdAsync(jsonPostEmp.Id);
            var jsonCheckAddEmp = JsonConvert.DeserializeObject<GetEmployeeModel>(responseGetById.Data);

            Assert.AreEqual(0, (int)responsePutEmp.StatusCode);
            Assert.IsNotNull(responsePutEmp.Content);
            CollectionAssert.AreEqual(employeeModelPut.ToString(), jsonUpdateEmp.ToString());
            Assert.AreEqual(jsonUpdateEmp.Id, jsonCheckAddEmp.Id);
            Assert.AreEqual(jsonUpdateEmp.Salary, jsonCheckAddEmp.Employee_salary);
            Assert.AreEqual(jsonUpdateEmp.Name, jsonCheckAddEmp.Employee_name);
            Assert.AreEqual(jsonUpdateEmp.Age, jsonCheckAddEmp.Employee_age);
        }
    }
}

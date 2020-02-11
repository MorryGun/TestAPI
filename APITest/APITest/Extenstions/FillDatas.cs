using APITest.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace APITest.Extenstions
{
    public class FillDatas
    {
        EmployeeModel employee = new EmployeeModel();
        public void CreateJsonObject()
        {
            //JObject jObjectBody = new JObject();
            //jObjectBody.Add(employee.employee_name, "TestName555");
            //jObjectBody.Add(employee.employee_salary, "55555");
            //jObjectBody.Add(employee.employee_age, "55");
            //jObjectBody.Add(employee.profile_image, "");

            //RestRequest restRequest = new RestRequest("/register", Method.POST);

            ////Adding Json body as parameter to the post request
            //restRequest.AddParameter("application/json", jObjectBody, ParameterType.RequestBody);
        }
    }
}

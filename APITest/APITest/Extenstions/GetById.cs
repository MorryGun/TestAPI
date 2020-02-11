using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace APITest.Extenstions
{
    public static class GetById
    {
        public static object PullDataFromListSingleId(int id)
        {
            var allEmployeesRoute = "http://dummy.restapiexample.com/api/v1/employees";
            var oneEmployeeRoute = "http://dummy.restapiexample.com/api/v1/employee/" + id;

            var client = new RestClient(allEmployeesRoute);
            var request = new RestRequest(allEmployeesRoute, Method.GET);

            var result = client.Get<object>(request);


            client = new RestClient(oneEmployeeRoute);
            request = new RestRequest(oneEmployeeRoute, Method.GET);
            request.AddCookie("PHPSESSID", result.Cookies[0].Value);
            result = client.Get<object>(request);
            return result;
        }
    }
}

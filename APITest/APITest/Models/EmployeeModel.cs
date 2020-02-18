using System;
using System.Collections.Generic;
using APITest.Constants;
using APITest.Controllers;
using APITest.Managers;
using APITest.Tests;
using Microsoft.Extensions.Configuration.Json;

namespace APITest.Models
{ 
    public class EmployeeModel
    { 
    public string Id { get; set; }
    public string Name { get; set; }
    public string Salary { get; set; }
    public string Age { get; set; }
    }
}

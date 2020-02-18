using System;
using System.Collections.Generic;
using System.Text;

namespace APITest.Models
{
    public class GetEmployeeModel
    {
        public string Id { get; set; }
        public string Employee_name { get; set; }
        public string Employee_salary { get; set; }
        public string Employee_age { get; set; }
        public string Profile_image { get; set; }
    }
}

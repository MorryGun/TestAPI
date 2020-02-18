using System;
using System.Collections.Generic;
using System.Text;

namespace APITest.Models
{
    public class PostModel
        {
            public string Status { get; set; }
            public List<EmployeeModel> Data { get; set; }
        }
}

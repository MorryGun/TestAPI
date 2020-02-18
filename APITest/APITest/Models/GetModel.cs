using System;
using System.Collections.Generic;
using System.Text;

namespace APITest.Models
{
    public class GetModel
    { 
        public string Status { get; set; }
        public List<GetEmployeeModel> Data { get; set; }
    }

}

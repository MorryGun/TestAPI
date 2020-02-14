using System;
using System.Collections.Generic;
using System.Text;

namespace APITest.Models
{
    public class FaultModel : BaseModel
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}

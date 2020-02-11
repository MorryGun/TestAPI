using System;
using System.Collections.Generic;
using System.Text;

namespace APITest.Models
{
    public class ResponseModel<T> : BaseModel
    {
        public string status { get; set; }
        public T data { get; set; } 
    }
}

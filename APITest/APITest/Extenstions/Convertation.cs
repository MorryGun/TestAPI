using APITest.Models;
using Newtonsoft.Json;
using RestSharp;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace APITest.Extenstions
{
    public class Convertation
    {
        public BaseModel ConvertDictionaryToJson(object response, BaseModel baseModel)
        {
            string jsonResult = JsonConvert.SerializeObject(response, Formatting.Indented);
            baseModel  = SimpleJson.DeserializeObject<BaseModel>(jsonResult);
            return baseModel;
        }

        public ResponseModel<ICollection<EmployeeModel>> ResponseConvertDictionaryToJson(object response)
        {
            string jsonResult = JsonConvert.SerializeObject(response, Formatting.Indented);
            
            return SimpleJson.DeserializeObject<ResponseModel<ICollection<EmployeeModel>>>(jsonResult);
        }

        public ResponseModel<EmployeeModel> EmployeeConvertDictionaryToJson(object response)
        {
            string jsonResult = JsonConvert.SerializeObject(response, Formatting.Indented);

            return SimpleJson.DeserializeObject<ResponseModel<EmployeeModel>>(jsonResult);
        }

        public FaultModel FaultConvertDictionaryToJson(object response)
        {
            string jsonResult = JsonConvert.SerializeObject(response, Formatting.Indented);

            return SimpleJson.DeserializeObject<FaultModel>(jsonResult);
        }

    }
}

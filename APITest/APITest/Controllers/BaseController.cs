using APITest.Constants;
using APITest.Managers;
using RestSharp;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.Json;
using APITest.Models;
using Newtonsoft.Json.Linq;

namespace APITest.Controllers
{
    public class BaseController : ConfigManager
    {
        protected string BaseUrl => Config[ConfigConstants.BaseUrl];
        protected RestClient RestClient => new RestClient(this.BaseUrl);

        public async Task <RestResponse<string>> GetAsync(string resource)
        {
            var request = new RestRequest(resource, Method.GET);
            return await RestClient.GetAsync<RestResponse<string>>(request);
        }
        
        protected async Task<RestResponse<string>> PostAsync(string resource, string body)
        {
            var request = new RestRequest(resource, Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", "charset = utf - 8", body, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            return await RestClient.PostAsync<RestResponse<string>>(request);
        }
        
        protected async Task<RestResponse<string>> DeleteAsync(string resource)
        {
            var request = new RestRequest(resource, Method.DELETE);
            return await RestClient.DeleteAsync<RestResponse<string>>(request);
        }
        
        protected async Task <RestResponse<string>> PutAsync(string resource, string body)
        {
            var request = new RestRequest(resource, Method.PUT);
            request.AddParameter("application/json; charset=utf-8", body, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            return await RestClient.PutAsync<RestResponse<string>>(request);
        }
    }
}

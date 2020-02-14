using APITest.Constants;
using APITest.Managers;
using RestSharp;
using System.Threading.Tasks;

namespace APITest.Controllers
{
    public class BaseController : ConfigManager
    {
        protected string BaseUrl => Config[ConfigConstants.BaseUrl];
        protected RestClient RestClient => new RestClient(this.BaseUrl);

        protected async Task<object> GetAsync(string resource)
        {
            var request = new RestRequest(resource, Method.GET);
            return await this.RestClient.GetAsync<object>(request);
        }

        protected async Task<object> ExecuteAsync(string resource, Method method)
        {
            var request = new RestRequest(resource, method);
            return await this.RestClient.ExecuteAsync<object>(request);
        }

        protected async Task<object> DeleteAsync(string resource)
        {
            var request = new RestRequest(resource, Method.DELETE);
            return await this.RestClient.DeleteAsync<object>(request);
        }

        protected async Task<object> PutAsync(string resource, string jsonEntity)
        {
            var request = new RestRequest(resource, Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(jsonEntity);
            return await this.RestClient.PutAsync<object>(request);
        }

        protected async Task<object> PostAsync(string resource, string jsonEntity)
        {
            IRestRequest request = new RestRequest(resource, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(jsonEntity);
            return await this.RestClient.PostAsync<object>(request);
        }
    }
}
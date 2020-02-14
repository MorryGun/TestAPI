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
        protected object Get(string resource)
        {
            var request = new RestRequest(resource, Method.GET);
            return this.RestClient.Get<object>(request);
        }

        protected async Task<object> PostAsync<T>(string resource, T entity)
        {
            var request = new RestRequest(resource, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(entity);
            return await this.RestClient.PostAsync<object>(request);
        }

        protected async Task<object> PutAsync<T>(string resource, T entity)
        {
            var request = new RestRequest(resource, Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(entity);
            return await this.RestClient.PutAsync<object>(request);
        }

        protected async Task<object> DeleteAsync(string resorce)
        {
            var request = new RestRequest(resorce, Method.DELETE);
            return await this.RestClient.DeleteAsync<object>(request);
        }
    }
}

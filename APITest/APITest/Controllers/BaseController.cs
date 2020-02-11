using APITest.Constants;
using APITest.Managers;
using APITest.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Cache;
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

        protected async Task<object> DeleteAsync(string resource)
        {
            var request = new RestRequest(resource, Method.DELETE);
            return await this.RestClient.DeleteAsync<object>(request);
        }

        protected async Task<object> PutAsync<T>(string resource, T entity)
        {
            var request = new RestRequest(resource, Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(entity);
            return await this.RestClient.PutAsync<object>(request);
        }

        protected async Task<object> PostAsync<T>(string resource, T entity)
        {
            IRestRequest request = new RestRequest(resource, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(entity);
            return await this.RestClient.PostAsync<object>(request);
        }
    }
}
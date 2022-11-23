using LendThingsCommonClasses.DTO;
using LendThingsMVC.Configuration;
using LendThingsMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NuGet.Protocol;
using System.Net.Http.Headers;
using System.Text;

namespace LendThingsMVC.Services
{
    public abstract class BaseModelService<B, F, C, U, D> : IBaseModelService<B, F, C, U, D>
    {
        private readonly IOptions<APIOptions> options;

        protected BaseModelService(IOptions<APIOptions> options)
        {
            this.options = options;
        }

        public abstract Task<ProcesedResponse<string>> DeleteAsync(D entity);
        public abstract bool Exists(int id);
        public abstract Task<ProcesedResponse<List<B>>> GetAllBaseAsync();
        public abstract Task<ProcesedResponse<List<F>>> GetAllFullAsync();
        public abstract Task<ProcesedResponse<F>> GetByIdAsync(int id);
        public abstract Task<ProcesedResponse<B>> SaveAsync(C entity);
        public abstract Task<ProcesedResponse<B>> UpdateAsync(U entity);

        async protected Task<HttpResponseMessage> DoGetRequestOn(string urlForRequestOnApi)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(options.Value.BaseUrl);

                // Agrega el header Accept: application/json para recibir la data como json  
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Hace la llamada
                var response = await client.GetAsync(urlForRequestOnApi);

                return response;
            }
        }

        async protected Task<HttpResponseMessage> DoPostRequestFor<T>(string urlForRequestOnApi, object body)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(options.Value.BaseUrl);

                // Agrega el header Accept: application/json para recibir la data como json  
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Hace la llamada
                var response = await client.PostAsJsonAsync(urlForRequestOnApi, body);


                return response;
            }
        }

        async protected Task<HttpResponseMessage> DoDeleteRequestFor<T>(string urlForRequestOnApi)
           where T : class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(options.Value.BaseUrl);

                // Agrega el header Accept: application/json para recibir la data como json  
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                // Hace la llamada
                var response = await client.DeleteAsync(urlForRequestOnApi);

                return response;
            }
        }

        async protected Task<HttpResponseMessage> DoPatchRequestFor<T>(string urlForRequestOnApi, object body)
            where T : class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(options.Value.BaseUrl);

                // Agrega el header Accept: application/json para recibir la data como json  
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
               
                var content = new StringContent(body.ToJson(), Encoding.UTF8, "application/json-patch+json");
                // Hace la llamada
                var response = await client.PatchAsync(urlForRequestOnApi, content);

                return response;
            }
        }

        async protected virtual Task<ProcesedResponse<T>> ProcessResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                {
                    var procesedResponse = new ProcesedResponse<T> { 
                        Body= await response.Content.ReadFromJsonAsync<T>(), 
                        Response=response
                    };
                    return procesedResponse;
                }
                else
                {
                    var procesedResponse = new ProcesedResponse<T>
                    {
                        Response = response,
                        PersonalizedError = await response.Content.ReadFromJsonAsync<string>()
                    };
                    return procesedResponse;
                }
        }
    }
}

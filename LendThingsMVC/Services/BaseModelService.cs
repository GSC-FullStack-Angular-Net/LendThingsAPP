using LendThingsMVC.Configuration;
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

        public abstract Task DeleteAsync(D entity);
        public abstract bool Exists(int id);
        public abstract Task<List<B>> GetAllBaseAsync();
        public abstract Task<List<F>> GetAllFullAsync();
        public abstract Task<F> GetByIdAsync(int id);
        public abstract Task SaveAsync(C entity);
        public abstract Task UpdateAsync(U entity);

        async protected Task<T> DoGetRequestFor<T>(string urlForRequestOnApi)
            where T : class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(options.Value.BaseUrl);

                // Agrega el header Accept: application/json para recibir la data como json  
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Hace la llamada
                var response = await client.GetAsync(urlForRequestOnApi);

                if (response.IsSuccessStatusCode)
                {
                    // Lee el response y lo deserializa
                    return await response.Content.ReadFromJsonAsync<T>();
                }
                // Sino devuelve null
                return await Task.FromResult<T>(null as T);
            }
        }

        async protected Task<T> DoPostRequestFor<T>(string urlForRequestOnApi, object body)
            where T : class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(options.Value.BaseUrl);

                // Agrega el header Accept: application/json para recibir la data como json  
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Hace la llamada
                var response = await client.PostAsJsonAsync(urlForRequestOnApi, body);
                
                if (response.IsSuccessStatusCode)
                {
                    // Lee el response y lo deserializa
                    var res= await response.Content.ReadFromJsonAsync<T>();
                }
                // Sino devuelve null
                return await Task.FromResult(null as T);
            }
        }
        async protected Task<T> DoDeleteRequestFor<T>(string urlForRequestOnApi)
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

                if (response.IsSuccessStatusCode)
                {
                    // Lee el response y lo deserializa
                    return await response.Content.ReadFromJsonAsync<T>();
                }
                // Sino devuelve null
                return await Task.FromResult<T>(null as T);
            }
        }
        async protected Task<T> DoPatchRequestFor<T>(string urlForRequestOnApi, object body)
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

                if (response.IsSuccessStatusCode)
                {
                    // Lee el response y lo deserializa
                    var res = await response.Content.ReadFromJsonAsync<T>();
                }
                // Sino devuelve null
                return await Task.FromResult(null as T);
            }
        }
    }
}

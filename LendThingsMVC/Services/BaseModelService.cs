using LendThingsMVC.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace LendThingsMVC.Services
{
    public abstract class BaseModelService<B, F, C, U, D> : IBaseModelService<B,F,C,U,D>
    {
        private readonly IOptions<APIOptions> options;

        protected BaseModelService(IOptions<APIOptions> options)
        {
            this.options = options;
        }

        public abstract void Delete(D entity);
        public abstract void DeleteAsync(D entity);
        public abstract bool Exists(int id);
        public abstract List<B> GetAllBase();
        public abstract Task<List<B>> GetAllBaseAsync();
        public abstract List<F> GetAllFull();
        public abstract Task<List<F>> GetAllFullAsync();
        public abstract F GetById(int id);
        public abstract Task<F> GetByIdAsync(int id);
        public abstract void SaveAsync(C entity);
        public abstract void UpdateAsync(U entity);

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

                // Si el servicio responde correctamente
                if (response.IsSuccessStatusCode)
                {
                    // Lee el response y lo deserializa como un Product
                    return await response.Content.ReadFromJsonAsync<T>();
                }
                // Sino devuelve null
                return await Task.FromResult<T>(null as T);
            }
        }

        async protected Task<CreatedResult> DoPostRequestFor<T>(string urlForRequestOnApi, object body)
            where T : class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(options.Value.BaseUrl);

                // Agrega el header Accept: application/json para recibir la data como json  
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                // Hace la llamada
                var response = await client.PostAsJsonAsync(urlForRequestOnApi,body);

                // Si el servicio responde correctamente
                if (response.IsSuccessStatusCode)
                {
                    // Lee el response y lo deserializa como un Product
                    return await response.Content.ReadFromJsonAsync<CreatedResult>();
                }
                // Sino devuelve null
                return await Task.FromResult(null as CreatedResult);
            }
        }
    }
}

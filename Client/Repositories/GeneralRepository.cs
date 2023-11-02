﻿using Client.Contracts;
using Newtonsoft.Json;
using System.Text;
using WebApi.Utilities.Handler;

namespace Client.Repositories
{
    public class GeneralRepository<Entity, PEntity, Tid> : IRepository<Entity, PEntity, Tid> where Entity : class where PEntity : class
    {
        protected readonly string request;
        private readonly HttpContextAccessor contextAccessor;
        protected HttpClient httpClient;

        //constructor
        public GeneralRepository(string request)
        {
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7157/api/")
            };
            //contextAccessor = new HttpContextAccessor();
            // Ini yg bawah skip dulu
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", contextAccessor.HttpContext?.Session.GetString("JWToken"));
        }

        public async Task<ResponseOkHandler<Entity>> Delete(Tid id)
        {
            ResponseOkHandler<Entity> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
            using (var response = httpClient.DeleteAsync(request + id).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOkHandler<Entity>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseOkHandler<IEnumerable<Entity>>> Get()
        {
            ResponseOkHandler<IEnumerable<Entity>> entityVM = null;
            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOkHandler<IEnumerable<Entity>>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseOkHandler<Entity>> Get(Tid id)
        {
            ResponseOkHandler<Entity> entityVM = null;
            using (var response = httpClient.GetAsync(request + id).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOkHandler<Entity>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseOkHandler<PEntity>> Post(PEntity entity)
        {
            ResponseOkHandler<PEntity> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOkHandler<PEntity>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseOkHandler<Entity>> Put(Entity entity)
        {
            ResponseOkHandler<Entity> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PutAsync(request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOkHandler<Entity>>(apiResponse);
            }
            return entityVM;
        }
    }
}

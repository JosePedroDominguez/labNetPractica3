using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using Lab.Practica.EF.Entities;
using Newtonsoft.Json;

namespace Lab.Practica.EF.Logic
{
    public class BossesService : IBossesService
    {
        private readonly HttpClient _httpClient;

        public BossesService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://eldenring.fanapis.com/api/")
            };
        }
        public List<Boss> GetBosses()
        {
            var response = _httpClient.GetAsync("bosses").Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var bosses = JsonConvert.DeserializeObject<List<Boss>>(content);
                return bosses;
            }
            else
            {
                throw new Exception("Error al obtener la lista de jefes.");
            }
        }
    }
}

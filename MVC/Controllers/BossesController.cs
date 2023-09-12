using Lab.Practica.EF.Entities;
using Lab.Practica.EF.Logic;
using MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class BossesController : Controller
    {
        private readonly string apiBaseUrl = "https://eldenring.fanapis.com/api/";

        public class ApiResponse<T>
        {
            public bool success { get; set; }
            public int count { get; set; }
            public List<T> data { get; set; }
        }
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var apiUrl = "bosses?limit=100";
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<Boss>>(jsonString);
                    var bosses = apiResponse.data;

                    return View(bosses);
                }
                else
                {
                    return View("Error");
                }
            }
        }

    }
  
}

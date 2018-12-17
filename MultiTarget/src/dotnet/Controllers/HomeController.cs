using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnet.Models;
using dotnet.Models.DataModels;
using Newtonsoft.Json;
using dotnet.Repositories;
using System.Net.Http;
using System.Net.Http.Headers;

namespace dotnet.Controllers
{
    public class HomeController : Controller
    {
        readonly IHttpClientFactoryService _httpClientFactory;

        public HomeController(IHttpClientFactoryService httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var prd = new List<Product>()
            {
                new Product(){Id=1,Name="Kalem",Price="4.99"},
                new Product(){Id=2,Name="Defter",Price="5.99"}
            };

            var client = _httpClientFactory.CreateClient();

            //client.BaseAddress=new Uri("http://localhost:5001/");
            client.DefaultRequestHeaders
      .Accept
      .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header


            HttpResponseMessage response = await client.PostAsync("create",new StringContent(content: JsonConvert.SerializeObject(value: prd)));

            if (!response.IsSuccessStatusCode)
            {
                
                RedirectToAction("About","Home");

            }

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

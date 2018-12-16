using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnet.Models;
using System.Net.Http;
using dotnet.Models.DataModels;
using Newtonsoft.Json;

namespace dotnet.Controllers
{
    public class HomeController : Controller
    {
        private static HttpClient _client = new HttpClient();


        public async Task<IActionResult> Index()
        {
            var prd = new List<Product>()
            {
                new Product(){Id=1,Name="Kalem",Price="4.99"},
                new Product(){Id=2,Name="Defter",Price="5.99"}
            };

            _client.BaseAddress=new Uri("http://localhost:5001/");
            HttpResponseMessage response = await _client.PostAsync("create",new StringContent(content: JsonConvert.SerializeObject(value: prd)));

            response.EnsureSuccessStatusCode();

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

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace WebApiVk.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();

            string json = await client.GetStringAsync(
                "https://api.vk.com/method/friends.getOnline?v=5.52&access_token=d805a7c9e56921f6e66419883844831d7636d723a710231a8834331d9fe247052d5e55d2218be54da130c");

            return View();
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebApiVk.Controllers
{
    public class VkController : Controller
    {
        public IActionResult Index()
        {
            try
            {

                

                return Ok();
            }
            catch (Exception e)
            {
                return Conflict("Ошибка! Проверьте соединение " + e.Message);
            }
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiVk.Models;

namespace WebApiVk.Controllers
{
    public class VkController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                using (FriendsContext db = new FriendsContext())
                {
                    var friends = db.Friends.ToList();

                    return Ok(friends);
                }

            }
            catch (Exception e)
            {
                return Conflict("Ошибка! Проверьте соединение " + e.Message);
            }
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using VkNet;
using WebApiVk.Models;
using VkNet.AudioBypassService.Extensions;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace WebApiVk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VkController : Controller
    {
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                using (FriendsContext db = new FriendsContext())
                {
                    List<Friend> allFriends = db.Friends.ToList();
                    List<Friend> thisMonthFriends = new List<Friend>();

                    foreach (var friend in allFriends)
                    {
                        if (friend.Birthday.Month == DateTime.Now.Month)
                        {
                            thisMonthFriends.Add(friend);
                        }
                    }

                    return Ok(thisMonthFriends);
                }
            }
            catch (Exception e)
            {
                return Conflict("Ошибка! Проверьте соединение \n" + e.Message);
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody] DataAccount account)
        {
            
            try
            {

            ServiceCollection services = new ServiceCollection();

            services.AddAudioBypass();

            VkApi api = new VkApi(services);

            // Авторизируемся для получения токена валидного для вызова методов Audio / Messages
              api.Authorize(new ApiAuthParams
              {
                  Login = account.Login,
                  Password = account.Password
              });
  
              api.Messages.Send(new MessagesSendParams
              {
                  RandomId = 123,
                  UserId = account.Id,
                  Message = "Желаю счастья в личной жизни.Пух"
              });
  
              return Ok();

            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }
    }
}

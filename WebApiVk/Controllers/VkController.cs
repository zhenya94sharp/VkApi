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
        private static List<Friend> cacheList = null;
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                using (FriendsContext db = new FriendsContext())
                {
                    if (cacheList==null)
                    {
                        cacheList = db.Friends.ToList();
                    }

                    List<Friend> thisMonthFriends = new List<Friend>();

                    foreach (var friend in cacheList)
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
        public ActionResult Post([FromBody] DataForMessage data)
        {
            try
            {
                ServiceCollection services = new ServiceCollection();

                services.AddAudioBypass();

                VkApi api = new VkApi(services);

                // Авторизируемся для получения токена валидного для вызова методов Audio / Messages
                api.Authorize(new ApiAuthParams
                {
                    Login = data.Login,
                    Password = data.Password
                });

                api.Messages.Send(new MessagesSendParams
                {
                    RandomId = 123,
                    UserId = data.Id,
                    Message = data.Message
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

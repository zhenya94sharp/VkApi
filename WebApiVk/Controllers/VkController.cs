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
    public class VkController : Controller
    {
        public IActionResult Get()
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
                return Conflict("Ошибка! Проверьте соединение \n" + e.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string json)
        {
            object data = JsonConvert.DeserializeObject(json);
            
            return Ok();
            /*
            ServiceCollection services = new ServiceCollection();

            services.AddAudioBypass();

            VkApi api = new VkApi(services);



            // Авторизируемся для получения токена валидного для вызова методов Audio / Messages
              api.Authorize(new ApiAuthParams
              {
                  Login = login,
                  Password = pass
              });
  
              api.Messages.Send(new MessagesSendParams
              {
                  RandomId = 123,
                  UserId = id,
                  Message = "Желаю счастья в личной жизни.Пух"
              });
  
              return Ok();
          }*/

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace emmaus_api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected ContentResult JsonContent(object data)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(data, jsonSettings);
            return Content(json, "application/json");
        }
    }

}

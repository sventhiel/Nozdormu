using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NameParser;

namespace Nozdormu.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class NamesController : ControllerBase
    {
        [HttpGet("name"), AllowAnonymous]
        public HumanName GetName(string name)
        {
            return new HumanName(name);
        }

        //[HttpPost("names"), AllowAnonymous]
        //public List<HumanName> GetNames([FromBody] List<string> names)
        //{
        //    var list = new List<HumanName>();

        //    foreach (var name in names)
        //    {
        //        list.Add(new HumanName(name));
        //    }
        //    return list;
        //}
    }
}
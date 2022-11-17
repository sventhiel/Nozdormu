using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NameParser;

namespace Nozdormu.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class NamesController : ControllerBase
    {
        [HttpPost("Name")]
        public HumanName GetName(string name)
        {
            return new HumanName(name);
        }

        [HttpPost("Names")]
        public List<HumanName> GetNames(List<string> names)
        {
            var list = new List<HumanName>();

            foreach (var name in names)
            {
                list.Add(new HumanName(name));
            }
            return list;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NameParser;

namespace Nozdormu.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class NamesController : ControllerBase
    {
        [HttpGet("Name")]
        public HumanName GetName(string name)
        {
            return new HumanName(name);
        }
    }
}

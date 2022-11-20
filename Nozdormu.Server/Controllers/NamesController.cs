using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NameParser;
using Newtonsoft.Json;
using Nozdormu.Library.Models.Names;

namespace Nozdormu.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class NamesController : ControllerBase
    {
        [HttpPost("Name")]
        public ReadHumanNameModel GetName([FromBody]string name)
        {
            var humanName = new HumanName(name);
            return ReadHumanNameModel.Convert(humanName);
        }

        [HttpPost("Names")]
        public List<ReadHumanNameModel> GetNames([FromBody]List<string> names)
        {
            var list = new List<ReadHumanNameModel>();

            foreach (var name in names)
            {
                var humanName = new HumanName(name);
                list.Add(ReadHumanNameModel.Convert(humanName));
            }
            return list;
        }
    }
}

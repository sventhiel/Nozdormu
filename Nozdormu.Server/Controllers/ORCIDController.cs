using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NameParser;
using Newtonsoft.Json;
using Nozdormu.Library.Models;
using Nozdormu.Library.Models.ORCID;
using Nozdormu.Server.Entities;
using Nozdormu.Server.Models;
using RestSharp;

namespace Nozdormu.Server.Controllers
{
    [Route("api")]
    [ApiController]
    //[Produces("application/xml")]
    public class ORCIDController : ControllerBase
    {
        [HttpGet("ORCID/{orcid}/Person")]
        public ReadORCIDPersonModel GetName(string orcid)
        {
            var client = new RestClient("https://pub.orcid.org/v3.0/");
            var request = new RestRequest($"{orcid}/person", Method.Get);

            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<ReadORCIDPersonModel>(response.Content);
        }
    }
}

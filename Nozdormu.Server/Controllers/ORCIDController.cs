using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nozdormu.Library.Models.ORCID;
using RestSharp;

namespace Nozdormu.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class ORCIDController : ControllerBase
    {
        [HttpGet("orcid/{orcid}/person"), AllowAnonymous]
        public ORCIDPerson GetName(string orcid)
        {
            var client = new RestClient("https://pub.orcid.org/v3.0/");
            var request = new RestRequest($"{orcid}/person", Method.Get);

            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<ORCIDPerson>(response.Content);
        }
    }
}
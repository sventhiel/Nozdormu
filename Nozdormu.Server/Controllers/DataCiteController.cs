using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using NameParser;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Nozdormu.Library.Extensions;
using Nozdormu.Library.Models;
using Nozdormu.Library.Models.ORCID;
using NuGet.Protocol;
using RestSharp;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using static System.Net.Mime.MediaTypeNames;

namespace Nozdormu.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataCiteController : ControllerBase
    {
        [HttpGet("DOI"), AllowAnonymous]
        public ReadDataCiteModel GetDOI(string doi)
        {
            var client = new RestClient("https://api.datacite.org");
            var request = new RestRequest($"dois/{doi}", Method.Get);

            //request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            //var read = ReadDataCiteModel.Deserialize(response.Content);

            //var write = CreateDataCiteModel.Deserialize(read.Serialize());
            //return read;

            return JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content);
        }

        [HttpGet("DOI2"), AllowAnonymous]
        public IActionResult GetDOI2(string doi)
        {
            var client = new RestClient("https://api.datacite.org");

            var request = new RestRequest($"dois/{doi}", Method.Get);

            request.AddHeader("Accept", "application/json");

            var response = client.Execute<dynamic>(request);

            return new JsonResult(response.Data);
        }

        [HttpPost("DOIs"), AllowAnonymous]
        public List<ReadDataCiteModel> GetDOIs([FromBody] List<string> dois)
        {
            var list = new List<ReadDataCiteModel>();

            return list;
        }
    }
}

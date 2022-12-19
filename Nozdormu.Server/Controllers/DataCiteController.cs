using LiteDB;
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
using Nozdormu.Server.Entities;
using Nozdormu.Server.Services;
using NuGet.Protocol;
using RestSharp;
using RestSharp.Authenticators;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using static System.Net.Mime.MediaTypeNames;

namespace Nozdormu.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class DataCiteController : ControllerBase
    {
        [HttpGet("datacite/{doi}"), AllowAnonymous]
        public IActionResult GetDOI(string doi)
        {
            var client = new RestClient("https://api.test.datacite.org/");
            var request = new RestRequest($"dois/{doi}", Method.Get);
            client.Authenticator = new HttpBasicAuthenticator("OODW.EQTDTN", "c0D2fMsGjmYJ_vQD5C");

            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            //var read = ReadDataCiteModel.Deserialize(response.Content);

            //var write = CreateDataCiteModel.Deserialize(read.Serialize());
            //return read;

            return Ok(ReadDataCiteModel.Deserialize(response.Content));
        }

        [HttpGet("datacite/{doi}/legacy"), AllowAnonymous]
        public IActionResult GetDOILegacy(string doi)
        {
            var client = new RestClient("https://api.datacite.org");

            var request = new RestRequest($"dois/{doi}", Method.Get);

            request.AddHeader("Accept", "application/json");

            var response = client.Execute<dynamic>(request);

            return new JsonResult(response.Data);
        }

        [HttpPost("datacite/doi"), Authorize]
        public IActionResult PostDOI(CreateDataCiteModel model)
        {
            var username = User?.Identity?.Name;

            List<ValidationResult> errors = new List<ValidationResult>();
            if (!model.Validate(out errors))
            {
                return BadRequest(errors);
            }

            var x = model.Serialize();

            return Ok(x);
        }
    }
}

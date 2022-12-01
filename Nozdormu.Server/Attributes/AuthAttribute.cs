using LiteDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nozdormu.Server.Entities;
using Nozdormu.Server.Extensions;
using Nozdormu.Server.Services;

namespace Nozdormu.Server.Attributes
{
    public class AuthAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //// appsettings.json
            //var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            //// UserService
            //var userService = new UserService(new ConnectionString(config.GetConnectionString("Nozdormu")));
            //User user = null;
            //// Token
            //var authorizationHeader = context.HttpContext.Request.GetHeader("Authorization");

            //if (authorizationHeader != null && authorizationHeader.Scheme == "Bearer" && !string.IsNullOrEmpty(authorizationHeader.Parameter))
            //{
            //    user = userService.FindByToken(authorizationHeader.Parameter);
            //}


            //var userName = context.HttpContext.User.Identity?.Name;


            // User


            //context.RouteData.Values.Add("user", user);

            //if (authorization == null)
            //{
            context.Result = new UnauthorizedResult();
            return;
            //}

            //switch (authorization.Scheme)
            //{
            //    case "Basic":
            //        return;

            //    case "Bearer":
            //        var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            //        if (authorization.Parameter.Equals(MyConfig.GetValue<string>("Credentials:Bearer")))
            //        {

            //        }
            //        return;

            //    default:
            //        context.Result = new ForbidResult();
            //        return;

            //}
        }
    }
}

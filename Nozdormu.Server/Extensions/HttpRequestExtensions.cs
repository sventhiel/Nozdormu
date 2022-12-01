using System.Net.Http.Headers;

namespace Nozdormu.Server.Extensions
{
    public static class HttpRequestExtensions
    {
        public static AuthenticationHeaderValue GetHeader(this HttpRequest request, string key)
        {
            AuthenticationHeaderValue result;

            if (AuthenticationHeaderValue.TryParse(request.Headers[key], out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CustomPortHttps
{
    public class CustomPortHttpsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly int _port;
        public CustomPortHttpsMiddleware(RequestDelegate next, int port)
        {
            if (!(port > 0)) throw new ArgumentOutOfRangeException(nameof(port), "Port must be greater than 0");
            _next = next;
            _port = port;
        }
        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.MovedPermanently)
            {
                var location = context.Response.GetTypedHeaders().Location;
#if DNX451
                string schemeHttps = Uri.UriSchemeHttps;
#else
                string schemeHttps = "https";
#endif
                if (String.Equals(location.Scheme, schemeHttps, StringComparison.OrdinalIgnoreCase))
                {
                    var builder = new UriBuilder(location);
                    builder.Port = _port;
                    context.Response.Headers["Location"] = builder.Uri.ToString();
                }

            }
        }
    }
    public static class CustomPortHttpsMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomPortHttps(this IApplicationBuilder app, int port)
        {
            return app.UseMiddleware<CustomPortHttpsMiddleware>(port);
        }
    }
}

using Microsoft.AspNetCore.Http;

namespace PolyFilms.Web
{
    public static class HttpHelper
    {
        private static IHttpContextAccessor HttpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public static HttpContext HttpContext => HttpContextAccessor.HttpContext;
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApp
{
    public class RequestHandler
    {
        public async Task HandleRequest(HttpContext context)
        {
            await context.Response.WriteAsync("Hello World");
        }
    }
}
using AnbimaConsumer.Application.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace AnbimaConsumer.Application.Infrastructure.Implementation
{
    public class HttpRepository : IHttpRepository
    {
        public HttpRepository(ILogger<HttpRepository> logger)
        {
            Logger = logger;
        }

        readonly ILogger<HttpRepository> Logger;

        public async Task<string[]> GetAsync(string url)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException exception)
            {
                Logger.LogWarning("Anbima respond with bad status code", exception);
                throw;
            }
            return (await response.Content.ReadAsStringAsync()).Split('\n');
        }
    }
}
using System.Threading.Tasks;

namespace AnbimaConsumer.Application.Infrastructure.Interfaces
{
    public interface IHttpRepository
    {
        Task<string[]> GetAsync(string url);
    }
}
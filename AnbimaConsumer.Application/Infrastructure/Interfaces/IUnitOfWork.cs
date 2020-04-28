using AnbimaConsumer.Application.Infrastructure.Implementation;
using System.Threading.Tasks;

namespace AnbimaConsumer.Application.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        EntityContext Context { get; }
        Task Commit();
    }
}

using AnbimaConsumer.Application.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace AnbimaConsumer.Application.Infrastructure.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(EntityContext context)
        {
            Context = context;
        }

        public EntityContext Context { get; }

        public async Task Commit()
        {
            await Context.SaveChangesAsync();
        }
    }
}
using AnbimaConsumer.Application.Infrastructure.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace AnbimaConsumer.Application.Infrastructure.Implementation
{
    [ExcludeFromCodeCoverage]
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
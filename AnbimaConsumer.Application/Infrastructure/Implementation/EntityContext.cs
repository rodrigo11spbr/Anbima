using AnbimaConsumer.Application.Infrastructure.FluentAPI;
using AnbimaConsumer.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AnbimaConsumer.Application.Infrastructure.Implementation
{
    public class EntityContext : DbContext
    {
        public EntityContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly IConfiguration Configuration;

        public DbSet<Anbima> Anbima { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnbimaMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
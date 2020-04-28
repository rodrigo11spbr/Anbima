using AnbimaConsumer.Application;
using AnbimaConsumer.Application.Infrastructure.Implementation;
using AnbimaConsumer.Application.Infrastructure.Interfaces;
using AnbimaConsumer.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Data.SqlClient;

namespace AnbimaConsumer.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ApplyDIScoped(services);
            ApplySwagger(services);

            services.AddControllers();
        }

        private void ApplySwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(x => x.SwaggerDoc("v1", new OpenApiInfo { Title = "Anbima", Version = "v1" }));
        }

        private void ApplyDIScoped(IServiceCollection services)
        {
            services.AddScoped<EntityContext, EntityContext>();
            services.AddScoped<IEntityRepository<Anbima>, EntityRepository<Anbima>>();
            services.AddScoped<IHttpRepository, HttpRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAnbimaApplication, AnbimaApplication>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(x => x.AllowAnyOrigin());

            ConfigureSwaggerEndpoint(app);
            ApplyMigration();

            logger.AddSeq(Configuration.GetSection("Url").GetSection("Seq").Value);

            app.UseHttpsRedirection();


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureSwaggerEndpoint(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("Swagger/v1/swagger.json", "Anbima API");
                x.RoutePrefix = string.Empty;
            });
        }

        private void ApplyMigration()
        {
            SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("SqlServer"));
            new Evolve.Evolve(connection)
            {
                IsEraseDisabled = true,
                Locations = new[] { "Migrations" },
            }.Migrate();
        }
    }
}

using AnbimaConsumer.Application.Infrastructure.Interfaces;
using AnbimaConsumer.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnbimaConsumer.Application
{
    public class AnbimaApplication : IAnbimaApplication
    {
        public AnbimaApplication(IConfiguration configuration, ILogger<AnbimaApplication> logger, IHttpRepository httpRepository, IEntityRepository<Anbima> anbimaRepository, IUnitOfWork unitOfWork)
        {
            Configuration = configuration;
            Logger = logger;
            HttpRepository = httpRepository;
            AnbimaRepository = anbimaRepository;
            UnitOfWork = unitOfWork;
        }

        readonly IConfiguration Configuration;
        readonly ILogger<AnbimaApplication> Logger;
        readonly IHttpRepository HttpRepository;
        readonly IEntityRepository<Anbima> AnbimaRepository;
        readonly IUnitOfWork UnitOfWork;

        public async Task<IEnumerable<Anbima>> GetByDate(DateTime date)
        {
            if (DateTime.Now.Date.Subtract(date).Days >= 7)
            {
                Logger.LogInformation("Date cannot exceed 7 days limit", date);
                throw new ArgumentException("Date cannot exceed 7 days limit");
            }

            string anbimaUrl = Configuration.GetSection("Url").GetSection("Anbima").Value.Replace("{date}", date.ToString("yyMMdd"));

            string[] anbimaLines = await HttpRepository.GetAsync(anbimaUrl);

            if (anbimaLines.Length < 3)
            {
                Logger.LogWarning("Anbima don't have sufficient lines", anbimaLines);
                throw new ArgumentOutOfRangeException("Anbima don't have sufficient lines");
            }

            anbimaLines = anbimaLines.Where(x => !x.StartsWith("\r") && !string.IsNullOrEmpty(x)).ToArray();
            anbimaLines = anbimaLines.Skip(2).ToArray(); // Skiping title and headers

            IEnumerable<Anbima> buildedAnbima = new Anbima().Build(anbimaLines);
            Logger.LogInformation("file was builded");

            await AnbimaRepository.Add(buildedAnbima);

            await UnitOfWork.Commit();
            Logger.LogInformation("File was save on database");

            return buildedAnbima;
        }
    }
}

using AnbimaConsumer.Application;
using AnbimaConsumer.Application.Infrastructure.Interfaces;
using AnbimaConsumer.Domain;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace AnbimaConsumer.UnitTest
{
    public class AnbimaApplicationUnitTest
    {
        readonly IConfiguration Configuration;

        public AnbimaApplicationUnitTest()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.UnitTest.json")
                .Build();

        }

        [Fact]
        public async Task WhenDatePass7Days_ShouldThrowArgumentException()
        {
            AnbimaApplication anbimaApplication = new AnbimaApplication(null, Mock.Of<ILogger<AnbimaApplication>>(), null, null, null);

            Exception exception = await Record.ExceptionAsync(() => anbimaApplication.GetByDate(DateTime.Now.AddDays(-8)));
            exception.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public async Task WhenAnbimaRespondWithLess3Lines_ShouldThrowArgumentOutOfRangeException()
        {
            Mock<IHttpRepository> httpMock = new Mock<IHttpRepository>();
            httpMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new string[2]);

            AnbimaApplication anbimaApplication = new AnbimaApplication(Configuration, Mock.Of<ILogger<AnbimaApplication>>(), httpMock.Object, null, null);
            Exception exception = await Record.ExceptionAsync(() => anbimaApplication.GetByDate(DateTime.Now.AddDays(-1)));
            exception.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public async Task WhenEverithingWorking_ShouldReturnArrayOfAnbimaFileNotEmpty()
        {
            string[] responseOfAnbima = new string[]
            {
                "ANBIMA - Associação Brasileira das Entidades dos Mercados Financeiro e de Capitais",
                "",
                "Titulo@Data Referencia@Codigo SELIC@Data Base/Emissao@Data Vencimento@Tx. Compra@Tx. Venda@Tx. Indicativas@PU@Desvio padrao@Interv. Ind. Inf. (D0)@Interv. Ind. Sup. (D0)@Interv. Ind. Inf. (D+1)@Interv. Ind. Sup. (D+1)@Criterio",
                "LTN@20200429@100000@20160708@20200701@3,1498@3,1377@3,144@994,731771@0@2,822@3,45@2,808@3,4389@Calculado",
                "LTN@20200429@100000@20180706@20201001@2,9378@2,9247@2,9316@987,692999@0,00219715725427@2,5629@3,2814@2,5506@3,2671@Calculado",
            };
            Mock<IHttpRepository> httpMock = new Mock<IHttpRepository>();
            httpMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(responseOfAnbima);

            AnbimaApplication anbimaApplication = new AnbimaApplication(Configuration, Mock.Of<ILogger<AnbimaApplication>>(), httpMock.Object, Mock.Of<IEntityRepository<Anbima>>(), Mock.Of<IUnitOfWork>());
            IEnumerable<Anbima> anbima = await anbimaApplication.GetByDate(DateTime.Now.AddDays(-1));
            anbima.Should().NotBeEmpty();
        }

    }
}

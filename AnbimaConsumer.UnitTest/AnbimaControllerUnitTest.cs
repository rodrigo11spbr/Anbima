using AnbimaConsumer.API.Controllers;
using AnbimaConsumer.Application;
using AnbimaConsumer.Domain;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AnbimaConsumer.UnitTest
{
    public class AnbimaControllerUnitTest
    {
        [Fact]
        public async Task WhenGetBydateThrowArgumentException_ShouldReturnBadRequest()
        {
            Mock<IAnbimaApplication> anbimaMock = new Mock<IAnbimaApplication>();
            anbimaMock.Setup(x => x.GetByDate(It.IsAny<DateTime>())).ThrowsAsync(new ArgumentException());

            AnbimaController anbimaController = new AnbimaController(anbimaMock.Object);
            IActionResult result = await anbimaController.GetByDate(DateTime.Now);

            (result as ObjectResult).StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task WhenGetBydateThrowHttpRequestException_ShouldReturnBadRequest()
        {
            Mock<IAnbimaApplication> anbimaMock = new Mock<IAnbimaApplication>();
            anbimaMock.Setup(x => x.GetByDate(It.IsAny<DateTime>())).ThrowsAsync(new HttpRequestException());

            AnbimaController anbimaController = new AnbimaController(anbimaMock.Object);
            IActionResult result = await anbimaController.GetByDate(DateTime.Now);

            (result as ObjectResult).StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task WhenGetBydateNotThrowAnyException_ShouldReturnBadRequest()
        {
            Mock<IAnbimaApplication> anbimaMock = new Mock<IAnbimaApplication>();
            anbimaMock.Setup(x => x.GetByDate(It.IsAny<DateTime>())).ReturnsAsync(new Anbima[1]);

            AnbimaController anbimaController = new AnbimaController(anbimaMock.Object);
            IActionResult result = await anbimaController.GetByDate(DateTime.Now);

            (result as ObjectResult).StatusCode.Should().Be(200);
        }
    }
}
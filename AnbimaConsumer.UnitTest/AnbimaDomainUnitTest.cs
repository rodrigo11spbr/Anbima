using AnbimaConsumer.Domain;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace AnbimaConsumer.UnitTest
{
    public class AnbimaDomainUnitTest
    {
        [Fact]
        public void WhenReceiveStringArray_ShouldPopulateObjectAndReturnArray()
        {
            string[] anbimaFile = new string[]
            {
                "LTN" +
                "@20200429" +
                "@100000" +
                "@20160708" +
                "@20200701" +
                "@3,1498" +
                "@3,1377" +
                "@3,144" +
                "@994,731771" +
                "@0" +
                "@2,822" +
                "@3,45" +
                "@2,808" +
                "@3,4389" +
                "@Calculado"
            };

            IEnumerable<Anbima> anbima = new Anbima().Build(anbimaFile);

            anbima.Select(x => x.Title.Should().Be("LTN"));

            DateTime.TryParseExact("20200429", "yyyyMMdd", null, DateTimeStyles.None, out DateTime referenceDate);
            anbima.Select(x => x.ReferenceDate.Should().Be(referenceDate));

            anbima.Select(x => x.SelicCode.Should().Be(Convert.ToInt64("100000")));

            DateTime.TryParseExact("20160708", "yyyyMMdd", null, DateTimeStyles.None, out DateTime emission);
            anbima.Select(x => x.Emisson.Should().Be(emission));

            DateTime.TryParseExact("20200701", "yyyyMMdd", null, DateTimeStyles.None, out DateTime dueDate);
            anbima.Select(x => x.DueDate.Should().Be(dueDate));

            anbima.Select(x => x.PurchaseTax.Should().Be(decimal.Parse("3,1498")));

            anbima.Select(x => x.SellTax.Should().Be(decimal.Parse("3,1377")));

            anbima.Select(x => x.IndicativeTax.Should().Be(decimal.Parse("3,144")));

            anbima.Select(x => x.PU.Should().Be(decimal.Parse("994,731771")));

            anbima.Select(x => x.StandardDeviation.Should().Be(decimal.Parse("0")));

            anbima.Select(x => x.IntervalMinDayzero.Should().Be(decimal.Parse("2,822")));
            anbima.Select(x => x.IntervalMinDayOne.Should().Be(decimal.Parse("3,45")));

            anbima.Select(x => x.IntervalMaxDayZero.Should().Be(decimal.Parse("2,808")));
            anbima.Select(x => x.IntervalMaxDayOne.Should().Be(decimal.Parse("3,4389")));

            anbima.Select(x => x.Status.Should().Be("Calculado"));

            anbima.Should().NotBeEmpty();
        }
    }
}
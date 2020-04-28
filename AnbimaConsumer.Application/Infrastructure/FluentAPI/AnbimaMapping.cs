using AnbimaConsumer.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnbimaConsumer.Application.Infrastructure.FluentAPI
{
    public class AnbimaMapping : IEntityTypeConfiguration<Anbima>
    {
        public void Configure(EntityTypeBuilder<Anbima> builder)
        {
            builder
                .ToTable("SELIC")
                .HasKey(x => x.ID);

            builder
                .Property(x => x.Title)
                .HasColumnName("TITLE");

            builder
                .Property(x => x.ReferenceDate)
                .HasColumnName("REFERENCE_DATE");

            builder
                .Property(x => x.SelicCode)
                .HasColumnName("SELIC_CODE");

            builder
                .Property(x => x.Emisson)
                .HasColumnName("EMISSION");

            builder
                .Property(x => x.DueDate)
                .HasColumnName("DUE_DATE");

            builder
                .Property(x => x.PurchaseTax)
                .HasColumnName("PURCHASE_TAX");

            builder
                .Property(x => x.SellTax)
                .HasColumnName("SELL_TAX");

            builder
                .Property(x => x.IndicativeTax)
                .HasColumnName("INDICATIVE_TAX");

            builder
                .Property(x => x.PU)
                .HasColumnName("PU");

            builder
                .Property(x => x.StandardDeviation)
                .HasColumnName("STANDARD_DEVIATION");

            builder
                .Property(x => x.IntervalMinDayzero)
                .HasColumnName("INTERVAL_MIN_DAY_ZERO");

            builder
                .Property(x => x.IntervalMaxDayZero)
                .HasColumnName("INTERVAL_MAX_DAY_ZERO");

            builder
                .Property(x => x.IntervalMinDayOne)
                .HasColumnName("INTERVAL_MIN_DAY_ONE");

            builder
                .Property(x => x.IntervalMaxDayOne)
                .HasColumnName("INTERVAL_MAX_DAY_ONE");

            builder
                .Property(x => x.Status)
                .HasColumnName("STATUS");
        }
    }
}
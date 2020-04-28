using System;
using System.Collections.Generic;
using System.Globalization;

namespace AnbimaConsumer.Domain
{
    public class Anbima
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public DateTime ReferenceDate { get; set; }
        public long SelicCode { get; set; }
        public DateTime Emisson { get; set; }
        public DateTime DueDate { get; set; }
        public decimal PurchaseTax { get; set; }
        public decimal SellTax { get; set; }
        public decimal IndicativeTax { get; set; }
        public decimal PU { get; private set; }
        public decimal StandardDeviation { get; set; }
        public decimal IntervalMinDayzero { get; set; }
        public decimal IntervalMaxDayZero { get; set; }
        public decimal IntervalMinDayOne { get; set; }
        public decimal IntervalMaxDayOne { get; set; }
        public string Status { get; set; }

        public IEnumerable<Anbima> Build(string[] file)
        {
            List<Anbima> taxes = new List<Anbima>();

            foreach (string line in file)
            {
                Anbima anbima = new Anbima();

                string[] columns = line.Split('@');

                anbima.ID = Guid.NewGuid();

                string title = columns[0];
                if (!title.Contains("--") && !string.IsNullOrEmpty(title))
                {
                    anbima.Title = columns[0];
                }

                string referenceDate = columns[1];
                if (DateTime.TryParseExact(referenceDate, "yyyyMMdd", null, DateTimeStyles.None, out DateTime convertedReference))
                {
                    anbima.ReferenceDate = convertedReference;
                }

                string selicCode = columns[2];
                if (long.TryParse(selicCode, out long convertedSelic))
                {
                    anbima.SelicCode = convertedSelic;
                }

                string emission = columns[3];
                if (DateTime.TryParseExact(emission, "yyyyMMdd", null, DateTimeStyles.None, out DateTime convertedEmission))
                {
                    anbima.Emisson = convertedEmission;
                }

                string dueDate = columns[4];
                if (DateTime.TryParseExact(dueDate, "yyyyMMdd", null, DateTimeStyles.None, out DateTime convertedDueDate))
                {
                    anbima.DueDate = convertedDueDate;
                }

                string purchaseTax = columns[5];
                if (decimal.TryParse(purchaseTax, out decimal convertedPurchaseTax))
                {
                    anbima.PurchaseTax = convertedPurchaseTax;
                }

                string sellTax = columns[6];
                if (decimal.TryParse(sellTax, out decimal convertedSellTax))
                {
                    anbima.SellTax = convertedSellTax;
                }

                string indicativeTax = columns[7];
                if (decimal.TryParse(indicativeTax, out decimal convertedIndicativeTax))
                {
                    anbima.IndicativeTax = convertedIndicativeTax;
                }

                string pu = columns[8];
                if (decimal.TryParse(pu, out decimal convertedPU))
                {
                    anbima.PU = convertedPU;
                }

                string standardDeviation = columns[9];
                if (decimal.TryParse(standardDeviation, out decimal convertedStandarDeviation))
                {
                    anbima.StandardDeviation = convertedStandarDeviation;
                }

                string intervalMinDayZero = columns[10];
                if (decimal.TryParse(intervalMinDayZero, out decimal convertedIntervalMinDayZero))
                {
                    anbima.IntervalMinDayzero = convertedIntervalMinDayZero;
                }

                string intervalMaxDayZero = columns[11];
                if (decimal.TryParse(intervalMaxDayZero, out decimal convertedIntervalMaxDayZero))
                {
                    anbima.IntervalMaxDayZero = convertedIntervalMaxDayZero;
                }

                string intervalMinDayOne = columns[12];
                if (decimal.TryParse(intervalMinDayOne, out decimal convertedIntervalMinDayOne))
                {
                    anbima.IntervalMinDayOne = convertedIntervalMinDayOne;
                }

                string intervalMaxDayOne = columns[13];
                if (decimal.TryParse(intervalMaxDayOne, out decimal convertedIntervalMaxDayOne))
                {
                    anbima.IntervalMaxDayOne = convertedIntervalMaxDayOne;
                }

                string status = columns[14];
                if (!status.Contains("--") && !string.IsNullOrEmpty(title))
                {
                    anbima.Status = status;
                }

                taxes.Add(anbima);
            };
            return taxes;
        }
    }
}
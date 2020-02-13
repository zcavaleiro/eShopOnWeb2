using System.Threading;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Infrastructure.Services.CurrencyService
{
    public class CurrencyServiceStatic : ICurrencyService
    {
        private static List<CurrencyGetSet> DEFAULT_RULES = new List<CurrencyGetSet>
        {
        new CurrencyGetSet { Source = Currency.EUR, Target = Currency.USD, Value = 0.9M },
        new CurrencyGetSet { Source = Currency.USD, Target = Currency.EUR, Value = 1.1M },
        };

        private readonly ICollection<CurrencyGetSet> _rules;

        public CurrencyServiceStatic(ICollection<CurrencyGetSet> rules = null)
        {
            _rules = rules ?? DEFAULT_RULES;
        }

        /// <inheritdoc />
        public Task<decimal> Convert(decimal value, Currency source, Currency target, CancellationToken cancellationToken = default)
        {
            var conversionValue = CalculatedConversionValue(source, target);
            var convertedValue = value * conversionValue;
            return Task.FromResult(convertedValue);
        }

        private decimal CalculatedConversionValue(Currency source, Currency target)
        {
            if(source == target){ return 1m;}
            var conversionRule = DEFAULT_RULES.Where(rule => rule.Source == source && rule.Target == target).FirstOrDefault();
            if (conversionRule == null) {
                throw new Exception("Conversion rule not found");
            }

            return conversionRule.Value;
        }
    }
}
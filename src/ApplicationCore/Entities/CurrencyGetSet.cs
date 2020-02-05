using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace ApplicationCore.Entities
{
    public class CurrencyGetSet
    {
        public Currency Source {get; set;}
        public  Currency Target {get; set;}
        public decimal Value {get; set;}
    }
}
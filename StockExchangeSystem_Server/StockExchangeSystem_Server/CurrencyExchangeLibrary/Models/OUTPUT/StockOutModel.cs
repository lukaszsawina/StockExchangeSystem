using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Models.OUTPUT
{
    public class StockOutModelDto
    {
        public string Symbol { get; set; }
        public decimal Value { get; set; }
        public decimal Volume { get; set; }
        public decimal ChangeWeek { get; set; }
        public decimal ChangeMonth { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Models.OUTPUT
{
    public class CurrencyOutModel
    {
        public string Symbol { get; set; }
        public decimal inUSD { get; set; }
        public decimal WeekChange { get; set; }
        public decimal MonthChange { get; set; }
    }
}

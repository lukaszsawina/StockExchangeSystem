using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Models.OUTPUT
{
    public class CryptoOutModelDto
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal Value { get; set; }
        public decimal Volume { get; set; }
        public decimal ChangeDay { get; set; }
        public decimal ChangeWeek { get; set; }
    }
}

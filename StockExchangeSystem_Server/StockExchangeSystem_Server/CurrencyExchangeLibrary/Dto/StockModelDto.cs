using CurrencyExchangeLibrary.Models.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Dto
{
    public class StockModelDto
    {
        public int ID { get; set; }
        public decimal CurrentValue { get; set; }
        public StockDataModel MetaData { get; set; }
    }
}

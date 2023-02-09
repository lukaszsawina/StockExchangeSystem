using CurrencyExchangeLibrary.Models.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Dto
{
    public class CurrencyModelDto
    {
        public int ID { get; set; }
        public decimal CurrentValue { get; set; }
        public CurrencyDataModel MetaData { get; set; }
    }
}

using CurrencyExchangeLibrary.Models.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Dto
{
    public class CryptoModelDto
    {
        public int ID { get; set; }
        public decimal CurrentValue { get; set; }
        public CryptoDataModel MetaData { get; set; }
    }
}

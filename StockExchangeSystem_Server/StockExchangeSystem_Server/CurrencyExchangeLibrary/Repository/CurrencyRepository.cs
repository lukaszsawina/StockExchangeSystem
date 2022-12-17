using CurrencyExchangeLibrary.Data;
using CurrencyExchangeLibrary.Interfaces;
using CurrencyExchangeLibrary.Models;
using CurrencyExchangeLibrary.Models.Currency;
using CurrencyExchangeLibrary.Models.OHLC;
using CurrencyExchangeLibrary.Models.OUTPUT;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly DataContext _context;
        private readonly IAPIKeyLogic _apiKey;
        private readonly int currencyAmound = 100;

        public CurrencyRepository(DataContext context, IAPIKeyLogic apiKey)
        {
            _context = context;
            _apiKey = apiKey;
        }

        public async Task<bool> CreateCurrencyAsync(string symbol)
        {

            string QUERY_URL = $"https://www.alphavantage.co/query?function=FX_DAILY&from_symbol={symbol}&to_symbol=USD&apikey={await _apiKey.GetKeyAsync()}";
            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {
                var currency = JsonConvert.DeserializeObject<CurrencyModel>(client.DownloadString(queryUri));
                JObject currencyObj = JObject.Parse(client.DownloadString(queryUri));


                int k = 0;
                foreach (var i in currencyObj.Last.First)
                {
                    if (k++ > currencyAmound)
                        break;

                    OHLCCurrencyModel newOHCL = i.First.ToObject<OHLCCurrencyModel>();
                    newOHCL.Time = DateTime.Parse(i.ToString().Split(':')[0].Replace('"', ' '));
                    newOHCL.Symbol = symbol;
                    currency.OHLCData.Add(newOHCL);                    

                }

                return await CreateAsync(currency);
            }

        }

        private async Task<bool> CreateAsync(CurrencyModel newCurrency)
        {
            await _context.Currency.AddAsync(newCurrency);
            return await SaveAsync();
        }

        public async Task<CurrencyModel> GetCurrencyAsync(string symbol)
        {
            CurrencyModel currency = new CurrencyModel();
            currency.MetaData = await _context.CurrencyData.Where(x => x.fromSymbol == symbol).FirstAsync();
            currency.OHLCData = await _context.OHLCCurrenciesData.Where(x => x.Symbol == symbol).ToListAsync();
            return currency;
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> CurrencyExistAsync(string symbol)
        {
            return await _context.Currency.AnyAsync(x => x.MetaData.fromSymbol == symbol);
        }

        public async Task<List<string>> GetCurrenciesCodesAsync()
        {
            return await _context.CurrencyData.Select(x => x.fromSymbol).ToListAsync();
        }

        public async Task<bool> UpdateCurrencyValueAsync(string symbol)
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency={symbol}&to_currency=USD&apikey={await _apiKey.GetKeyAsync()}";
            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {

                RealTimeModel currency = JsonConvert.DeserializeObject<RealTimeModel>(client.DownloadString(queryUri));

                var output = await _context.Currency.Where(s => s.MetaData.fromSymbol == symbol).FirstOrDefaultAsync();
                output.CurrentValue = currency.value.ExchangeRate;

                _context.Currency.Update(output);
                return await SaveAsync();
            }
        }

        public async Task<CurrencyOutModel> GetCurrencyOutModelAsync(string symbol)
        {
            try
            {

                var currencyData = await _context.CurrencyData.Where(x => x.fromSymbol == symbol).FirstAsync();
                var currency = await _context.Currency.Where(s => s.MetaData.fromSymbol == symbol).FirstOrDefaultAsync();
                var ohlcW = await GetOHLCFromDayAsync(symbol, DateTime.Today.AddDays(-7));
                var ohlcM = await GetOHLCFromDayAsync(symbol, DateTime.Today.AddDays(-30));

                var output = new CurrencyOutModel();
                var PLNUSD = await _context.Currency.Where(s => s.MetaData.fromSymbol == "PLN").FirstOrDefaultAsync();

                output.Symbol = currencyData.fromSymbol;
                output.inUSD = currency.CurrentValue;
                output.WeekChange = (ohlcW.CloseUSD - currency.CurrentValue) / ohlcW.CloseUSD * 100;
                output.MonthChange = (ohlcM.CloseUSD - currency.CurrentValue) / ohlcM.CloseUSD * 100;

                return output;
            }
            catch(Exception ex)
            {
                throw new Exception("no PLN in database");
            }
        }
        private async Task<OHLCCurrencyModel> GetOHLCFromDayAsync(string symbol, DateTime day)
        {
            var output = await _context.OHLCCurrenciesData.Where(s => (s.Symbol == symbol) && (s.Time == day)).FirstOrDefaultAsync();
            while(output == null)
                output = await _context.OHLCCurrenciesData.Where(s => (s.Symbol == symbol) && (s.Time == day.AddDays(-1))).FirstOrDefaultAsync();

            return output;
        }
        public async Task<List<CurrencyOutModel>> GetCurrenciesAsync()
        {
            var currencies = new List<CurrencyOutModel>();

            foreach (var c in await GetCurrenciesCodesAsync())
            {

                currencies.Add(await GetCurrencyOutModelAsync(c));
            }

            return currencies;
        }
    }
}

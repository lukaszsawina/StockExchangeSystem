using CurrencyExchangeLibrary.Data;
using CurrencyExchangeLibrary.Interfaces;
using CurrencyExchangeLibrary.Models.Crypto;
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
    public class CryptoRepository : ICryptoRepository
    {

        private readonly DataContext _context;
        private readonly IAPIKeyLogic _apiKey;

        private readonly int cryptoAmound = 100;

        public CryptoRepository(DataContext context, IAPIKeyLogic apiKey)
        {
            _context = context;
            _apiKey = apiKey;
        }
        public async Task<List<string>> GetCryptoCodesAsync()
        {
            return await _context.Crypto.Select(x=>x.MetaData.DCCode).ToListAsync();
        }
        public async Task<List<CryptoOutModel>> GetCryptosAsync()
        {
            var cryptos = new List<CryptoOutModel>();
            
            foreach(var c in await GetCryptoCodesAsync())
            {

                cryptos.Add(await GetCryptoOutput(c));
            }

            return cryptos;
        }
        private async Task<CryptoOutModel> GetCryptoOutputAsync(string symbol)
        {
            var cryptoData = await _context.CryptoData.Where(x => x.DCCode == symbol).FirstAsync();
            var crypto = await _context.Crypto.Where(s => s.MetaData.DCCode == symbol).FirstOrDefaultAsync();
            var ohlcvY = await GetOHLCVFromDayAsync(symbol, DateTime.Today.AddDays(-1));
            var ohlcvW = await GetOHLCVFromDayAsync(symbol, DateTime.Today.AddDays(-7));
            var output = new CryptoOutModel();

            output.Name = cryptoData.DCName;
            output.Symbol = cryptoData.DCCode;
            output.Value = crypto.CurrentValue;
            output.Volume = ohlcvY.Volume;
            output.ChangeDay = (ohlcvY.CloseUSD - crypto.CurrentValue) /ohlcvY.CloseUSD*100;
            output.ChangeWeek = ((ohlcvW.CloseUSD - crypto.CurrentValue) / ohlcvW.CloseUSD) * 100;

            return output;
        }
        public async Task<bool> CreateAsync(CryptoModel newCrypto)
        {
            await _context.AddAsync(newCrypto);
            return await SaveAsync();
        }
        public async Task<bool> CreateCryptoAsync(string symbol)
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=DIGITAL_CURRENCY_DAILY&symbol={symbol}&market=USD&apikey=H8T0LC277CGD8MKO";
            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {
                var crypto = JsonConvert.DeserializeObject<CryptoModel>(client.DownloadString(queryUri));
                JObject cryptoObj = JObject.Parse(client.DownloadString(queryUri));


                int k = 0;
                foreach (var i in cryptoObj.Last.First)
                {
                    if (k++ > cryptoAmound)
                        break;

                    OHLCVModel newOHCLV = i.First.ToObject<OHLCVModel>();
                    newOHCLV.Time = DateTime.Parse(i.ToString().Split(':')[0].Replace('"', ' '));
                    newOHCLV.Symbol = symbol;
                    crypto.OHLCVData.Add(newOHCLV);

                }

                return await CreateAsync(crypto);
            }
        }
        public async Task<bool> UpdateCryptoModelAsync(string symbol)
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=DIGITAL_CURRENCY_DAILY&symbol={symbol}&market=USD&apikey={await _apiKey.GetKeyAsync()}";
            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {
                var crypto = JsonConvert.DeserializeObject<CryptoModel>(client.DownloadString(queryUri));
                JObject cryptoObj = JObject.Parse(client.DownloadString(queryUri));
                DateTime latestOHLCV = (await GetLatestOHLCVAsync(symbol)).Time;

                int new_element_count = 0;
                List<OHLCVModel> elementsToAdd = new List<OHLCVModel>();
                List<OHLCVModel> elementsToRemove = new List<OHLCVModel>();

                foreach (var i in cryptoObj.Last.First)
                {
                    OHLCVModel newOHLCV = i.First.ToObject<OHLCVModel>();
                    newOHLCV.Time = DateTime.Parse(i.ToString().Split(':')[0].Replace('"', ' '));


                    if (newOHLCV.Time == latestOHLCV)
                        break;

                    new_element_count++;
                    newOHLCV.Symbol = symbol;
                    elementsToAdd.Add(newOHLCV);

                }

                await this.CreateOHCLVAsync(elementsToAdd);
                if(new_element_count > 0)
                    _context.OHLCVData.RemoveRange(_context.OHLCVData.Where(x => x.Symbol == symbol && x.Time < elementsToAdd.First().Time.AddDays(-cryptoAmound)));

                return await SaveAsync();
            }
        }
        public async Task<bool> CreateOHCLVAsync(List<OHLCVModel> newOHLC)
        {
            await _context.OHLCVData.AddRangeAsync(newOHCl);
            return await SaveAsync();
        }
        public async Task<CryptoModel> GetCryptoAsync(string symbol)
        {
            CryptoModel crypto = new CryptoModel();
            crypto.MetaData = await _context.CryptoData.Where(x => x.DCCode == symbol).FirstAsync();
            crypto.OHLCVData = await _context.OHLCVData.Where(x => x.Symbol == symbol).ToListAsync();
            return crypto;
        }
        public async Task<OHLCVModel> GetLatestOHLCVAsync(string symbol)
        {
            return await _context.OHLCVData.Where(s => s.Symbol == symbol).OrderByDescending(x=>x.Time).FirstAsync();
        }
        private async Task<OHLCVModel> GetOHLCVFromDayAsync(string symbol, DateTime day)
        {
            return await _context.OHLCVData.Where(s => (s.Symbol == symbol) && (s.Time == day)).FirstOrDefaultAsync();
        }
        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();

            return saved > 0 ? true : false;
        }
        public async Task<bool> UpdateCryptoCurrentAsync(string symbol)
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency={symbol}&to_currency=USD&apikey={await _apiKey.GetKeyAsync()}";
            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {

                RealTimeCryptoModel crypto = JsonConvert.DeserializeObject<RealTimeCryptoModel>(client.DownloadString(queryUri));

                var output = await _context.Crypto.Where(s => s.MetaData.DCCode == symbol).FirstOrDefaultAsync();
                output.CurrentValue = crypto.value.ExchangeRate;

                _context.Crypto.Update(output);
                return await SaveAsync();
            }
        }
        public async Task<bool> CryptoExistAsync(string symbol)
        {
            return await _context.Crypto.AnyAsync(x => x.MetaData.DCCode == symbol);
        }
        public async Task<CryptoModel> GetWeeklyCryptoAsync(string symbol)
        {
            CryptoModel crypto = new CryptoModel();
            crypto.MetaData = await _context.CryptoData.Where(x => x.DCCode == symbol).FirstAsync();
            List<OHLCVModel> listOfOHLCV = await _context.OHLCVData.Where(x => x.Symbol == symbol).ToListAsync();

            foreach(var i in listOfOHLCV)
            {
                if (i.Time.DayOfWeek == DayOfWeek.Sunday)
                    crypto.OHLCVData.Add(i);
            }

            if (DateTime.Today.DayOfWeek != DayOfWeek.Sunday)
            {
                crypto.OHLCVData.Add(listOfOHLCV.Last());
                listOfOHLCV.Remove(listOfOHLCV.Last());
            }

            return crypto;
        }
        public async Task<CryptoModel> GetMonthlyCryptoAsync(string symbol)
        {
            CryptoModel crypto = new CryptoModel();
            crypto.MetaData = await _context.CryptoData.Where(x => x.DCCode == symbol).FirstAsync();
            List<OHLCVModel> listOfOHLCV = await _context.OHLCVData.Where(x => x.Symbol == symbol).ToListAsync();

            foreach (var i in listOfOHLCV)
            {
                if ((int)i.Time.Day - DateTime.DaysInMonth(i.Time.Year, i.Time.Month) == 0)
                    crypto.OHLCVData.Add(i);
            }

            if ((int)DateTime.Today.Day - DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month) != 0)
            {
                crypto.OHLCVData.Add(listOfOHLCV.Last());
                listOfOHLCV.Remove(listOfOHLCV.Last());
            }



            return crypto;
        }
    }
}

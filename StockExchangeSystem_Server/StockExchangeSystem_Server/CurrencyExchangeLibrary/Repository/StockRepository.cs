using CurrencyExchangeLibrary.Data;
using CurrencyExchangeLibrary.Interfaces;
using CurrencyExchangeLibrary.Models.OHLC;
using CurrencyExchangeLibrary.Models.OUTPUT;
using CurrencyExchangeLibrary.Models.Stock;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;


namespace CurrencyExchangeLibrary.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly DataContext _context;
        private readonly IAPIKeyLogic _apiKey;

        public StockRepository(DataContext context, IAPIKeyLogic apiKey)
        {
            _context = context;
            _apiKey = apiKey;
        }

        //Get
        public async Task<List<StockOutModelDto>> GetStocksAsync()
        {
            var stocks = new List<StockOutModelDto>();

            foreach (var c in await GetStocksCodesAsync())
            {
                stocks.Add(await GetStockOutputAsync(c));
            }

            return stocks;
        }
        private async Task<StockOutModelDto> GetStockOutputAsync(string symbol)
        {
            //Pobranie wszystkich informacji z bazy o akcji
            var stockData = await _context.StockData.Where(x => x.Symbol == symbol).FirstAsync();
            var stock = await _context.Stock.Where(s => s.MetaData.Symbol == symbol).FirstAsync();
            //Pobranie ostatniej świeczki z bazy danych
            var latestOHLCV = await GetLatestOHLCVAsync(symbol);
            var ohlcvW = new OHLCVStockModel();
            var ohlcvM = new OHLCVStockModel();

            //Sprawdzenie czy dzisiaj jest któryś z dni weekendy
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                ohlcvW = await GetOHLCVFromDayAsync(symbol, DateTime.Today.AddDays(-9));
                ohlcvM = await GetOHLCVFromDayAsync(symbol, DateTime.Today.AddDays(-33));
            }
            else
            {
                ohlcvW = await GetOHLCVFromDayAsync(symbol, DateTime.Today.AddDays(-7));
                ohlcvM = await GetOHLCVFromDayAsync(symbol, DateTime.Today.AddDays(-33));
            }

            //Model do zwrócenia
            var output = new StockOutModelDto()
            {
                Symbol = stockData.Symbol,
                Value = stock.CurrentValue,
                ChangeWeek = (ohlcvW.Close - stock.CurrentValue) / ohlcvW.Close * 100,
                ChangeMonth = ((ohlcvM.Close - stock.CurrentValue) / ohlcvM.Close) * 100
            };

            return output;
        }
        public async Task<StockModel> GetStockAsync(string symbol)
        {

            StockModel stock = await _context.Stock.Where(x => x.MetaData.Symbol == symbol).FirstOrDefaultAsync();
            stock.MetaData = await _context.StockData.Where(x => x.Symbol == symbol).FirstAsync();
            stock.OHLCVData = await _context.OHLCVStockData.Where(x => x.Symbol == symbol).ToListAsync();
            return stock;
        }
        public async Task<bool> StockExistAsync(string symbol)
        {
            return await _context.Stock.AnyAsync(x => x.MetaData.Symbol == symbol);
        }
        public async Task<List<OHLCVStockModel>> GetStockOHLCVAsync(string symbol)
        {
            return await _context.OHLCVStockData.Where(x => x.Symbol == symbol).ToListAsync();
        }
        public async Task<List<OHLCVStockModel>> GetWeeklyStockOHLCVAsync(string symbol)
        {
            List<OHLCVStockModel> output = new List<OHLCVStockModel>();
            List<OHLCVStockModel> listOfOHLCV = await _context.OHLCVStockData.Where(x => x.Symbol == symbol).ToListAsync();

            foreach (var i in listOfOHLCV)
            {
                if (i.Time.DayOfWeek == DayOfWeek.Monday)
                    output.Add(i);
            }

            if (DateTime.Today.DayOfWeek != DayOfWeek.Monday)
            {
                output.Add(listOfOHLCV.Last());
                listOfOHLCV.Remove(listOfOHLCV.Last());
            }

            return output;
        }
        public async Task<List<OHLCVStockModel>> GetMonthlyStockOHLCVAsync(string symbol)
        {
            var output = new List<OHLCVStockModel>();
            List<OHLCVStockModel> listOfOHLCV = await _context.OHLCVStockData.Where(x => x.Symbol == symbol).ToListAsync();

            foreach (var i in listOfOHLCV)
            {
                if ((int)i.Time.Day - DateTime.DaysInMonth(i.Time.Year, i.Time.Month) == 0)
                    output.Add(i);
            }

            if ((int)DateTime.Today.Day - DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month) != 0)
            {
                output.Add(listOfOHLCV.Last());
                listOfOHLCV.Remove(listOfOHLCV.Last());
            }
            return output;
        }
        public async Task<OHLCVStockModel> GetLatestOHLCVAsync(string symbol)
        {
            return await _context.OHLCVStockData.Where(s => s.Symbol == symbol).OrderByDescending(x => x.Time).FirstAsync();
        }
        private async Task<OHLCVStockModel> GetOHLCVFromDayAsync(string symbol, DateTime day)
        {
            return await _context.OHLCVStockData.Where(s => (s.Symbol == symbol) && (s.Time == day)).FirstOrDefaultAsync();
        }
        public async Task<List<string>> GetStocksCodesAsync()
        {
            return await _context.Stock.Select(x => x.MetaData.Symbol).ToListAsync();
        }
        
        //Post
        public async Task<bool> CreateStockAsync(string symbol)
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol={symbol}&outputsize=full&apikey={await _apiKey.GetKeyAsync()}";
            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {
                var stock = JsonConvert.DeserializeObject<StockModel>(client.DownloadString(queryUri));
                JObject StockObj = JObject.Parse(client.DownloadString(queryUri));

                foreach (var i in StockObj.Last.First)
                {

                    OHLCVStockModel newOHCLV = i.First.ToObject<OHLCVStockModel>();
                    newOHCLV.Time = DateTime.Parse(i.ToString().Split(':')[0].Replace('"', ' '));
                    newOHCLV.Symbol = symbol;
                    if (newOHCLV.Time < DateTime.Today.AddYears(-1))
                        break;
                    stock.OHLCVData.Add(newOHCLV);

                }

                return await CreateAsync(stock);
            }
        }
        private async Task<bool> CreateAsync(StockModel newStock)
        {
            await _context.Stock.AddAsync(newStock);
            return await SaveAsync();
        }
        public async Task<bool> CreateOHLCAsync(List<OHLCVStockModel> newOHLC)
        {
            await _context.OHLCVStockData.AddRangeAsync(newOHLC);
            return await SaveAsync();
        }

        //Put
        public async Task<bool> UpdateStockCurrentAsync(string symbol)
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={await _apiKey.GetKeyAsync()}";
            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {

                GlobalQuote stock = JsonConvert.DeserializeObject<GlobalQuote>(client.DownloadString(queryUri));

                var output = await _context.Stock.Where(s => s.MetaData.Symbol == symbol).FirstOrDefaultAsync();
                output.CurrentValue = stock.ValueData.Value;

                _context.Stock.Update(output);
                return await SaveAsync();
            }
        }
        public async Task<bool> UpdateStockModelAsync(string symbol)
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol={symbol}&outputsize=compact&apikey={await _apiKey.GetKeyAsync()}";
            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {
                var stock = JsonConvert.DeserializeObject<StockModel>(client.DownloadString(queryUri));
                JObject stockObj = JObject.Parse(client.DownloadString(queryUri));
                DateTime latestOHLCV = (await GetLatestOHLCVAsync(symbol)).Time;

                int new_element_count = 0;
                List<OHLCVStockModel> elementsToAdd = new List<OHLCVStockModel>();
                List<OHLCVStockModel> elementsToRemove = new List<OHLCVStockModel>();

                foreach (var i in stockObj.Last.First)
                {
                    OHLCVStockModel newOHLCV = i.First.ToObject<OHLCVStockModel>();
                    newOHLCV.Time = DateTime.Parse(i.ToString().Split(':')[0].Replace('"', ' '));


                    if (newOHLCV.Time == latestOHLCV)
                        break;

                    new_element_count++;
                    newOHLCV.Symbol = symbol;
                    elementsToAdd.Add(newOHLCV);
                }

                if (new_element_count > 0)
                {
                    await this.CreateOHLCAsync(elementsToAdd);
                    _context.OHLCVStockData.RemoveRange(_context.OHLCVStockData.Where(x => x.Symbol == symbol && x.Time < elementsToAdd.First().Time.AddYears(-1)));
                }

                return await SaveAsync();
            }
        }
        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<List<StockOutModelDto>> GetBestStocksAsync()
        {
            var stocks = await GetStocksAsync();

            return stocks.Where(x=>x.ChangeWeek >0 || x.ChangeMonth > 0).OrderByDescending(t => t.ChangeWeek).Take(5).ToList();
        }
    }
}

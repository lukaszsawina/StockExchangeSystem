﻿using CurrencyExchangeLibrary.Data;
using CurrencyExchangeLibrary.Interfaces;
using CurrencyExchangeLibrary.Models.OHLC;
using CurrencyExchangeLibrary.Models.OUTPUT;
using CurrencyExchangeLibrary.Models.Stock;
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
    public class StockRepository : IStockRepository
    {
        private readonly DataContext _context;
        private readonly IAPIKeyLogic _apiKey;
        private readonly int stockAmound = 260;

        public StockRepository(DataContext context, IAPIKeyLogic apiKey)
        {
            _context = context;
            _apiKey = apiKey;
        }

        //Get
        public async Task<List<StockOutModel>> GetStocksAsync()
        {
            var stocks = new List<StockOutModel>();

            foreach (var c in await GetStocksCodesAsync())
            {
                stocks.Add(await GetStockOutputAsync(c));
            }

            return stocks;
        }
        private async Task<StockOutModel> GetStockOutputAsync(string symbol)
        {
            var stockData = await _context.StockData.Where(x => x.Symbol == symbol).FirstAsync();
            var stock = await _context.Stock.Where(s => s.MetaData.Symbol == symbol).FirstOrDefaultAsync();
            var latestOHLCV = await GetLatestOHLCVAsync(symbol);
            var ohlcvW = new OHLCVStockModel();
            var ohlcvM = new OHLCVStockModel();

            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                ohlcvW = await GetOHLCVFromDayAsync(symbol, DateTime.Today.AddDays(-9));
                ohlcvM = await GetOHLCVFromDayAsync(symbol, DateTime.Today.AddDays(-33));
            }
            else
            {
                ohlcvW = await GetOHLCVFromDayAsync(symbol, DateTime.Today.AddDays(-7));
                ohlcvM = await GetOHLCVFromDayAsync(symbol, DateTime.Today.AddDays(-31));
            }

            var output = new StockOutModel();

            output.Symbol = stockData.Symbol;
            output.Value = stock.CurrentValue;
            output.Volume = latestOHLCV.Volume;
            output.ChangeWeek = (ohlcvW.Close - stock.CurrentValue) / ohlcvW.Close * 100;
            output.ChangeMonth = ((ohlcvM.Close - stock.CurrentValue) / ohlcvM.Close) * 100;

            return output;
        }
        public async Task<StockModel> GetStockAsync(string symbol)
        {

            StockModel stock = new StockModel();
            stock.MetaData = await _context.StockData.Where(x => x.Symbol == symbol).FirstAsync();
            stock.OHLCVData = await _context.OHLCVStockData.Where(x => x.Symbol == symbol).ToListAsync();
            return stock;
        }
        public async Task<bool> StockExistAsync(string symbol)
        {
            return await _context.Stock.AnyAsync(x => x.MetaData.Symbol == symbol);
        }
        public async Task<StockModel> GetWeeklyStockAsync(string symbol)
        {
            StockModel stock = new StockModel();
            stock.MetaData = await _context.StockData.Where(x => x.Symbol == symbol).FirstAsync();
            List<OHLCVStockModel> listOfOHLCV = await _context.OHLCVStockData.Where(x => x.Symbol == symbol).ToListAsync();

            foreach (var i in listOfOHLCV)
            {
                if (i.Time.DayOfWeek == DayOfWeek.Monday)
                    stock.OHLCVData.Add(i);
            }

            if (DateTime.Today.DayOfWeek != DayOfWeek.Monday)
            {
                stock.OHLCVData.Add(listOfOHLCV.Last());
                listOfOHLCV.Remove(listOfOHLCV.Last());
            }

            return stock;
        }
        public async Task<StockModel> GetMonthlyStockAsync(string symbol)
        {
            StockModel stock = new StockModel();
            stock.MetaData = await _context.StockData.Where(x => x.Symbol == symbol).FirstAsync();
            List<OHLCVStockModel> listOfOHLCV = await _context.OHLCVStockData.Where(x => x.Symbol == symbol).ToListAsync();

            foreach (var i in listOfOHLCV)
            {
                if ((int)i.Time.Day - DateTime.DaysInMonth(i.Time.Year, i.Time.Month) == 0)
                    stock.OHLCVData.Add(i);
            }

            if ((int)DateTime.Today.Day - DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month) != 0)
            {
                stock.OHLCVData.Add(listOfOHLCV.Last());
                listOfOHLCV.Remove(listOfOHLCV.Last());
            }
            return stock;
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


                int k = 0;
                foreach (var i in StockObj.Last.First)
                {
                    if (k++ > stockAmound)
                        break;

                    OHLCVStockModel newOHCLV = i.First.ToObject<OHLCVStockModel>();
                    newOHCLV.Time = DateTime.Parse(i.ToString().Split(':')[0].Replace('"', ' '));
                    newOHCLV.Symbol = symbol;
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
                    _context.OHLCVStockData.RemoveRange(_context.OHLCVStockData.Where(x => x.Symbol == symbol && x.Time < elementsToAdd.First().Time.AddDays(-stockAmound)));
                }

                return await SaveAsync();
            }
        }
        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
using CurrencyExchangeLibrary.Interfaces;
using CurrencyExchangeLibrary.Models.OHLC;
using CurrencyExchangeLibrary.Repository;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictLibrary
{
    public class PredictStock
    {
        MLContext _mlContext = new MLContext();
        private readonly IStockRepository _stockRepository;

        public PredictStock(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<List<OHLCVStockModel>> predict(string symbol)
        {
            var stocks = await GetStockToPredict(symbol, 100);
            Random rnd = new Random();
            var output = new List<OHLCVStockModel>();
            var latest = await _stockRepository.GetLatestOHLCVAsync(symbol);
            for (int i = 0; i < 14; i++)
            {
                var model = Train(_mlContext, stocks);

                var predictionFunction = _mlContext.Model.CreatePredictionEngine<Data, CryptoPrediction>(model);

                var taxiTripSample = new Data()
                {
                    Time = DateTime.Now.AddDays(-i),
                    Close = 0
                };
                stocks.Add(taxiTripSample);

                var prediction = predictionFunction.Predict(taxiTripSample);
                output.Add(new OHLCVStockModel { Close = Convert.ToDecimal(prediction.Close+rnd.Next((int)Math.Round(Convert.ToSingle(latest.Close) - prediction.Close), (int)Math.Round(Convert.ToSingle(latest.Close) - prediction.Close) +300)), Time = DateTime.Now.AddDays(i+1) });
            }

            return output;

        }

        private ITransformer Train(MLContext mlContext, List<Data> cryptos)
        {
            IDataView data = _mlContext.Data.LoadFromEnumerable<Data>(cryptos);


            var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: "Close")
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "TimeEncoded", inputColumnName: "Time"))
                .Append(mlContext.Transforms.Concatenate("Features", "TimeEncoded", "Close"))
                .Append(mlContext.Regression.Trainers.FastTree());

            var model = pipeline.Fit(data);

            return model;
        }
        private class Data
        {
            [LoadColumn(0)]
            public DateTime Time { get; set; }
            [LoadColumn(1)]
            public float Close { get; set; }
        }

        private async Task<List<Data>> GetStockToPredict(string symbol, int n)
        {
            var output = new List<Data>();
            var data = await _stockRepository.GetStockAsync(symbol);
            data.OHLCVData.Reverse();

            for (int i = 0; i < n; i++)
            {
                var c = new Data
                {
                    Time = data.OHLCVData[i].Time,
                    Close = Convert.ToSingle(data.OHLCVData[i].Close)
                };
                output.Add(c);
            }
            return output;
        }
    }
}

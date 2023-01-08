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
    public class PredictCurrency
    {
        private readonly ICurrencyRepository _currencyRepository;
        MLContext _mlContext = new MLContext();

        public PredictCurrency(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<List<OHLCCurrencyModel>> predict(string symbol)
        {
            var currencies = await GetCurrencyToPredict(symbol, 100);
            Random rnd = new Random();
            var output = new List<OHLCCurrencyModel>();
            var latest = await _currencyRepository.GetLatestOHLCAsync(symbol);
            for (int i = 0; i < 14; i++)
            {
                var model = Train(_mlContext, currencies);

                var predictionFunction = _mlContext.Model.CreatePredictionEngine<Data, CryptoPrediction>(model);

                var taxiTripSample = new Data()
                {
                    Time = DateTime.Now.AddDays(-i),
                    Close = 0
                };
                currencies.Add(taxiTripSample);

                var prediction = predictionFunction.Predict(taxiTripSample);
                output.Add(new OHLCCurrencyModel { CloseUSD = Convert.ToDecimal(prediction.Close+rnd.Next((int)Math.Round(Convert.ToSingle(latest.CloseUSD) - prediction.Close), (int)Math.Round(Convert.ToSingle(latest.CloseUSD) - prediction.Close) +300)), Time = DateTime.Now.AddDays(i+1) });
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

        private async Task<List<Data>> GetCurrencyToPredict(string symbol, int n)
        {
            var output = new List<Data>();
            var data = await _currencyRepository.GetCurrencyAsync(symbol);
            data.OHLCData.Reverse();

            for (int i = 0; i < n; i++)
            {
                var c = new Data
                {
                    Time = data.OHLCData[i].Time,
                    Close = Convert.ToSingle(data.OHLCData[i].CloseUSD)
                };
                output.Add(c);
            }
            return output;
        }
    }
}

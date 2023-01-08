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
    public class PredictCrypto
    {
        private readonly ICryptoRepository _cryptoRepository;
        MLContext _mlContext = new MLContext();

        public PredictCrypto(ICryptoRepository cryptoRepository)
        {
            _cryptoRepository = cryptoRepository;
        }

        public async Task<List<OHLCVCryptoModel>> predict(string symbol)
        {
            var cryptos = await GetCryptoToPredict(symbol, 100);
            Random rnd = new Random();
            var output = new List<OHLCVCryptoModel>();
            var latest = await _cryptoRepository.GetLatestOHLCVAsync(symbol);
            for (int i = 0; i < 14; i++)
            {
                var model = Train(_mlContext, cryptos);

                var predictionFunction = _mlContext.Model.CreatePredictionEngine<CryptoData, CryptoPrediction>(model);

                var taxiTripSample = new CryptoData()
                {
                    Time = DateTime.Now.AddDays(-i),
                    Close = 0
                };
                cryptos.Add(taxiTripSample);

                var prediction = predictionFunction.Predict(taxiTripSample);
                output.Add(new OHLCVCryptoModel { CloseUSD = Convert.ToDecimal(prediction.Close+rnd.Next((int)Math.Round(Convert.ToSingle(latest.CloseUSD) - prediction.Close), (int)Math.Round(Convert.ToSingle(latest.CloseUSD) - prediction.Close) +300)), Time = DateTime.Now.AddDays(i+1) });
            }

            return output;

        }

        private ITransformer Train(MLContext mlContext, List<CryptoData> cryptos)
        {
            IDataView data = _mlContext.Data.LoadFromEnumerable<CryptoData>(cryptos);


            var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: "Close")
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "TimeEncoded", inputColumnName: "Time"))
                .Append(mlContext.Transforms.Concatenate("Features", "TimeEncoded", "Close"))
                .Append(mlContext.Regression.Trainers.FastTree());

            var model = pipeline.Fit(data);

            return model;
        }
        private class CryptoData
        {
            [LoadColumn(0)]
            public DateTime Time { get; set; }
            [LoadColumn(1)]
            public float Close { get; set; }
        }

        private async Task<List<CryptoData>> GetCryptoToPredict(string symbol, int n)
        {
            var output = new List<CryptoData>();
            var data = await _cryptoRepository.GetCryptoAsync(symbol);
            data.OHLCVCryptoData.Reverse();

            for (int i = 0; i < n; i++)
            {
                var c = new CryptoData
                {
                    Time = data.OHLCVCryptoData[i].Time,
                    Close = Convert.ToSingle(data.OHLCVCryptoData[i].CloseUSD)
                };
                output.Add(c);
            }
            return output;
        }
    }
}

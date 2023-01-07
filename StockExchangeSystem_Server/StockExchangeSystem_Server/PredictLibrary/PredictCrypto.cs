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

        public async Task predict()
        {

            var model = await Train(_mlContext);

            TestSinglePrediction(_mlContext, model);
        }

        private async Task<ITransformer> Train(MLContext mlContext)
        {
            IDataView data = _mlContext.Data.LoadFromEnumerable<CryptoData>(await GetCryptoToPredict(100));


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

        private async Task<List<CryptoData>> GetCryptoToPredict(int n)
        {
            var output = new List<CryptoData>();
            var data = await _cryptoRepository.GetCryptoAsync("BTC");
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
        private async Task Evaluate(MLContext mlContext, ITransformer model)
        {
            IDataView data = _mlContext.Data.LoadFromEnumerable<CryptoData>(await GetCryptoToPredict(10));

            var predictions = model.Transform(data);

            var metrics = mlContext.Regression.Evaluate(predictions, "Label", "Score");

            Console.WriteLine($"*       RSquared Score:      {metrics.RSquared:0.##}");
            Console.WriteLine($"*       Root Mean Squared Error:      {metrics.RootMeanSquaredError:#.##}");
        }

        private void TestSinglePrediction(MLContext mlContext, ITransformer model)
        {
            var predictionFunction = mlContext.Model.CreatePredictionEngine<CryptoData, CryptoPrediction>(model);

            var taxiTripSample = new CryptoData()
            {
                Time = DateTime.Now.AddDays(1),
                Close = 0
            };


            var prediction = predictionFunction.Predict(taxiTripSample);
            Console.WriteLine($"Predicted fare: {prediction.Close:0.####}, actual fare: 45832,01");
        }
    }
}

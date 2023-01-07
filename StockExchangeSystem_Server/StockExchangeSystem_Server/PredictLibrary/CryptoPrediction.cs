using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictLibrary
{
    public class CryptoPrediction
    {
        [ColumnName("Score")]
        public float Close;
    }
}

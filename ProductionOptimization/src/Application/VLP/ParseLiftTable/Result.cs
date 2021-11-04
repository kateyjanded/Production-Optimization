using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public class Result : IResult
    {
        public double[] FlowRates { get; set; }
        public double[] BottomHolePressures { get; set; }

        public string ReservoirPressureUnit { get; set; }
        public string BottomHolePressureUnit { get; set; }
        public string RateUnit { get; set; }
        public string WaterCutUnit { get; set; }
        public string WaterGasRatioUnit { get; set; }
        public string GasOilRatioUnit { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public interface IResult
    {
        double[] FlowRates { get; set; }
        double[] BottomHolePressures { get; set; }
        string ReservoirPressureUnit { get; set; }
        string BottomHolePressureUnit { get; set; }
        string RateUnit { get; set; }
        string WaterCutUnit { get; set; }
        string WaterGasRatioUnit { get; set; }
        string GasOilRatioUnit { get; set; }
    }
}

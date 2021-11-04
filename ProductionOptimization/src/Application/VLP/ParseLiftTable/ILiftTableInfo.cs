using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public interface ILiftTableInfo
    {
        string ContentString { get; set; }
        //double[] FlowRates { get; set; }
        double TubingHeadPressure { get; set; }
        double WaterFraction { get; set; }
        double GasFraction { get; set; }
        double ArtificialLiftQuantity { get; set; }

        string TubingHeadPressureUnit { get; set; }
        string WaterFractionUnit { get; set; }
        string GasFractionUnit { get; set; }
        string ArtificialLiftQuantityUnit { get; set; }
        string ArtificialLiftUnit { get; set; }
    }
}

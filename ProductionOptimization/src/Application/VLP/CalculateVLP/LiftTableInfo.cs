using Application.VLP.ParseLiftTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.CalculateVLP
{
    public class LiftTableInfo : ILiftTableInfo
    {
        public string ContentString { get; set; }
        //public double[] FlowRates { get; set; }
        public double TubingHeadPressure { get; set; }
        public double WaterFraction { get; set; }
        public double GasFraction { get; set; }
        public double ArtificialLiftQuantity { get; set; }

        public string TubingHeadPressureUnit { get; set; }
        public string WaterFractionUnit { get; set; }
        public string GasFractionUnit { get; set; }
        public string ArtificialLiftQuantityUnit { get; set; }
        public string ArtificialLiftUnit { get; set; }
    }
}

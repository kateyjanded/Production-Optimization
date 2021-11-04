using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public class Curve : ICurve
    {
        public double[] Pressures { get; set; }
        public double[] Rates { get; set; }
        public string PressureUnit { get; set; }
        public string RateUnit { get; set; }
        //public RateType RateType { get; set; }
    }
}

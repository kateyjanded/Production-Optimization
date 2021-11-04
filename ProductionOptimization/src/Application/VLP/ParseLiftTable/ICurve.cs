using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public interface ICurve
    {
        double[] Pressures { get; set; }
        double[] Rates { get; set; }
        string PressureUnit { get; set; }
        string RateUnit { get; set; }
        //RateType RateType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public interface IIPRTableInfo
    {
        string ContentString { get; set; }
        string RateUnit { get; set; }
        string ReservoirPressureUnit { get; set; }
        string BottomHolePressureUnit { get; set; }

    }
}

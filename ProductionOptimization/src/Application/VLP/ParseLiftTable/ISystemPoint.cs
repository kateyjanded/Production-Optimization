using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public interface ISystemPoint
    {
        double FlowRate { get; set; }
        double Pressure { get; set; }
        string FlowRateUnit { get; set; }
        string PressureUnit { get; set; }
        string Message { get; set; }
        bool Success { get; set; }
    }
}

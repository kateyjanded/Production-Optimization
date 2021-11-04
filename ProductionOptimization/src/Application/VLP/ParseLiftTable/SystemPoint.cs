using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public class SystemPoint : ISystemPoint
    {
        public double FlowRate { get; set; }
        public double Pressure { get; set; }
        public string FlowRateUnit { get; set; }
        public string PressureUnit { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; } = true;
    }
}

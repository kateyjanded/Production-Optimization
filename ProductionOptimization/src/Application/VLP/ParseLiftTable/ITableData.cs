using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public interface ITableData
    {
        List<double> Rates { get; set; }
        List<double> Pressures { get; set; }
        double AOF { get; set; }
        double TotalGOR { get; set; }
        double WaterCut { get; set; }
        double WaterGasRatio { get; set; }
        double CondensateGasRatio { get; set; }
        double ReservoirPressure { get; set; }
        double ReservoirTemperature { get; set; }
    }
}

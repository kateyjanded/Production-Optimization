using Application.Common.Interfaces;
using Application.PVT.Query;
using Domain.Entities.ModelComponents;

namespace Application.SystemAnalysisModels.Query.GetWellModel
{
    public class IPRDto: IMapFrom<IPR>
    {
        public bool UseLiftTable { get; set; }
        public ParamEntryDTO WaterFraction { get; set; }
        public ParamEntryDTO GasFraction { get; set; }
        public ParamEntryDTO ReservoirPressure { get; set; }
        public ParamEntryDTO ReservoirTemperature { get; set; }
        public ParamEntryDTO ProductivityIndex { get; set; }
        public double[] Rates { get; set; }
        public double[] Pressures { get; set; }
        public string LiftTableContent { get; set; }
        public string LiftTablePath { get; set; }
    }
}

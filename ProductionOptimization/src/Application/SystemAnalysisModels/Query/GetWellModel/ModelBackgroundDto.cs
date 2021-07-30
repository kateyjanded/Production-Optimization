using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities.ModelComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SystemAnalysisModels.Query.GetWellModel
{
    public class ModelBackgroundDto : IMapFrom<ModelBackground>
    {
        public string Description { get; set; }
        public string FluidType { get; set; }
        public string FlowType { get; set; }
        public string WellType { get; set; }
        public bool SandControl { get; set; }
        public bool TemperatureModelling { get; set; }
        public bool ArtificialLift { get; set; }
        public bool SurfaceProfileModelling { get; set; }
        public bool UseLiftTable { get; set; }
    }
}

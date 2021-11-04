using Application.Common.Interfaces;
using Application.PVT.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.UpdateVLP
{
    public class VLPDTO : IMapFrom<Domain.Entities.ModelComponents.VLP>
    {
        public ParamEntryDTO WaterFraction { get; set; }
        public ParamEntryDTO GasFraction { get; set; }
        public ParamEntryDTO THP { get; set; }
        public ParamEntryDTO GasLiftFraction { get; set; }
        public double[] Rates { get; set; }
        public double[] Pressures { get; set; }
        public string LiftTableContent { get; set; }
        public string LiftTablePath { get; set; }
    }
}

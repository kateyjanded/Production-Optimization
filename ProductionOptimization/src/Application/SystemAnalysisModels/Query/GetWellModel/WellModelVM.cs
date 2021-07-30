using Application.Common.Interfaces;
using Domain.Entities;
using System;

namespace Application.SystemAnalysisModels.Query.GetWellModel
{
    public class WellModelVM: IMapFrom<SystemAnalysisModel>
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string DrainagePointName { get; set; }
        public string FluidPropertyName { get; set; }
        public DateTime ModelDate { get; set; } 
        public string Name { get; set; }
        public ModelBackgroundDto ModelBackground { get; set; }
        public PVTDto PVT { get; set; }
        public IPRDto IPR { get; set; }
    }
}

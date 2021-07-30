using Application.Common.Interfaces;
using Application.PVT.Command;
using Application.PVT.Query;
using Domain.Common;
using Domain.Entities;
using System;

namespace Application.SystemAnalysisModels.Query.GetWellModel
{
    public class PVTDto: IMapFrom<Domain.Entities.ModelComponents.PVT>
    {
        public Guid Id { get; set; }
        public string FluidType { get; set; }
        public double OilGravity { get; set; }
        public double GasGravity { get; set; }
        public ParamEntryDTO GasRatio { get; set; }
        public ParamEntryDTO Temperature { get; set; }
        public ParamEntryDTO WaterSalinity { get; set; }
        public ParamEntryDTO Pressure { get; set; }
        public double C02 { get; set; }
        public double H2S { get; set; }
        public double N2 { get; set; }
        public string RSBO { get; set; }
        public string UO { get; set; }
        public string BlackOilModel { get; set; }
        public Guid SystemAnalysisID { get; set; }
    }
}
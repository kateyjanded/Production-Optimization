using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.ModelComponents
{
    public class PVT:AuditableEntity
    {
        public PVT()
        {
            FluidType = "Oil";
        }
        public virtual string FluidType { get; set; }
        public virtual double OilGravity { get; set; }
        public double GasGravity { get; set; }
        public virtual ParamEntry GasRatio { get; set; }
        public virtual ParamEntry Temperature { get; set; }
        public virtual ParamEntry WaterSalinity { get; set; }
        public virtual ParamEntry Pressure { get; set; }
        public virtual double GasViscosity { get; set; }
        public virtual double C02 { get; set; }
        public virtual double H2S { get; set; }
        public virtual double N2 { get; set; }
        public virtual string RSBO { get; set; }
        public virtual string UO { get; set; }
        public virtual string BlackOilModel { get; set; }
        public virtual Guid SystemAnalysisID { get; set; }
        [ForeignKey("SystemAnalysisID")]
        public virtual SystemAnalysisModel SystemAnalysisModel { get; set; }
    }
}

using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.ModelComponents
{
    public class IPR: AuditableEntity
    {
        public IPR()
        {
            WaterFraction = new ParamEntry();
            GasFraction = new ParamEntry();
            ReservoirPressure = new ParamEntry();
            ReservoirTemperature = new ParamEntry();
            ProductivityIndex = new ParamEntry();
        }
        public bool UseLiftTable { get; set; }
        public ParamEntry WaterFraction { get; set; }
        public ParamEntry GasFraction { get; set; }
        public ParamEntry ReservoirPressure { get; set; }
        public ParamEntry ReservoirTemperature { get; set; }
        public ParamEntry ProductivityIndex { get; set; }
        public string LiftTableContent { get; set; }
        public string LiftTablePath { get; set; }
        public double[] Rates { get; set; }
        public double[] Pressures { get; set; }
        public Guid SystemAnalysisModelId { get; set; }
        [ForeignKey("SystemAnalysisModelId")]
        public virtual SystemAnalysisModel SystemAnalysisEOIModel { get; set; }
    }
}

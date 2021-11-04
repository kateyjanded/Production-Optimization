using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.ModelComponents
{
    public class VLP : AuditableEntity
    {
        public VLP()
        {
            WaterFraction = new ParamEntry();
            GasFraction = new ParamEntry();
            THP = new ParamEntry();
            GasLiftFraction = new ParamEntry();
        }
        public bool UseLiftTable { get; set; }
        public ParamEntry WaterFraction { get; set; }
        public ParamEntry GasFraction { get; set; }
        public ParamEntry THP { get; set; }
        public ParamEntry GasLiftFraction { get; set; }
        public string LiftTableContent { get; set; }
        public string LiftTablePath { get; set; }
        public double[] Rates { get; set; }
        public double[] Pressures { get; set; }
        public Guid SystemAnalysisModelId { get; set; }
        [ForeignKey("SystemAnalysisModelId")]
        public virtual SystemAnalysisModel SystemAnalysisEOIModel { get; set; }
    }
}

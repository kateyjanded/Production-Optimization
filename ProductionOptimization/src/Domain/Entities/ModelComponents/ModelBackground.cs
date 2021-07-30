using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.ModelComponents
{
    public class ModelBackground: AuditableEntity
    {
        public virtual string Description { get; set; } = "";
        public virtual string ModelDate { get; set; } = DateTime.Today.ToString("d/MM/yyyy");
        public virtual FluidTypeEnum FluidType { get; set; }
        public virtual FlowTypeEnum FlowType { get; set; }
        public virtual string WellType { get; set; }
        public virtual bool SandControl { get; set; }
        public virtual bool TemperatureModelling { get; set; }
        public virtual bool ArtificialLift { get; set; }
        public virtual bool SurfaceProfileModelling { get; set; }
        public virtual bool UseLiftTable { get; set; }
        public Guid SystemAnalysisModelId { get; set; }
        [ForeignKey("SystemAnalysisModelId")]
        public virtual SystemAnalysisModel SystemAnalysisEOIModel { get; set; }
    }
}

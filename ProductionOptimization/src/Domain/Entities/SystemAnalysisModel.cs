using Domain.Common;
using Domain.Entities.ModelComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class SystemAnalysisModel: AuditableEntity
    {
        public virtual string DrainagePointName { get; set; }
        public virtual string FluidPropertyName { get; set; }
        public virtual DateTime ModelDate { get; set; }
        public virtual string Name { get; set; }
        public virtual ModelBackground ModelBackground { get; set; }
        public virtual IPR IPR { get; set; }
        public virtual PVT PVT { get; set; }
        //public virtual IList<GeothermalGradient> GeoThermalGradient { get; set; }
       // public virtual SandControlData SandControlData { get; set; }
        //public virtual VLP VLP { get; set; }
        //public virtual VLPCurve VLPCurve { get; set; }
        //public virtual FLP FLP { get; set; }
       // public virtual FLPCurve FLPCurve { get; set; }
        //public virtual IPRCurve IPRCurve { get; set; }
        //public virtual VLPIPRCurve VLPIPRCurve { get; set; }
        //public virtual IList<SurfaceData> SurfaceData { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsSelected { get; set; }
    }
}

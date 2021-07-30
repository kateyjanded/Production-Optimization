﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ModelComponents
{
    public class GeothermalGradient
    {
        public virtual ParamEntry SurfaceTemperature { get; protected set; }
    }
}
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Extensions
{
    public static class StringToFluidTypeEnumExtension
    {
        public static FluidTypeEnum ConvertToEnum(this string flowTypeEnum)
        {
            switch (flowTypeEnum)
            {
                case "Oil":
                    return FluidTypeEnum.Oil;
                case "Gas":
                    return FluidTypeEnum.Hydrocarbon;
                case "HydroCarbon":
                    return FluidTypeEnum.Hydrocarbon;
                case "Water":
                    return FluidTypeEnum.Water;
                case "GasCondensate":
                    return FluidTypeEnum.GasCondensate;
                default:
                    return FluidTypeEnum.Oil;
            }
        }
    }
}

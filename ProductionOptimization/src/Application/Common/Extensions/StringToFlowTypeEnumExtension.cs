using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Extensions
{
    public static class StringToFlowTypeEnumExtension
    {
        public static FlowTypeEnum ConvertToFlowTypeEnum(this string flowTypeEnum)
        {
            switch (flowTypeEnum)
            {
                case "Annular":
                    return FlowTypeEnum.Annular;
                case "Tubular":
                    return FlowTypeEnum.Tubing;
                default:
                    return FlowTypeEnum.Annular;
            }
        }
    }
}

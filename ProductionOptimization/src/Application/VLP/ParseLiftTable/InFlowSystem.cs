using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public class InFlowSystem : FlowSystem
    {
        public double MaxPressure
        {
            get
            {
                return ((IPRTable)components[0]).MaxPressure;
            }
        }

        public double MinPressure
        {
            get
            {
                return ((IPRTable)components[0]).MinPressure;
            }
        }

        public double AOF
        {
            get
            {
                return ((IPRTable)components[0]).AOF;
            }
        }

        public double WaterCut
        {
            get
            {
                return ((IPRTable)components[0]).WaterCut;
            }
        }

        public double TotalGOR
        {
            get
            {
                return ((IPRTable)components[0]).TotalGOR;
            }
        }

        public double WaterGasRatio { get => ((IPRTable)components[0]).WaterGasRatio; }
        public double CondensateGasRatio { get => ((IPRTable)components[0]).CondensateGasRatio; }
        public string RateUnit { get => ((IPRTable)components[0]).RateUnit; }
        public string BottomHolePressureUnit { get => ((IPRTable)components[0]).BottomHolePressureUnit; }
        public string FlowRatesUnit { get => RateUnit; }
        public string BottomHolePressuresUnit { get => BottomHolePressureUnit; }
        public double ReservoirPressure
        {
            get
            {
                return ((IPRTable)components[0]).ReservoirPressure;
            }
        }

        public double ReservoirTemperature
        {
            get
            {
                return ((IPRTable)components[0]).ReservoirTemperature;
            }
        }
        public InFlowSystem(IPRTable iprTable) : base()
        {
            components.Add(iprTable);
        }

        public InFlowSystem(IPRTable iprTable, LiftTable liftTable)
        {
            components.Add(iprTable);
            components.Add(liftTable);
        }

        public double BHP(double rate)
        {
            return components[0].BHP(rate);
        }

        public double Rate(double pressure)
        {
            return components[0].Rate(pressure);
        }

        public IResult InFlowCurve()
        {
            return ((IPRTable)components[0]).IPRCurve();
        }

        public ICurve GetInflowCurve()
        {
            return ((IPRTable)components[0]).IPRCurve(1);
        }
    }
}

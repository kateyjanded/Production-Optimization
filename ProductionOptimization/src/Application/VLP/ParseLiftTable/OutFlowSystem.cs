using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public class OutFlowSystem : FlowSystem
    {
        public string RateUnit { get => ((LiftTable)components[0]).RateUnit; }
        public string THPUnit { get => ((LiftTable)components[0]).THPUnit; }
        public string ArtificialLiftUnit { get => ((LiftTable)components[0]).ArtificialLiftUnit; }
        public string BottomHolePressuresUnit { get => THPUnit; }
        public string FlowRatesUnit { get => RateUnit; }
        public OutFlowSystem(LiftTable liftTable) : base()
        {
            components.Add(liftTable);
        }

        public OutFlowSystem(IPRTable iprTable, LiftTable liftTable)
        {
            components.Add(iprTable);
            components.Add(liftTable);
        }
        public IResult OutFlowCurve()
        {
            return ((LiftTable)components[0]).OutFlowCurve();
        }
        public Dictionary<string, string> QuantitiesAndUnits()
        {
            return ((LiftTable)components[0]).QuantitiesAndUnits;
        }
        public List<TableColumn> Quantities()
        {
            return ((LiftTable)components[0]).Quantities;
        }
        public double BHP(double rate)
        {
            return components[0].BHP(rate);
        }
        public double Rate(double pressure)
        {
            return 0.0;
        }

        public ICurve GetOutflowCurve(double thp, double wfr, double gfr, double alq)
        {
            return ((LiftTable)components[0]).GetOutflowCurve(thp, wfr, gfr, alq);
        }

        public void SetUnits(string thpUnit, string wfrUnit, string gfrUnit, string alqUnit)
        {
            ((LiftTable)components[0]).SetUnits(thpUnit, wfrUnit, gfrUnit, alqUnit);
        }
    }
}

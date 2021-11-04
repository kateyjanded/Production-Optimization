using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public class LiftTable : IComponent
    {
        private VFPTable table;

        public double TubingHeadPressure { get; set; } = -1.0;
        public double WaterFraction { get; set; } = -1.0;
        public double GasFraction { get; set; } = -1.0;
        public double ArtificialLiftQuantity { get; set; } = -1.0;
        public string RateUnit { get => table.Unit("FLO"); }
        public string THPUnit { get => table.Unit("THP"); }
        public string ArtificialLiftUnit { get => table.Unit("ALQ"); }
        private string THPInputUnit;
        private string WFRInputUnit;
        private string GFRInputUnit;
        private string ALQInputUnit;
        public Dictionary<string, string> QuantitiesAndUnits
        {
            get
            {
                return table.QuantitiesAndUnits;
            }
        }

        public List<TableColumn> Quantities
        {
            get
            {
                return table.Quantities;
            }
        }
        public double[] FlowRates
        {
            get
            {
                return table.FlowRates;
            }
        }


        public LiftTable(ILiftTableInfo liftTableInfo)
        {
            var parser = new VFPTableParser();
            table = parser.ParseVFPTable(liftTableInfo.ContentString);

            // Deal with THP and convert input unit to lifttable unit
            var THPLiftableUnit = table.QuantitiesAndUnits["THP"];
            THPInputUnit = liftTableInfo.TubingHeadPressureUnit;
            if (THPLiftableUnit.ToLower() == THPInputUnit.ToLower())
            {
                TubingHeadPressure = liftTableInfo.TubingHeadPressure;
            }
            else if (THPLiftableUnit.ToLower() == "psia" && THPInputUnit.ToLower() == "psig")
            {
                TubingHeadPressure = liftTableInfo.TubingHeadPressure + 14.7;
            }
            else if (THPLiftableUnit.ToLower() == "psig" && THPInputUnit.ToLower() == "psia")
            {
                TubingHeadPressure = liftTableInfo.TubingHeadPressure - 14.7;
            }
            else
            {
                throw new Exception("TubingHeadPressure must be in Psia or Psig");
            }

            // Handle Water Fraction
            if (table.AxisExist("WFR"))
            {
                string WFRLifttableUnit = table.Unit("WFR");
                WFRInputUnit = liftTableInfo.WaterFractionUnit;
                if (WFRInputUnit == null || WFRInputUnit == "")
                {
                    throw new Exception("No unit entered for Water Fraction");
                }

                if (WFRLifttableUnit.ToLower() == WFRInputUnit.ToLower())
                {
                    WaterFraction = liftTableInfo.WaterFraction;
                }
                else
                {
                    var ConvFactor = UnitConverter.Factor[WFRInputUnit][WFRLifttableUnit];
                    WaterFraction = liftTableInfo.WaterFraction * ConvFactor;
                }
            }
            // Handle Gas Fraction
            if (table.AxisExist("GFR"))
            {
                string GFRLifttableUnit = table.Unit("GFR");
                GFRInputUnit = liftTableInfo.GasFractionUnit;
                if (GFRInputUnit == null || GFRInputUnit == "")
                {
                    throw new Exception("No unit entered for Gas Fraction");
                }

                if (GFRLifttableUnit.ToLower() == GFRInputUnit.ToLower())
                {
                    GasFraction = liftTableInfo.GasFraction;
                }
                else
                {
                    var ConvFactor = UnitConverter.Factor[GFRInputUnit][GFRLifttableUnit];
                    GasFraction = liftTableInfo.GasFraction * ConvFactor;
                }
            }
            // Handle Artificial Lift Quantity
            if (table.AxisExist("ALQ"))
            {
                string ALQLifttableUnit = table.Unit("ALQ");
                ALQInputUnit = liftTableInfo.ArtificialLiftQuantityUnit;
                if (ALQInputUnit == null || ALQInputUnit == "")
                {
                    throw new Exception("No unit entered for Water Fraction");
                }

                if (ALQLifttableUnit.ToLower() == ALQInputUnit.ToLower())
                {
                    ArtificialLiftQuantity = liftTableInfo.ArtificialLiftQuantity;
                }
                else
                {
                    var ConvFactor = UnitConverter.Factor[ALQInputUnit][ALQLifttableUnit];
                    ArtificialLiftQuantity = liftTableInfo.ArtificialLiftQuantity * ConvFactor;
                }
            }
        }

        public double BHP(double rate)
        {
            return table.GetBHP(rate, TubingHeadPressure, WaterFraction,
                                GasFraction, ArtificialLiftQuantity);
        }
        public double THP(double rate)
        {
            return 0.0;
        }
        public double Rate(double pressure)
        {
            return 0.0;
        }
        public IResult OutFlowCurve()
        {
            var bhps = new double[FlowRates.Length];

            for (int i = 0; i < FlowRates.Length; ++i)
            {
                var flowRate = FlowRates[i];

                bhps[i] = table.GetBHP(flowRate, TubingHeadPressure, WaterFraction,
                                GasFraction, ArtificialLiftQuantity);
            }
            return new Result
            {
                FlowRates = FlowRates,
                BottomHolePressures = bhps
            };
        }

        public ICurve GetOutflowCurve(double thp, double wfr, double gfr, double alq)
        {
            var res = Convert(thp, wfr, gfr, alq);
            thp = res.Item1;
            wfr = res.Item2;
            gfr = res.Item3;
            alq = res.Item4;
            var bhps = new double[FlowRates.Length];

            for (int i = 0; i < FlowRates.Length; ++i)
            {
                var flowRate = FlowRates[i];
                bhps[i] = table.GetBHP(flowRate, thp, wfr, gfr, alq);
            }
            return new Curve
            {
                Rates = FlowRates,
                Pressures = bhps,
                RateUnit = RateUnit,
                PressureUnit = THPUnit
            };
        }

        public void SetUnits(string thpUnit, string wfrUnit, string gfrUnit, string alqUnit)
        {
            if (thpUnit != null || thpUnit != "") THPInputUnit = thpUnit;
            if (wfrUnit != null || wfrUnit != "") WFRInputUnit = wfrUnit;
            if (gfrUnit != null || gfrUnit != "") GFRInputUnit = gfrUnit;
            if (alqUnit != null || alqUnit != "") ALQInputUnit = alqUnit;
        }
        private Tuple<double, double, double, double> Convert(double thp, double wfr, double gfr, double alq)
        {
            var thpc = thp;
            var wfrc = wfr;
            var gfrc = gfr;
            var alqc = alq;

            // Deal with THP and convert input unit to lifttable unit
            var THPLiftableUnit = table.QuantitiesAndUnits["THP"];

            var pressUnitList = new List<string> { "psia", "psig" };
            if (pressUnitList.Contains(THPInputUnit.ToLower()))
            {
                if (THPLiftableUnit.ToLower() == "psia" && THPInputUnit.ToLower() == "psig")
                {
                    thpc = thp + 14.7;
                }
                else if (THPLiftableUnit.ToLower() == "psig" && THPInputUnit.ToLower() == "psia")
                {
                    thpc = thp - 14.7;
                }
            }
            else
            {
                throw new Exception("TubingHeadPressure must be in Psia or Psig");
            }

            // Handle Water Fraction
            if (wfr >= 0.0)
            {
                if (table.AxisExist("WFR"))
                {
                    string WFRLifttableUnit = table.Unit("WFR");
                    if (WFRInputUnit == null || WFRInputUnit == "")
                    {
                        throw new Exception("No unit entered for Water Fraction");
                    }

                    if (WFRLifttableUnit.ToLower() != WFRInputUnit.ToLower())
                    {
                        var ConvFactor = UnitConverter.Factor[WFRInputUnit][WFRLifttableUnit];
                        wfrc = wfr * ConvFactor;
                    }
                }
            }

            // Handle Gas Fraction
            if (gfr >= 0.0)
            {
                if (table.AxisExist("GFR"))
                {
                    string GFRLifttableUnit = table.Unit("GFR");
                    if (GFRInputUnit == null || GFRInputUnit == "")
                    {
                        throw new Exception("No unit entered for Gas Fraction");
                    }

                    if (GFRLifttableUnit.ToLower() != GFRInputUnit.ToLower())
                    {
                        var ConvFactor = UnitConverter.Factor[GFRInputUnit][GFRLifttableUnit];
                        gfrc = gfr * ConvFactor;
                    }
                }
            }

            // Handle Artificial Lift Quantity
            if (alq >= 0.0)
            {
                if (table.AxisExist("ALQ"))
                {
                    string ALQLifttableUnit = table.Unit("ALQ");
                    if (ALQInputUnit == null || ALQInputUnit == "")
                    {
                        throw new Exception("No unit entered for Artificial lift quantity");
                    }

                    if (ALQLifttableUnit.ToLower() != ALQInputUnit.ToLower())
                    {
                        var ConvFactor = UnitConverter.Factor[ALQInputUnit][ALQLifttableUnit];
                        alqc = alq * ConvFactor;
                    }
                }
            }

            return new Tuple<double, double, double, double>(thpc, wfrc, gfrc, alqc);

        }
    }
}

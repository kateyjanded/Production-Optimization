using MathematicsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public class ProductionSystem : IProductionSystem
    {
        public OutFlowSystem OutFlowSystem { get; set; }
        public InFlowSystem InFlowSystem { get; set; }


        public ProductionSystem(IIPRTableInfo iprTableInfo)
        {
            var iprTable = new IPRTable(iprTableInfo);
            InFlowSystem = new InFlowSystem(iprTable);
        }

        public ProductionSystem(ILiftTableInfo liftTableInfo)
        {
            var liftTable = new LiftTable(liftTableInfo);
            OutFlowSystem = new OutFlowSystem(liftTable);
        }

        public ProductionSystem(IIPRTableInfo iprTableInfo, ILiftTableInfo liftTableInfo)
        {
            var iprTable = new IPRTable(iprTableInfo);
            InFlowSystem = new InFlowSystem(iprTable);

            var liftTable = new LiftTable(liftTableInfo);

            OutFlowSystem = new OutFlowSystem(liftTable);
        }

        public IResult InFlowCurve()
        {
            if (InFlowSystem == null)
            {
                throw new Exception("No inflow object found!");
            }
            return InFlowSystem.InFlowCurve();
        }

        public IResult OutFlowCurve()
        {
            if (OutFlowSystem == null)
            {
                throw new Exception("No outflow object found!");
            }
            return OutFlowSystem.OutFlowCurve();
        }

        public ICurve GetInflowCurve()
        {
            if (InFlowSystem == null)
            {
                throw new Exception("No inflow object found!");
            }
            return InFlowSystem.GetInflowCurve();
        }


        public ICurve GetOutflowCurve(double thp, double wfr, double gfr, double alq)
        {
            return OutFlowSystem.GetOutflowCurve(thp, wfr, gfr, alq);
        }

        public ICurve GetOutflowCurve(double thp, double wfr, double gfr)
        {
            return OutFlowSystem.GetOutflowCurve(thp, wfr, gfr, -1.0);
        }
        public void SetUnits(string thpUnit, string wfrUnit, string gfrUnit, string alqUnit)
        {
            OutFlowSystem.SetUnits(thpUnit, wfrUnit, gfrUnit, alqUnit);
        }

        public void SetUnits(string thpUnit, string wfrUnit, string gfrUnit)
        {
            OutFlowSystem.SetUnits(thpUnit, wfrUnit, gfrUnit, null);
        }

        public ISystemPoint ComputeSystemPoint()
        {
            //double p = (InFlowSystem.MinPressure + InFlowSystem.MaxPressure) / 2;
            double p = 4000;
            double Q = 0;
            double pcalc = 0;
            int i;

            int MaxIter = 50;

            for (i = 0; i < MaxIter; ++i)
            {
                Q = InFlowSystem.Rate(p);
                pcalc = OutFlowSystem.BHP(Q);
                if (Math.Abs(pcalc - p) / p < 1e-4)
                {
                    break;
                }
                p = pcalc;

            }

            if (i >= MaxIter)
            {
                throw new Exception("No convergence after 50 iterations");
            }

            var res = new SystemPoint
            {
                FlowRate = Q,
                Pressure = pcalc
            };

            return res;
        }


        public ColVec ComputeSystemPoint1()
        {
            var p0 = (InFlowSystem.MinPressure + InFlowSystem.MaxPressure) / 2;
            var Q0 = 2000;
            var x0 = new ColVec(p0, Q0);

            Func<ColVec, ColVec> fun = (ColVec x) => Residual(x);
            return Solvers.FSolve(fun, x0);
        }

        public ISystemPoint ComputeSystemPoint2()
        {
            var Qstart = OutFlowCurve().FlowRates.Min();

            //
            Func<double, double> fun = (double q) => InFlowSystem.BHP(q) - OutFlowSystem.BHP(q);
            var DF = new Derivative(fun);
            // Ordinary newton-raphson
            int MaxIter = 20;
            var Tol = 1e-4;
            var Q = 50.0;
            int i;

            for (i = 0; i < MaxIter; ++i)
            {
                var Qold = Q;
                var res = DF.Evaluate(Qold);
                var f = res[0];
                var df = res[1];
                var d = -f / df;
                Q += d;
                if (Math.Abs(Q - Qold) / Qold < Tol)
                {
                    break;
                }
            }

            return new SystemPoint
            {
                FlowRate = Q,
                Pressure = InFlowSystem.BHP(Q)
            };
        }


        public ISystemPoint ComputeSystemPoint3()
        {
            var Qstart = OutFlowCurve().FlowRates.Min();

            //
            Func<double, double> fun = (double q) => InFlowSystem.BHP(q) - OutFlowSystem.BHP(q);
            var DF = new Derivative(fun);
            // Ordinary newton-raphson
            var df = new Derivative(fun);
            var Q = ScalarSolvers.ScalarSolvers.NewtonBracket(fun, df, 500.0, 5000.0);

            return new SystemPoint
            {
                FlowRate = Q,
                Pressure = InFlowSystem.BHP(Q)
            };
        }


        public ISystemPoint ComputeSystemPoint4()
        {
            var Qstart = OutFlowCurve().FlowRates.Min();

            // Get rate units from IPR and LiftTable
            string iprRateUnit = InFlowSystem.RateUnit;
            string vlpRateUnit = OutFlowSystem.RateUnit;
            var convFactor = UnitConverter.Factor[vlpRateUnit][iprRateUnit];

            // Get pressure units
            string iprPressureUnit = InFlowSystem.BottomHolePressureUnit;
            string vlpPressureUnit = OutFlowSystem.THPUnit;


            Func<string, string, double, double> makeIprPressureConsistent =
                (string iprPUnit, string vlpPUnit, double pwfIpr) =>
                {
                    bool pUnitEqual = iprPUnit.ToLower() == vlpPUnit.ToLower();

                    if (!pUnitEqual)
                    {
                        if (iprPressureUnit.ToLower() == "psig" && vlpPressureUnit.ToLower() == "psia")
                        {
                            pwfIpr += 14.7;
                        }
                        else if (iprPressureUnit.ToLower() == "psia" && vlpPressureUnit.ToLower() == "psig")
                        {
                            pwfIpr -= 14.7;
                        }
                        else
                        {
                            throw new Exception("Pressure must be in psia or psig");
                        }
                    }

                    return pwfIpr;
                };

            // We make vlp table units the reference unit


            Func<double, double> fun = (double q) =>
            {
                var qIpr = q * convFactor; // convert to IPR rate unit
                var pwfIpr = InFlowSystem.BHP(qIpr);

                // ensure pwf unit from ipr is same as that from vlp b4 finding residual
                pwfIpr = makeIprPressureConsistent(iprPressureUnit, vlpPressureUnit, pwfIpr);
                return pwfIpr - OutFlowSystem.BHP(q);
            };

            Func<double, double> f = (double q) => OutFlowSystem.BHP(q);
            var DF = new Derivative(f);

            // Ordinary newton-raphson
            //var df = new Derivative(fun);

            var convFactor1 = UnitConverter.Factor[iprRateUnit][vlpRateUnit];
            var Qb1 = OutFlowCurve().FlowRates.Max();
            var Qb2 = InFlowCurve().FlowRates.Max() * convFactor1;
            var Qb = Math.Min(Qb1, Qb2);

            var Qinit = 0.5 * (Qstart + Qb);

            var res = ScalarSolvers.ScalarSolvers.Newton(fun, Qinit);
            var Q = res.Solution;

            var rs = DF.Evaluate(Q);
            var dPdQoutflow = rs[1];

            //var test = Solvers.Diff1(f, Q);
            var dQ = 5.0;
            var Qleft = Q - dQ;
            var pOutflowLeft = OutFlowSystem.BHP(Qleft);
            var pInflowLeft = InFlowSystem.BHP(Qleft * convFactor); // convert arg to iprRateUnit
            pInflowLeft = makeIprPressureConsistent(iprPressureUnit, vlpPressureUnit, pInflowLeft);

            var Qright = Q + dQ;
            var pOutflowRight = OutFlowSystem.BHP(Qright);
            var pInflowRight = InFlowSystem.BHP(Qright * convFactor);
            pInflowRight = makeIprPressureConsistent(iprPressureUnit, vlpPressureUnit, pInflowRight);

            //if ((pOutflowLeft - pInflowLeft) > 0.0)

            //if (dPdQoutflow < 0.0)]

            if ((pOutflowLeft - pInflowLeft) > 0.0)
            {
                // point calculate above is unstable, bracket and calculate a new stable point
                dQ = 1e-1;
                var Qa = Q;
                do
                {

                    Qa += dQ;

                } while (fun(Qa) <= 0.0);

                //var Qb1 = OutFlowCurve().FlowRates.Max();
                //var Qb2 = InFlowCurve().FlowRates.Max();
                //var Qb = Math.Min(Qb1, Qb2);

                Q = ScalarSolvers.ScalarSolvers.NewtonBracket(fun, Qa, Qb);
            }

            return new SystemPoint
            {
                FlowRate = Q,
                Pressure = OutFlowSystem.BHP(Q),
                FlowRateUnit = OutFlowSystem.RateUnit,
                PressureUnit = OutFlowSystem.THPUnit
            };
        }

        public ISystemPoint ComputeSystemPoint5()
        {
            var inflowCurve = InFlowCurve();
            var outflowCurve = OutFlowCurve();
            var inflowRateUnit = InFlowSystem.RateUnit;
            var outflowRateUnit = OutFlowSystem.RateUnit;
            var inflowBHPUnit = InFlowSystem.BottomHolePressuresUnit;
            var outflowBHPUnit = OutFlowSystem.BottomHolePressuresUnit;

            // convert outflow rate units to inflow rate units
            var convFact1 = UnitConverter.Factor[outflowRateUnit][inflowRateUnit];
            var rates = outflowCurve.FlowRates.Select(x => x * convFact1).ToArray();
            outflowCurve.FlowRates = rates;
            outflowCurve.RateUnit = inflowRateUnit;
            inflowCurve.RateUnit = inflowRateUnit;

            // No need to do conversion for pressure because it is always in psia

            if (!SystemIsNormalScenario(inflowCurve, outflowCurve))
            {
                var result = new SystemPoint
                {
                    FlowRateUnit = inflowRateUnit,
                    PressureUnit = inflowBHPUnit,
                    Message = "Abnormal situation, please check system plot and adjust",
                    Success = false
                };

                return result;
            }

            // Check that the system has intersection
            var intersectionBracket = FindSystemPointRateBracket(inflowCurve, outflowCurve);
            bool hasIntersection = intersectionBracket.Item3;
            if (!hasIntersection)
            {
                string msg = "System has NO intersection! Artificial lift may help." +
                              " If you're already on artificial lift, try increasing" +
                              " the GasLiftRate or Ratio";

                var result = new SystemPoint
                {
                    FlowRateUnit = inflowRateUnit,
                    PressureUnit = inflowBHPUnit,
                    Message = msg,
                    Success = false
                };

                return result;
            }


            // If we get this far then we have a normal system whose intersection has been 
            // bracketed Time to extract bracket
            var Qa = intersectionBracket.Item1;
            var Qb = intersectionBracket.Item2;

            var fn = new Interp1d(outflowCurve.FlowRates, outflowCurve.BottomHolePressures);

            // Construct equation to solve:
            Func<double, double> fun = (double q) =>
            {
                double lhs = InFlowSystem.BHP(q);
                //double rhs = OutFlowSystem.BHP(q);
                double rhs = fn.Interpolate(q);
                return lhs - rhs;// This has to be modified!
            };

            var Q = ScalarSolvers.ScalarSolvers.NewtonBracket(fun, Qa, Qb);

            return new SystemPoint
            {
                FlowRate = Q,
                Pressure = fn.Interpolate(Q), //OutFlowSystem.BHP(Q),
                FlowRateUnit = inflowRateUnit,
                PressureUnit = OutFlowSystem.THPUnit,
                Message = "System intersection successfully computed.",
                Success = true
            };
        }

        public ISystemPoint ComputeSystemPoint5(ICurve inflow, ICurve outflow)
        {
            //var inflowCurve = InFlowCurve();
            //var outflowCurve = OutFlowCurve();
            //var inflowRateUnit = InFlowSystem.RateUnit;
            //var outflowRateUnit = OutFlowSystem.RateUnit;
            //var inflowBHPUnit = InFlowSystem.BottomHolePressuresUnit;
            //var outflowBHPUnit = OutFlowSystem.BottomHolePressuresUnit;
            var inflowRateUnit = inflow.RateUnit;
            var outflowRateUnit = outflow.RateUnit;

            // convert outflow rate units to inflow rate units
            var convFact1 = UnitConverter.Factor[outflowRateUnit][inflowRateUnit];
            //var rates = outflowCurve.FlowRates.Select(x => x * convFact1).ToArray();
            var rates = outflow.Rates.Select(x => x * convFact1).ToArray();
            //outflowCurve.FlowRates = rates;
            //outflowCurve.RateUnit = inflowRateUnit;
            //inflowCurve.RateUnit = inflowRateUnit;

            var inflowCurve = new Result
            {
                BottomHolePressures = inflow.Pressures,
                FlowRates = inflow.Rates
            };

            var outflowCurve = new Result
            {
                BottomHolePressures = outflow.Pressures,
                FlowRates = rates
            };

            // No need to do conversion for pressure because it is always in psia

            if (!SystemIsNormalScenario(inflowCurve, outflowCurve))
            {
                var result = new SystemPoint
                {
                    FlowRateUnit = inflowRateUnit,
                    PressureUnit = inflow.PressureUnit,
                    Message = "Abnormal situation, please check system plot and adjust",
                    Success = false
                };

                return result;
            }

            // Check that the system has intersection
            var intersectionBracket = FindSystemPointRateBracket(inflowCurve, outflowCurve);
            bool hasIntersection = intersectionBracket.Item3;
            if (!hasIntersection)
            {
                string msg = "System has NO intersection! Artificial lift may help." +
                              " If you're already on artificial lift, try increasing" +
                              " the GasLiftRate or Ratio";

                var result = new SystemPoint
                {
                    FlowRateUnit = inflowRateUnit,
                    PressureUnit = inflow.PressureUnit,
                    Message = msg,
                    Success = false
                };

                return result;
            }


            // If we get this far then we have a normal system whose intersection has been 
            // bracketed Time to extract bracket
            var Qa = intersectionBracket.Item1;
            var Qb = intersectionBracket.Item2;

            var fnOutflow = new Interp1d(outflowCurve.FlowRates, outflowCurve.BottomHolePressures);

            var fnInflow = new Interp1d(inflowCurve.FlowRates, inflowCurve.BottomHolePressures);
            // Construct equation to solve:

            Func<double, double> fun = (double q) =>
            {
                double lhs = fnInflow.Interpolate(q);
                //double rhs = OutFlowSystem.BHP(q);
                double rhs = fnOutflow.Interpolate(q);
                return lhs - rhs;// This has to be modified!
            };

            //Func<double, double> fun = (double q) =>
            //{
            //    double lhs = InFlowSystem.BHP(q);
            //    //double rhs = OutFlowSystem.BHP(q);
            //    double rhs = fn.Interpolate(q);
            //    return lhs - rhs;// This has to be modified!
            //};

            var Q = ScalarSolvers.ScalarSolvers.NewtonBracket(fun, Qa, Qb);

            return new SystemPoint
            {
                FlowRate = Q,
                Pressure = fnOutflow.Interpolate(Q), //OutFlowSystem.BHP(Q),
                FlowRateUnit = inflowRateUnit,
                PressureUnit = inflow.PressureUnit,
                Message = "System intersection successfully computed.",
                Success = true
            };
        }

        public List<double> UnstableOperatingPointBracket()
        {
            var Qmin = OutFlowCurve().FlowRates.Min();
            var Qmax = OutFlowCurve().FlowRates.Max();
            var Q = Qmin;
            var Pinflow = InFlowSystem.BHP(Q);
            var Poutflow = OutFlowSystem.BHP(Q);
            double Qold = -10.0;
            Q += 100.0;

            while ((Poutflow >= Pinflow) || (Q <= Qmax))
            {
                Qold = Q;
                Pinflow = InFlowSystem.BHP(Q);
                Poutflow = OutFlowSystem.BHP(Q);
                Q += 100.0;
            }

            var result = new List<double>();
            if (Qold > 0)
            {
                result.Add(Qold);
                result.Add(Q);
            }

            return result;
        }

        public List<double> StableOperatingPointBracket(double Qstart)
        {
            var Q = Qstart;
            var Pinflow = InFlowSystem.BHP(Q);
            var Poutflow = OutFlowSystem.BHP(Q);
            double Qold = -10.0;

            while (Poutflow <= Pinflow)
            {
                Qold = Q;
                Q += 100.0;
                Pinflow = InFlowSystem.BHP(Q);
                Poutflow = OutFlowSystem.BHP(Q);
            }

            var result = new List<double>();
            if (Qold > 0)
            {
                result.Add(Qold);
                result.Add(Q);
            }

            return result;
        }

        public bool HasUnstableOperatingPoint()
        {
            var Qstart = OutFlowCurve().FlowRates.Min();
            var Pinflow = InFlowSystem.BHP(Qstart);
            var Poutflow = OutFlowSystem.BHP(Qstart);
            return Poutflow > Pinflow;
        }

        private ColVec Residual(ColVec x)
        {
            var Q = x[0];
            var p = x[1];
            var res1 = Q - InFlowSystem.Rate(p);
            var res2 = p - OutFlowSystem.BHP(Q);

            return new ColVec(res1, res2);
        }

        private string GetCurveToUse(IResult inflowCurve, IResult outflowCurve)
        {
            double maxOutflowRate = outflowCurve.FlowRates.Max();
            double maxInflowRate = inflowCurve.FlowRates.Max();
            return (maxOutflowRate < maxInflowRate) ? "outflow" : "inflow";
        }

        private double FindMaxMinRate(double[] inflowRates, double[] outflowRates)
        {
            double minOutflowRate = outflowRates.Min();
            double minInflowRate = inflowRates.Min();
            return (minOutflowRate > minInflowRate) ? minOutflowRate : minInflowRate;
        }

        private bool SystemIsNormalScenario(IResult inflowCurve, IResult outflowCurve)
        {
            double maxOutFQ = outflowCurve.FlowRates.Max();
            double maxInFQ = inflowCurve.FlowRates.Max();

            if (maxOutFQ <= maxInFQ)
            {
                int n = outflowCurve.FlowRates.Length;
                var pOutF = outflowCurve.BottomHolePressures[n - 1];
                var rates = inflowCurve.FlowRates;
                var bhps = inflowCurve.BottomHolePressures;
                var interp = new Interp1d(rates, bhps);
                var pInF = interp.Interpolate(maxOutFQ);
                return pOutF > pInF;
            }
            else
            {
                int n = inflowCurve.FlowRates.Length;
                var pInF = inflowCurve.BottomHolePressures[n - 1];
                var rates = outflowCurve.FlowRates;
                var bhps = outflowCurve.BottomHolePressures;
                var interp = new Interp1d(rates, bhps);
                var pOutF = interp.Interpolate(maxOutFQ);
                return pOutF > pInF;
            }
        }

        private Tuple<double, double, bool> FindSystemPointRateBracketUsingOutFlowRates
        (double Qleft, IResult inflowCurve, IResult outflowCurve)
        {
            // Let Q = OutFlowCurve().FlowRates[last]. In this algorithm we're assuming 
            // that OutFlowCurve().BHP[last] > InFlowCurve().BHP[at Q] so that the right  
            // endpoint of the OutFlowCurve is above the right endpoint of the InFlowCurve.

            var rates = inflowCurve.FlowRates;
            var bhps = inflowCurve.BottomHolePressures;
            int n = outflowCurve.FlowRates.Length;
            var interp = new Interp1d(rates, bhps);
            Func<double, double> BHPInflow = (double Q) => interp.Interpolate(Q);

            double qPrev = outflowCurve.FlowRates[n - 1];
            double qNext = outflowCurve.FlowRates[n - 2];
            double pOutFlow = outflowCurve.BottomHolePressures[n - 2];
            double pInFlow = BHPInflow(qNext);

            int i = n - 2;

            while ((pOutFlow - pInFlow) > 0 && (--i >= 0))
            {
                // The second condition above is redundant because first cond. is guarranted  
                // to be false before i gets < 0. This ofcourse assumes that pre-condition is 
                // obeyed or respected.

                qPrev = qNext;
                qNext = outflowCurve.FlowRates[i];
                pOutFlow = outflowCurve.BottomHolePressures[i];
                pInFlow = BHPInflow(qNext);
            }

            bool hasIntersection = true;
            if ((pOutFlow - pInFlow) > 0 && (i < 0)) hasIntersection = false;

            return new Tuple<double, double, bool>(qNext, qPrev, hasIntersection);
        }

        private Tuple<double, double, bool> FindSystemPointRateBracketUsingInFlowRates
        (double Qleft, IResult inflowCurve, IResult outflowCurve)
        {
            // Let Q = InFlowCurve().FlowRates[last]. In this algorithm we're assuming 
            // that InFlowCurve().BHP[last] > InFlowCurve().BHP[at Q] so that the right  
            // endpoint of the OutFlowCurve is above the right endpoint of the InFlowCurve.

            var rates = outflowCurve.FlowRates;
            var bhps = outflowCurve.BottomHolePressures;
            int n = inflowCurve.FlowRates.Length;
            var interp = new Interp1d(rates, bhps);
            Func<double, double> BHPOutflow = (double Q) => interp.Interpolate(Q);

            double qPrev = inflowCurve.FlowRates[n - 1];
            double qNext = inflowCurve.FlowRates[n - 2];
            double pInFlow = inflowCurve.BottomHolePressures[n - 2];
            double pOutFlow = BHPOutflow(qNext);

            int i = n - 2;

            while ((pOutFlow - pInFlow) > 0 && (--i >= 0))
            {
                // The second condition above is redundant because first cond. is guarranted  
                // to be false before i gets < 0. This ofcourse assumes that pre-condition is 
                // obeyed or respected.

                qPrev = qNext;
                qNext = inflowCurve.FlowRates[i];
                pInFlow = inflowCurve.BottomHolePressures[i];
                if (qNext < Qleft) qNext = Qleft;
                pOutFlow = BHPOutflow(qNext);
            }

            bool hasIntersection = true;
            if ((pOutFlow - pInFlow) > 0 && (i < 0)) hasIntersection = false;

            return new Tuple<double, double, bool>(qNext, qPrev, hasIntersection);
        }

        private Tuple<double, double, bool> FindSystemPointRateBracketUsingInFlowRates1
        (double Qleft, IResult inflowCurve, IResult outflowCurve)
        {
            // Let Q = InFlowCurve().FlowRates[last]. In this algorithm we're assuming 
            // that InFlowCurve().BHP[last] > InFlowCurve().BHP[at Q] so that the right  
            // endpoint of the OutFlowCurve is above the right endpoint of the InFlowCurve.

            var rates = outflowCurve.FlowRates;
            var bhps = outflowCurve.BottomHolePressures;
            int n = inflowCurve.FlowRates.Length;
            var interp = new Interp1d(rates, bhps);
            Func<double, double> BHPOutflow = (double Q) => interp.Interpolate(Q);

            double qPrev = inflowCurve.FlowRates[n - 1];
            //double qNext = inflowCurve.FlowRates[n - 2];
            //double pInFlow = inflowCurve.BottomHolePressures[n - 2];
            //double pOutFlow = BHPOutflow(qNext);
            bool hasIntersection = true;
            int i = 0;

            for (i = n - 2; i != -1; --i)
            {
                double q = inflowCurve.FlowRates[i];
                double pInFlow = inflowCurve.BottomHolePressures[i];
                if (q < rates.First())
                {
                    return new Tuple<double, double, bool>(0, 0, false);
                }
                double pOutFlow = BHPOutflow(q);
                double diff = pOutFlow - pInFlow;
                if (diff <= 0.0)
                    return new Tuple<double, double, bool>(q, qPrev, hasIntersection);
                qPrev = q;

            }


            hasIntersection = false;

            return new Tuple<double, double, bool>(0.0, 0.0, hasIntersection);
        }


        private Tuple<double, double, bool> FindSystemPointRateBracketUsingOutFlowRates1
        (double Qleft, IResult inflowCurve, IResult outflowCurve)
        {
            // Let Q = OutFlowCurve().FlowRates[last]. In this algorithm we're assuming 
            // that OutFlowCurve().BHP[last] > InFlowCurve().BHP[at Q] so that the right  
            // endpoint of the OutFlowCurve is above the right endpoint of the InFlowCurve.

            var rates = inflowCurve.FlowRates;
            var bhps = inflowCurve.BottomHolePressures;
            int n = outflowCurve.FlowRates.Length;
            var interp = new Interp1d(rates, bhps);
            Func<double, double> BHPInflow = (double Q) => interp.Interpolate(Q);

            double qPrev = outflowCurve.FlowRates[n - 1];
            double qNext = outflowCurve.FlowRates[n - 2];
            double pOutFlow = outflowCurve.BottomHolePressures[n - 2];
            double pInFlow = BHPInflow(qNext);

            int i = n - 2;

            while ((pOutFlow - pInFlow) > 0 && (--i >= 0))
            {
                // The second condition above is redundant because first cond. is guarranted  
                // to be false before i gets < 0. This ofcourse assumes that pre-condition is 
                // obeyed or respected.

                qPrev = qNext;
                qNext = outflowCurve.FlowRates[i];
                pOutFlow = outflowCurve.BottomHolePressures[i];
                pInFlow = BHPInflow(qNext);
            }

            bool hasIntersection = true;
            if ((pOutFlow - pInFlow) > 0 && (i < 0)) hasIntersection = false;

            return new Tuple<double, double, bool>(qNext, qPrev, hasIntersection);
        }


        private Tuple<double, double, bool> FindSystemPointRateBracketUsingFlows
        (double Qleft, IResult curveA, IResult curveB)
        {

            var rates = curveB.FlowRates;
            var bhps = curveB.BottomHolePressures;
            int n = curveA.FlowRates.Length;
            var interp = new Interp1d(rates, bhps);
            Func<double, double> BHP = (double Q) => interp.Interpolate(Q);

            double qPrev = curveA.FlowRates[n - 1];
            double pInFlow = curveA.BottomHolePressures[n - 1];
            double pOutFlow = BHP(qPrev);
            double diffPrev = pOutFlow - pInFlow;
            //double qNext = inflowCurve.FlowRates[n - 2];
            //double pInFlow = inflowCurve.BottomHolePressures[n - 2];
            //double pOutFlow = BHPOutflow(qNext);
            bool hasIntersection = true;
            int i = 0;

            for (i = n - 2; i != -1; --i)
            {
                double q = curveA.FlowRates[i];
                pInFlow = curveA.BottomHolePressures[i];
                if (q < rates.First())
                {
                    return new Tuple<double, double, bool>(0, 0, false);
                }
                pOutFlow = BHP(q);
                double diff = pOutFlow - pInFlow;
                if (Math.Sign(diff) != Math.Sign(diffPrev))
                    return new Tuple<double, double, bool>(q, qPrev, hasIntersection);
                qPrev = q;
                diffPrev = diff;

            }


            hasIntersection = false;

            return new Tuple<double, double, bool>(0.0, 0.0, hasIntersection);
        }


        private Tuple<double, double, bool>
        FindSystemPointRateBracket(IResult inflowCurve, IResult outflowCurve)
        {
            string curveName = GetCurveToUse(inflowCurve, outflowCurve);
            var inRates = inflowCurve.FlowRates;
            var outRates = outflowCurve.FlowRates;
            var Qmin = FindMaxMinRate(inRates, outRates);

            //if (curveName.ToLower() == "outflow")
            //{
            //    return FindSystemPointRateBracketUsingOutFlowRates(Qmin, inflowCurve, outflowCurve);
            //}
            //else
            //{
            //    return FindSystemPointRateBracketUsingInFlowRates1(Qmin, inflowCurve, outflowCurve);
            //}

            if (curveName.ToLower() == "outflow")
            {
                return FindSystemPointRateBracketUsingFlows(Qmin, outflowCurve, inflowCurve);
            }
            else
            {
                return FindSystemPointRateBracketUsingFlows(Qmin, inflowCurve, outflowCurve);
            }


        }

    }
}

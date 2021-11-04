using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public class IPRTable : IComponent
    {
        public string ContentString { get; set; }
        private ITableData data;
        public double[] Rates { get { return data.Rates.ToArray(); } }
        public double[] Pressures { get { return data.Pressures.ToArray(); } }
        public double AOF { get { return data.AOF; } }
        public double WaterCut { get { return data.WaterCut; } }
        public double TotalGOR { get { return data.TotalGOR; } }
        public double WaterGasRatio { get => data.WaterGasRatio; }
        public double CondensateGasRatio { get => data.CondensateGasRatio; }
        public double ReservoirPressure { get { return data.ReservoirPressure; } }
        public double ReservoirTemperature { get { return data.ReservoirTemperature; } }
        public string RateUnit { get; }
        public string ReservoirPressureUnit { get; }
        public string BottomHolePressureUnit { get; }
        public double MaxRate
        {
            get
            {
                return data.Rates.Max();
            }
        }
        public double MinRate
        {
            get
            {
                return data.Rates.Min();
            }
        }
        public double MaxPressure
        {
            get
            {
                return data.Pressures.Max();
            }
        }
        public double MinPressure
        {
            get
            {
                return data.Pressures.Min();
            }
        }

        public IPRTable(IIPRTableInfo iprTable)
        {
            ContentString = iprTable.ContentString;
            RateUnit = iprTable.RateUnit;
            ReservoirPressureUnit = iprTable.ReservoirPressureUnit;
            BottomHolePressureUnit = iprTable.BottomHolePressureUnit;
            data = ParseTable();

            var unit = iprTable.BottomHolePressureUnit;
            if (unit != null)
            {
                if (unit.ToLower() == "psig")
                {
                    var bhps = data.Pressures.Select(x => x + 14.7).ToList();
                    data.Pressures = bhps;
                    BottomHolePressureUnit = "psia";
                }
            }
        }

        private ITableData ParseTable()
        {
            var rawLines = ContentString.Split('\n');
            var lines = SqueezeOutNonEssentials(rawLines);
            var rates = new List<double>();
            var pressures = new List<double>();

            var npoints = int.Parse(lines[pointers["Number of Rates"]]);
            var QPStartIndex = pointers["Table of Rates Pwf Twf"];
            var onePastEnd = npoints + QPStartIndex;

            for (int i = 3; i < onePastEnd; ++i)
            {
                var line = lines[i];
                var lineSplit = line.Split('\t');
                rates.Add(double.Parse(lineSplit[0]));
                pressures.Add(double.Parse(lineSplit[1]));
            }

            int AOFOffset = 0;
            int TotalGOROffset = pointers["Total GOR"];
            int WaterCutOffset = pointers["Water Cut"];
            int ReservoirPressureOffset = pointers["Reservoir Pressure"];
            int ReservoirTemperatureOffset = pointers["Reservoir Temperature"];
            int WaterGasRatioOffset = pointers["Water Gas Ratio"];
            int CondensateGasRatioOffset = pointers["Condensate Gas Ratio"];
            int AOFIndex = onePastEnd + AOFOffset;
            int TotalGORIndex = onePastEnd + TotalGOROffset;
            int WaterCutIndex = onePastEnd + WaterCutOffset;
            int ReservoirPressureIndex = onePastEnd + ReservoirPressureOffset;
            int ReservoirTemperatureIndex = onePastEnd + ReservoirTemperatureOffset;
            int WaterGasRatioIndex = onePastEnd + WaterGasRatioOffset;
            int CondensateGasRatioIndex = onePastEnd + CondensateGasRatioOffset;



            var result = new TableData()
            {
                Rates = rates,
                Pressures = pressures,
                AOF = double.Parse(lines[AOFIndex]),
                TotalGOR = double.Parse(lines[TotalGORIndex]),
                WaterCut = double.Parse(lines[WaterCutIndex]),
                WaterGasRatio = double.Parse(lines[WaterGasRatioIndex]),
                CondensateGasRatio = double.Parse(lines[CondensateGasRatioIndex]),
                ReservoirPressure = double.Parse(lines[ReservoirPressureIndex]),
                ReservoirTemperature = double.Parse(lines[ReservoirTemperatureIndex])
            };

            return result;
        }


        public double Rate(double pressure)
        {
            //int j = FindIntervalDescending(Pressures, pressure);
            //return InterpLinear1D(pressure, Pressures, Rates, j);
            var fun = new Interp1d(Pressures, Rates);
            return fun.Interpolate(pressure);
        }

        public double BHP(double rate)
        {
            //int j = FindIntervalAscending(Rates, rate);
            //return InterpLinear1D(rate, Rates, Pressures, j);
            var fun = new Interp1d(Rates, Pressures);
            return fun.Interpolate(rate);
        }

        public double Residual(double pressure, double rate)
        {
            return rate - Rate(pressure);
        }

        private int FindIntervalAscending(double[] axis, double x)
        {
            for (int index = 0; index < axis.Length; ++index)
            {
                if (axis[index] >= x)        // Pressures in descending order
                    return index;
            }

            return axis.Length;
        }

        private int FindIntervalDescending(double[] axis, double x)
        {
            for (int index = 0; index < axis.Length; ++index)
            {
                if (axis[index] <= x)      // Pressures in descending order
                    return index;
            }

            return axis.Length;
        }

        private double InterpLinear1D(double xi, double[] x, double[] y, int j)
        {
            return y[j - 1] + (xi - x[j - 1]) * (y[j] - y[j - 1]) / (x[j] - x[j - 1]);
        }
        public ITableData ComputeRatesPressures()
        {
            return ParseTable();
        }

        public IResult IPRCurve()
        {
            var result = new Result
            {
                BottomHolePressures = Pressures,
                FlowRates = Rates
            };

            return result;
        }

        public ICurve IPRCurve(int x)
        {
            // the parameter x is not really used but there so that we can
            // overload the method IPRCurve
            var result = new Curve
            {
                Pressures = Pressures,
                Rates = Rates,
                PressureUnit = BottomHolePressureUnit,
                RateUnit = RateUnit
            };

            return result;
        }

        private List<string> SqueezeOutNonEssentials(string[] stringList)
        {
            var result = new List<string>();
            foreach (var line in stringList)
            {
                if (StartsWithANumber(line))
                {
                    result.Add(line.Trim());
                }
            }
            return result;
        }

        private List<string> Squeeze(string[] stringList)
        {
            var result = new List<string>();
            foreach (var line in stringList)
            {
                if (!IsEmptyLine(line))
                {
                    result.Add(line.Trim());
                }
            }
            return result;
        }

        private bool StartsWithANumber(string line)
        {
            string pattern = @"^\s*\d+";

            var re = new Regex(pattern);

            return re.IsMatch(line);
        }

        private bool IsEmptyLine(string line)
        {
            string pattern = @"^\s*;?\s*$";

            var re = new Regex(pattern);

            return re.IsMatch(line);
        }

        private Dictionary<string, int> pointers = new Dictionary<string, int>
        {
            { "Well Fluid Type", 0 },         // (0-Oil 1-Gas 2-Condensate)
            { "Well Type", 1 },               // (0-Procer 1-Injector)
            { "Number of Rates" , 2},
            { "Table of Rates Pwf Twf", 3},
            { "AOF", 0 },
            { "Reservoir Pressure", 1},
            { "Reservoir Temperature", 2},
            { "Water Cut", 3},
            { "Water Gas Ratio", 4 },
            { "Total GOR", 5 },
            { "Condensate Gas Ratio", 6 },
            { "Oil Gravity", 7 },
            { "Gas Gravity", 8 },
            { "Water Salinity", 9 },
            { "H2S Mole Percent", 10 },
            { "CO2 Mole Percent", 11 },
            { "N2 Mole Percent", 12 },
            { "Relative Permeability Flag", 13 },    //  (0-Unused  1-Used)
            { "Residual Water Saturation", 14 },
            { "Residual Oil Saturation", 15 },
            { "Water EndPoint", 16 },
            { "Oil EndPoint", 17 },
            { "Water Exponent", 18 },
            { "Oil Exponent", 19 },
            { "Water Cut During Test", 20 },
            { "Compaction Permeability Reduction Model", 21 }, //  (0-No  1-Yes)
            { "Vogel Correction Flag Flag", 22 }               //  (0-No  1-Yes)
        };


        private List<string> fields = new List<string>
        {
            "Well Fluid Type",                   //   (0-Oil 1-Gas 2-Condensate)
            "Well Type",                         //    (0-Procer 1-Injector)
            "Number of Rates",
            "Table of Rates Pwf Twf",
            "AOF",
            "Reservoir Pressure",
            "Reservoir Temperature",
            "Water Cut",
            "Water Gas Ratio",
            "Total GOR",
            "Condensate Gas Ratio",
            "Oil Gravity",
            "Gas Gravity",
            "Water Salinity",
            "H2S Mole Percent",
            "CO2 Mole Percent",
            "N2 Mole Percent",
            "Relative Permeability Flag",        //  (0-Unused  1-Used)
            "Residual Water Saturation",
            "Residual Oil Saturation",
            "Water EndPoint",
            "Oil EndPoint",
            "Water Exponent",
            "Oil Exponent",
            "Water Cut During Test",
            "Compaction Permeability Reduction Model",    //  (0-No  1-Yes)
            "Vogel Correction Flag Flag",                 //  (0-No  1-Yes)
            "Residual Gas Saturation",
            "Gas EndPoint",
            "Gas Exponent",
            "GOR During Test"
        };
    }


    public class TableData : ITableData
    {
        public List<double> Rates { get; set; }
        public List<double> Pressures { get; set; }
        public double AOF { get; set; }
        public double TotalGOR { get; set; }
        public double WaterCut { get; set; }
        public double WaterGasRatio { get; set; }
        public double CondensateGasRatio { get; set; }
        public double ReservoirPressure { get; set; }
        public double ReservoirTemperature { get; set; }
    }
}

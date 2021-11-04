using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public class VFPTable
    {
        private Header header;
        private Dictionary<TableColumn, List<double>> axesDict;
        private Dictionary<string, int> indicesDict;
        private Dictionary<string, TableColumn> columnsDict;
        private NDimensionalArray table;
        private RegularGridInterpolator interpolator;

        public Dictionary<string, string> QuantitiesAndUnits { get; }
        public List<TableColumn> Quantities { get; }

        public double[] FlowRates
        {
            get
            {
                foreach (var elem in axesDict)
                {
                    if (elem.Key.Type == "FLO")
                        return axesDict[elem.Key].ToArray();
                }
                throw new Exception("Error!");
            }
        }

        public VFPTable(
                          Header header,
                          Dictionary<TableColumn, List<double>> axesDict,
                          NDimensionalArray table
                       )
        {

            this.header = header;

            if (!axisInDictionary("FLO", axesDict))
            {
                throw new Exception("FLO data must be supplied in axes dictionary");
            }

            if (!axisInDictionary("THP", axesDict))
            {
                throw new Exception("THP data must be supplied in axes dictionary");
            }

            this.axesDict = axesDict;
            this.table = table;
            this.indicesDict = new Dictionary<string, int>();

            this.columnsDict = new Dictionary<string, TableColumn>();
            this.QuantitiesAndUnits = new Dictionary<string, string>();
            this.Quantities = new List<TableColumn>();

            var axes = new List<double>[axesDict.Count];
            foreach (var e in axesDict)
            {
                var idx = e.Key.Index;
                axes[idx] = e.Value;
                this.indicesDict.Add(e.Key.Type, idx);
                this.columnsDict.Add(e.Key.Name, e.Key);
                this.QuantitiesAndUnits.Add(e.Key.Name, e.Key.Unit);
                this.Quantities.Add(e.Key);
            }

            var newAxes = axes.ToList<List<double>>();

            this.interpolator = new RegularGridInterpolator(newAxes, table);
        }

        public bool AxisExist(string axisName)
        {
            return axisInDictionary(axisName, axesDict);
        }

        public string Unit(string axisName)
        {
            foreach (var key in axesDict.Keys)
            {
                if (key.Type == axisName.ToUpper())
                {
                    return key.Unit;
                }
            }

            return "";
        }

        private double BHPhelper(List<double> point)
        {
            return this.interpolator.Interpolate(point);
        }

        public double GetBHP(double flow, double thp, double wfr = -1.0, double gfr = -1.0, double alq = -1.0)
        {
            var point = new double[this.axesDict.Count];
            point[this.indicesDict["FLO"]] = flow;
            point[this.indicesDict["THP"]] = thp;
            //int inputDim = 2;

            if (wfr >= 0.0)
            {
                if (!this.indicesDict.ContainsKey("WFR"))
                {
                    throw new Exception("WFR is not an axis in the table being interpolated");
                }
                point[this.indicesDict["WFR"]] = wfr;
                //inputDim++;
            }

            if (gfr >= 0.0)
            {
                if (!this.indicesDict.ContainsKey("GFR"))
                {
                    throw new Exception("GFR is not an axis in the table being interpolated");
                }
                point[this.indicesDict["GFR"]] = gfr;
                //inputDim++;
            }

            if (alq >= 0.0)
            {
                if (!this.indicesDict.ContainsKey("ALQ"))
                {
                    throw new Exception("ALQ is not an axis in the table being interpolated");
                }
                point[this.indicesDict["ALQ"]] = alq;
                //inputDim++;
            }

            return BHPhelper(point.ToList());
        }

        private bool axisInDictionary(string axisName, Dictionary<TableColumn, List<double>> axes)
        {
            foreach (var key in axes.Keys)
            {
                if (key.Type == axisName.ToUpper())
                {
                    return true;
                }
            }

            return false;
        }
    }
}

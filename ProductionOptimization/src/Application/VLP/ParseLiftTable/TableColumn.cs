using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public class TableColumn
    {
        public const string FLO = "FLO";
        public const string THP = "THP";
        public const string WFR = "WFR";
        public const string GFR = "GFR";
        public const string ALQ = "ALQ";

        public const string GAS = "GAS";
        public const string OIL = "OIL";
        public const string LIQ = "LIQ";

        public const string WCT = "WCT";
        public const string WOR = "WOR";
        public const string WGR = "WGR";

        public const string GOR = "GOR";
        public const string GLR = "GLR";
        public const string OGR = "OGR";

        public const string GRAT = "GRAT";
        public const string IGLR = "IGLR";
        public const string TGLR = "GFR";
        public const string COMP = "COMP";
        public const string PUMP = "PUMP";
        public const string DENG = "DENG";
        public const string DENO = "DENO";

        public string Type { get; }
        public string Name { get; }
        public int Index { get; }
        public string Unit { get; }

        public Dictionary<string, List<string>> ColumnTypes { get; } = new Dictionary<string, List<string>>
        {
            {"FLO", new List<string>{"GAS", "OIL", "LIQ" } },
            { "WFR", new List<string>{"WCT", "WOR", "WGR" } },
            { "GFR", new List<string>{"GOR", "GLR", "OGR" } },
            { "THP", new List<string>{"THP"} },
            { "ALQ", new List<string>{"GRAT", "IGLR", "TGLR", "COMP", "PUMP", "DENO", "DENG" } }
        };

        public TableColumn(string name, string type, int index)
        {
            type = type.ToUpper();
            name = name.ToUpper();

            if (!ColumnTypes.ContainsKey(type))
            {
                throw new Exception($"{type} is not a valid columnType");
            }

            if (!ColumnTypes[type].Contains(name))
            {
                throw new Exception($"{name} is not a valid {type} type");
            }

            this.Type = type;
            this.Name = name;
            this.Index = index;
        }
        public TableColumn(string name, string type, int index, string unit)
        {
            type = type.ToUpper();
            name = name.ToUpper();

            if (!ColumnTypes.ContainsKey(type))
            {
                throw new Exception($"{type} is not a valid columnType");
            }

            if (!ColumnTypes[type].Contains(name))
            {
                throw new Exception($"{name} is not a valid {type} type");
            }

            this.Type = type;
            this.Name = name;
            this.Index = index;
            this.Unit = unit;
        }

        public override int GetHashCode()
        {
            return this.Type.GetHashCode() ^ this.Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            var isEqual = (this.GetType() == obj.GetType() &&
                           this.Name == ((TableColumn)obj).Name &&
                           this.Type == ((TableColumn)obj).Type);
            bool result = false;
            if (isEqual)
            {
                result = true;
            }

            return result;
        }
    }
}

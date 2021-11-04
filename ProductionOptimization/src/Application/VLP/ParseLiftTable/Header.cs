using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public class Header
    {
        public int TableNo { get; set; }
        public double ReferenceDepth { get; set; }
        public string FLOType { get; set; }
        public string WFRType { get; set; }
        public string GFRType { get; set; }
        public string THPType { get; set; } = "THP"; //must be THP
        public string ALQType { get; set; }
        public string UnitType { get; set; }
        public string TabType { get; set; } = "BHP"; //must be BHP
        public Header() { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public class Derivative
    {
        public Func<double, double> Fun { get; set; }
        public double H { get; set; }
        public Derivative(Func<double, double> fun, double h = 1e-7)
        {
            Fun = fun;
            H = h;
        }

        public double[] Evaluate(double x)
        {
            var fValue = Fun(x);
            var fValue1 = Fun(x + H);
            var dfValue = (fValue1 - fValue) / H;

            var result = new double[] { fValue, dfValue };
            return result;
        }
    }
}

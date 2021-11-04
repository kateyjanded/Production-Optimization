using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public class Utility
    {
        public static List<Tuple> CartesianProduct(Tuple tupleA, Tuple tupleB)
        {
            var result = new List<Tuple>();

            foreach (var b in tupleB)
            {
                var temp = tupleA.Copy();
                temp.Append(b);
                result.Add(temp);
            }
            return result;
        }

        public static List<Tuple> CartesianProduct(List<Tuple> tuples, Tuple tuple)
        {
            var result = new List<Tuple>();

            foreach (var e in tuples)
            {
                var temp = Utility.CartesianProduct(e, tuple);
                result.AddRange(temp);
            }

            return result;
        }

        public static List<Tuple> CartesianProduct(List<Tuple> tuples)
        {
            if (tuples.Count == 1 || tuples.Count == 0)
                return tuples;

            var items = tuples[0].DecomposeToSingleElementTuples();

            var result = CartesianProduct(items, tuples[1]);

            for (int i = 2; i < tuples.Count; ++i)
            {
                result = CartesianProduct(result, tuples[i]);
            }

            return result;
        }

        public static int FindInterval(List<double> axis, double x)
        {
            for (int index = 0; index < axis.Count; ++index)
            {
                if (axis[index] >= x)
                    return index;
            }

            return axis.Count;
        }

        public static List<double> Linspace(double a, double b, int n)
        {
            double h = (b - a) / (n - 1);
            var result = new List<double>();

            for (int i = 0; i < n; ++i)
            {
                double x = a + i * h;
                result.Add(x);
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public class RegularGridInterpolator
    {
        private List<List<double>> axes;
        private NDimensionalArray data;
        private bool boundsError;
        public RegularGridInterpolator(List<List<double>> axes, NDimensionalArray data)
        {
            if (axes.Count != data.Ndim)
            {
                throw new Exception("Input axes dimension does not match input data dimension");
            }
            this.axes = axes;
            this.data = data;
            this.boundsError = false;

        }

        public RegularGridInterpolator SetBoundErrorFlag(bool flag)
        {
            this.boundsError = flag;
            return this;
        }
        public double Interpolate(List<double> point)
        {
            if (point.Count != axes.Count)
            {
                throw new Exception($"The requested sample point has dimension {point.Count}" +
                    $", but this RegularGridInterpolator has dimension {data.Ndim}");
            }

            if (boundsError)
            {
                for (int i = 0; i < axes.Count; ++i)
                {
                    int n = axes[i].Count;
                    if (point[i] < axes[i][0] || point[i] > axes[i][n - 1])
                    {
                        throw new Exception("The requested point is out of bounds in " +
                            $"dimension {i}");
                    }
                }
            }


            var res = FindIndices(point);
            var indices = res.Item1;
            var normDistances = res.Item2;

            double result = EvaluateLinear(indices, normDistances);

            return result;
        }

        private Tuple<Tuple, List<double>> FindIndices(List<double> point)
        {
            var indices = new Tuple();
            var normedDistances = new List<double>();
            //var outOfBounds = new List<bool>();

            for (int i = 0; i < point.Count; ++i)
            {
                int j = Utility.FindInterval(axes[i], point[i]) - 1;
                if (j < 0) j = 0;
                if (j > axes[i].Count - 2) j = axes[i].Count - 2;
                indices.Append(j);
                double nd = (point[i] - axes[i][j]) / (axes[i][j + 1] - axes[i][j]);
                normedDistances.Add(nd);
            }

            var result = new Tuple<Tuple, List<double>>(indices, normedDistances);

            return result;
        }

        private double EvaluateLinear(Tuple indices, List<double> normedDistances)
        {
            var intervals = new List<Tuple>();
            foreach (var i in indices)
            {
                var interval = new Tuple(i, i + 1);
                intervals.Add(interval);
            }

            var edges = Utility.CartesianProduct(intervals);

            double value = 0.0;

            foreach (var edgeIndices in edges)
            {
                double weight = 1.0;
                for (int j = 0; j < normedDistances.Count; ++j)
                {
                    int ei = edgeIndices[j];
                    int i = indices[j];
                    double yi = normedDistances[j];
                    weight *= (ei == i ? 1 - yi : yi);
                }

                value += this.data[edgeIndices] * weight;
            }

            return value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public class Interp1d
    {
        private double[] x;
        private double[] y;
        private bool isAscending;
        public Interp1d(double[] x, double[] y)
        {
            isAscending = IsAscending(x);
            bool isDescending = IsDescending(x);

            if (!(isAscending || isDescending))
            {
                throw new Exception("x values must be in ascending or descending order");
            }
            this.x = x;
            this.y = y;
        }

        private bool IsAscending(double[] x)
        {
            for (int i = 1; i < x.Length; ++i)
            {
                if (x[i] < x[i - 1])
                    return false;
            }
            return true;
        }

        private bool IsDescending(double[] x)
        {
            for (int i = 1; i < x.Length; ++i)
            {
                if (x[i] > x[i - 1])
                    return false;
            }
            return true;
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

        public double InterpLinear1D(double xi, double[] x, double[] y, int j)
        {
            return y[j - 1] + (xi - x[j - 1]) * (y[j] - y[j - 1]) / (x[j] - x[j - 1]);
        }

        public double Interpolate(double xi)
        {
            bool cond1 = (x[0] <= xi && xi <= x[x.Length - 1]);
            bool cond2 = (x[0] >= xi && xi >= x[x.Length - 1]);
            bool xi_is_in_array = cond1 || cond2;
            if (!xi_is_in_array)
            {
                throw new Exception("xi is outside the range of x array values");
            }
            double yLower;
            double yUpper;
            double xLower;
            double xUpper;
            int j;

            if (isAscending)
            {
                j = FindIntervalAscending(x, xi) - 1; // j is lower bound
                j = j < 0 ? 0 : j;  // To handle a case when xi == x[0]
                xLower = x[j];
                xUpper = x[j + 1];
                yLower = y[j];
                yUpper = y[j + 1];
            }
            else
            {
                j = FindIntervalDescending(x, xi); // j is lower bound
                xLower = x[j];
                xUpper = x[j - 1];   // upper is previous value
                yLower = y[j];
                yUpper = y[j - 1];
            }

            double slope = (yUpper - yLower) / (xUpper - xLower);
            var yi = slope * (xi - xLower) + yLower;

            return yi;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable.ScalarSolvers
{
    public class Result
    {
        public int NoOfIterations { get; set; }
        public int NoOfLineSearchIterations { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public double Solution { get; set; }
        public double Residual { get; set; }
    }
    public class ScalarSolvers
    {
        public static double NewtonBracket(
            Func<double, double> f,
            Derivative df,
            double a,
            double b,
            double tol = 1.0e-9)
        {
            var fa = f(a);
            if (fa == 0.0) return a;
            var fb = f(b); //2069 access code
            if (fb == 0.0) return b;
            if (Math.Sign(fa) == Math.Sign(fb))
            {
                throw new Exception("Root is not bracketed");
            }
            var x = 0.5 * (a + b);

            for (int i = 0; i < 30; ++i)
            {
                var fx = f(x);
                if (fx == 0.0) return x;
                // Tighten the brackets on the root
                if (Math.Sign(fa) != Math.Sign(fx))
                {
                    b = x;
                }
                else
                {
                    a = x;
                }

                //Try a Newton-Raphson step
                var res = df.Evaluate(x);
                var dfx = res[1];

                double dx;

                try
                {
                    dx = -fx / dfx;
                }
                catch (Exception)
                {
                    dx = b - a;
                }

                x += dx;

                // if the result is outside the brackets, use bisection
                if ((b - x) * (x - a) < 0.0)
                {
                    dx = 0.5 * (b - a);
                    x = a + dx;
                }

                // Check for convergence
                if (Math.Abs(dx) < tol * Math.Max(Math.Abs(b), 1.0)) return x;
                // if abs(dx) < tol * max(abs(b), 1.0): return x
            }

            throw new Exception("Too many iterations in Newton-Raphson");
        }

        public static double NewtonBracket(
            Func<double, double> f,
            double a,
            double b,
            double tol = 1.0e-9)
        {
            var df = new Derivative(f);
            return NewtonBracket(f, df, a, b);
        }

        public static Result Newton(
            Func<double, double> f,
            Derivative df,
            double x,
            double atol = 1.0e-7,
            double rtol = 1.0e-4)
        {
            var result = new Result();
            var alpha = 1e-4;
            var MaxIter = 30;
            var MaxIterL = 70;
            var res = df.Evaluate(x);
            var fx = res[0];
            var dfx = res[1];
            var r0 = Math.Abs(fx);

            int iter = 0;

            while ((Math.Abs(fx) > (rtol * r0 + atol)) && (iter <= MaxIter))
            {
                if (dfx == 0.0) throw new Exception("Singularity failure");
                var d = -fx / dfx; // search direction
                double lambda = 1.0;
                var xt = x + lambda * d; // trial point
                var fxt = f(xt);

                // Backtracking line search
                int iterL = 0;
                while ((Math.Abs(fxt) > (1 - alpha * lambda) * Math.Abs(fx)) && iterL <= MaxIterL)
                {
                    lambda = lambda / 2; // reject step
                    xt = x + lambda * d; // trial point
                    fxt = f(xt);
                    ++iterL;
                }

                if (iterL > MaxIterL)
                {

                    result.NoOfIterations = iter;
                    result.NoOfLineSearchIterations = iterL;
                    result.Success = false;
                    result.Residual = fxt;
                    result.Solution = xt;
                    result.Message = "Failure of Newton during line search";

                    return result;
                }

                // accept xt
                x = xt;
                fx = fxt;
                res = df.Evaluate(x);
                dfx = res[1];
            }

            if (iter > MaxIter)
            {
                result.NoOfIterations = iter;
                result.Success = false;
                result.Residual = fx;
                result.Solution = x;
                result.Message = "Failure of Newton. Max iteration exceeded";
                return result;
            }

            result.NoOfIterations = iter;
            result.Success = true;
            result.Residual = fx;
            result.Solution = x;
            result.Message = "Newton procedure succeded";
            return result;

        }

        public static Result Newton(Func<double, double> f, double x)
        {
            var df = new Derivative(f);
            return Newton(f, df, x);
        }
    }
}

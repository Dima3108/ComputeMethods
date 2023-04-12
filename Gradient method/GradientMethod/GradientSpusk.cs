using System;
using System.Collections.Generic;
using System.Text;

namespace GradientMethod
{
   public class GradientSpusk
    {
        public Function function;
        /// <summary>
        /// начальное приблежение
        /// </summary>
        public double[] x0

            ;
            /// <summary>
            /// точность расчета
            /// </summary>
         public double   epsiland;
        public GradientSpusk(Function function_,double []x0_,double eps_)
        {
            function = function_;
            x0 = x0_;
            epsiland = eps_;
        }
        public const double lambda = 0.001;
        public double[] NextX(double[] xi)
        {
            var xI1 = function.DerivativeFunction(xi);
            for (int i = 0; i < xI1.Length; i++)
                xI1[i] = xI1[i] - lambda * xI1[i];
            return xI1;
        }
        public bool IsStop(double[] xi, double[] xi1)
        {
            //double val = Math.Abs(xi1[0] - xi[0]);
            bool is_big = false;
            for(int i=0;i<xi.Length;i++)
                if(Math.Abs(xi1[i]-xi[1])<epsiland)
                {
                    is_big = true;
                    break;
                }
            if (is_big)
            {
                return ( (function.Function(xi1) - function.Function(xi)) < epsiland);
            }
            else return false;
        }
        public double Compute()
        {
            double[] X = x0;
            double[] X1 = NextX(X);
            while (!IsStop(X1, X))
            {
                X1.CopyTo(X, 0);
                X1 = NextX(X);
            }
            return function.Function(X1);
        }
    }
}

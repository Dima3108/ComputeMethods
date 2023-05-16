using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpsoneM
{
  public  class Integrated
    {
        double a, b;
        FunctionInterface f;
        public Integrated(double a,double b,FunctionInterface f)
        {
            this.a = a;
            this.b = b;
            this.f = f;
        }
        public double ComputeX(double x)
        {
            return x - ((SimpsonMethod.ComputeValue(a, x, f) - b) / f.function(x));
        }
        public double Compute(double eps)
        {
            double x0 =ushort.MaxValue*Math.Abs(a)
                ;
            double x = ComputeX(x0);
            while (Math.Abs(x0 - x) > eps)
            {
                x0 = x;
                x = ComputeX(x0);
            }
            return x0;
        }
    }
}

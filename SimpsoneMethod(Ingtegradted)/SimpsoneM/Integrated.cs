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
        public double ComputeX(double x,bool ISV2M=false)
        {
            return x - ((((!ISV2M)?SimpsonMethod.ComputeValue(a, x, f):SimpsonMethod.ComputeValueV2(a,x,f)) - b) / f.function(x));
        }
        public double ComputeXV2(double x)
        {
            double f_x = f.function(x);
            double f_a = f.function(a);
            double f_ax = f.function((a + x) / 2);
            return (5.0 * x) + a + (((a - x) * (f_a - (4 * f_ax))) / f_x);
        }
        public double Compute(double eps,bool isCV2=false,bool isCOV2=false)
        {
            double x0 =ushort.MaxValue*Math.Abs(a)
                ;

            double x;
            if (isCOV2 == false)
                x = ComputeX(x0, isCV2);
            else x = ComputeXV2(x0);
            while (Math.Abs( x0-x) > eps)
            {
                x0 = x;
                if (isCOV2 == false)
                    x = ComputeX(x0, isCV2);
                else x = ComputeXV2(x0);
            }
            return x0;
        }
    }
}

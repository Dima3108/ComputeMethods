using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpsoneM
{
  public  class SimpsonMethod
    {
        public FunctionInterface function;
        public double a, b;
        public double f_a;
        public double c;
        /// <summary>
        /// x
        /// ∫f(x)dx=b
        /// a
        /// </summary>
        /// <param name="function_">интегрируемая функция </param>
        /// <param name="A">Нижнея граница интегрирования</param>
        /// <param name="B">результат c</param>
        public SimpsonMethod(FunctionInterface function_,double A,double B)
        {
            function = function_;
            a = A;
            b = B;
            f_a = function.function(a);
            c = (3 * b) + f_a;
        }
        /*
         * (x/2)(f(a)+f(x))-af(x)+2f((x+a)/2)(x-a)=3b+f(a)
         */
        public double ComputeValue(double x)
        {
            double f_x = function.function(x);
            double f_xa = function.function((x + a) / 2);
            return ((x / 2) * (f_a + f_x)) - (a * f_x) + ((2 * f_xa) * (x - a));

        }
        public static double ComputeValue(double a_,double b_, FunctionInterface f)
        {
            double a = Math.Min(a_, b_);
            double b = Math.Max(a_, b_);
            double v = (b - a) / 6;
            double v2 = (b + a) / 2;
            return v * (f.function(a) + f.function(v2) + f.function(b));
        }
        public double DeviationEstimate(double d_b) =>Math.Abs( ( d_b/b) - 1.00000000);
        public double SegmentationCalculation(double start_val,double max_val,int crushing_step,double eps)
        {
 double offset = (max_val - start_val) / crushing_step;
            double delta_0 = DeviationEstimate(ComputeValue(start_val + offset));
                //Math.Abs( DeviationEstimate(ComputeValue(start_val))-DeviationEstimate(ComputeValue(start_val+offset)));
            double val = start_val;
            int step = 0;
            /* if (delta_0 <= eps)
                 return start_val;*/
#if DEBUG
            Console.WriteLine($"of:{offset}");
#endif
            if (offset < eps)
            {
                if(DeviationEstimate(ComputeValue(start_val))> DeviationEstimate(ComputeValue(start_val + offset))){
                    return start_val + offset;
                }
                else
                {
                    return start_val;
                }
            }

            for(int i = 1; i < crushing_step - 1; i++)
            {
                double delta = //Math.Abs(DeviationEstimate(ComputeValue(start_val + (offset * i))) - 
                    DeviationEstimate(ComputeValue(start_val + (offset * (i + 1))))
                    //)
                    ;
                if (delta < delta_0)
                {
                    delta_0 = delta;
                    step = i;

                }
            }
#if DEBUG
            Console.WriteLine($"dl:{DeviationEstimate(ComputeValue(start_val + (offset * step)))}-{DeviationEstimate(ComputeValue(start_val + (offset * (step+1))))}");
#endif
            return SegmentationCalculation(start_val + (offset * step), start_val + (offset * (step + 1)), crushing_step, eps);
        }
        public const double eps= 0.00000000005;
        public const int step = 1024+512+256+12;
        public double ComputeResult()
        {
            if (b == 0)
            {
                return a;
            }
            else
            {
                double v = -2.3456789;
                bool is_cvad_function = function.function(v) == function.function(-v);
                bool is_lin_func;
                double[] r = new double[] { function.function(-1.00123), function.function(-1.00123 + 3.0456), function.function(8.0009345) };
                is_lin_func = (r[0] <= r[1] && r[1] <= r[2]) || (r[0] > r[1] && r[1] > r[2]);
                bool is_vozr = r[0] < r[1] && r[1] < r[2];
                if (b > 0)
                {
                    if (is_cvad_function || is_lin_func)
                    {
#if DEBUG
                        Console.WriteLine(is_vozr);
                        Console.WriteLine(f_a < b);
#endif
                        if ((is_vozr && f_a < b)||(!is_vozr&&b>f_a))
                        {
                            return SegmentationCalculation(a, short.MaxValue, step, eps);
                        }
                        else
                        {
                            return SegmentationCalculation(short.MinValue, a, step, eps);
                        }
                    }
                    else return SegmentationCalculation(short.MinValue, short.MaxValue, step, eps);
                    throw new Exception();
                        
                }
                else
                {
                    if (is_cvad_function || is_lin_func)
                    {
                        if ((!is_vozr && f_a < b) || (is_vozr && b > f_a))
                        {
                            return SegmentationCalculation(a, short.MaxValue, step, eps);
                        }
                        else
                        {
                            return SegmentationCalculation(short.MinValue, a, step, eps);
                        }
                    }
                    else return SegmentationCalculation((double)short.MinValue, (double)short.MaxValue, step, eps);
                    throw new Exception();
                }
            }
        }
        /*{
            double dlt = d_b / b;
            return dlt - 1.0;
        }*/

    }
}

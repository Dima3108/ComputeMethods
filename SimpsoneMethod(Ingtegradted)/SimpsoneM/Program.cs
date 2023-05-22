using System;

namespace SimpsoneM
{
  public  class line_funct : FunctionInterface
    {
        public double function(double x) => (4 * x) ;//2x^2   8 16
    }
    public class cvadro_func : FunctionInterface
    {
        public double function(double x) => 2 * x * x;
    }
    public class five_degfunc: FunctionInterface
    {
        public double function(double x) => Math.Pow(x, 5.0) / 120.0;
    }
    public class free_one_degfunc : FunctionInterface
    {
        public double function(double x) => (-3 * Math.Pow(x, 3.0)) + 15 * x;
    }
    public class seven_foo_one_degfunc : FunctionInterface
    {
        public double function(double x) => (-Math.Pow(x, 7.0)) + (3.0 * Math.Pow(x, 2.0)) - 4 * x;
    }
    public class mix_function : FunctionInterface
    {
        public double function(double x) => (7.00 * Math.Pow(x, 2.0)) + (2.0 * Math.Cos(x));
    }
    public class deg_function_free : FunctionInterface
    {
        public double function(double x) => (x * x * x) - (4 * x * x) + 32;
    }
    class Program
    {
        static void Main(string[] args)
        {
            const double eps = 0.0000003;
            /* SimpsonMethod simpsonMethod = new SimpsonMethod(new line_funct(), 2, 10);
           Console.WriteLine(  simpsonMethod.ComputeResult() + "\n@\n");
             SimpsonMethod simpsonMethod1 = new SimpsonMethod(new cvadro_func(), 3, 126);
             Console.WriteLine(simpsonMethod1.ComputeResult() + "\n@\n");
             SimpsonMethod simpsonMethod2 = new SimpsonMethod(new five_degfunc(), 21, 7504535.5875);
             Console.WriteLine(simpsonMethod2.ComputeResult() + "\n@\n");
             SimpsonMethod simpsonMethod3 = new SimpsonMethod(new free_one_degfunc(), -3, -389897100);
             Console.WriteLine(simpsonMethod3.ComputeResult() + "\n@\n");
             SimpsonMethod simpsonMethod4 = new SimpsonMethod(new seven_foo_one_degfunc(), -3, -8935.125);
             Console.WriteLine(simpsonMethod4.ComputeResult()+"\n@\n");*/
            Integrated intr = new Integrated(2, 18, new line_funct());
            Console.WriteLine(intr.Compute(eps));
            Console.WriteLine(intr.Compute(eps,true));
            //Console.WriteLine(intr.Compute(eps, isCOV2:true));
            Integrated intr2 = new Integrated( -3, -389897100,new free_one_degfunc());
            Console.WriteLine(intr2.Compute(eps));
            Console.WriteLine(intr2.Compute(eps,true));
          //  Console.WriteLine(intr2.Compute(eps, isCOV2: true));
            Integrated integrated3 = new Integrated(0, 19.0869, new mix_function());
            Console.WriteLine(integrated3.Compute(eps));
            Console.WriteLine(integrated3.Compute(eps, true));
            //Console.WriteLine(integrated3.Compute(eps, isCOV2: true));
            Integrated integrated4 = new Integrated(3, 1123.75, new deg_function_free());
            Console.WriteLine(integrated4.Compute(eps));
            Console.WriteLine(integrated4.Compute(eps, true));
            //Console.WriteLine(integrated4.Compute(eps, isCOV2: true));
            /* Integrated intr3 = new Integrated(-3, -8935.125, new seven_foo_one_degfunc())
 ;
             Console.WriteLine(intr3.Compute(0.00003));*/
        }
    }
}

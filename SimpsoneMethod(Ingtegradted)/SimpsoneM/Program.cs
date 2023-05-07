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
    class Program
    {
        static void Main(string[] args)
        {
            SimpsonMethod simpsonMethod = new SimpsonMethod(new line_funct(), 2, 10);
          Console.WriteLine(  simpsonMethod.ComputeResult() + "\n@\n");
            SimpsonMethod simpsonMethod1 = new SimpsonMethod(new cvadro_func(), 3, 126);
            Console.WriteLine(simpsonMethod1.ComputeResult() + "\n@\n");
            SimpsonMethod simpsonMethod2 = new SimpsonMethod(new five_degfunc(), 21, 7504535.5875);
            Console.WriteLine(simpsonMethod2.ComputeResult() + "\n@\n");
            SimpsonMethod simpsonMethod3 = new SimpsonMethod(new free_one_degfunc(), -3, -389897100);
            Console.WriteLine(simpsonMethod3.ComputeResult() + "\n@\n");
            SimpsonMethod simpsonMethod4 = new SimpsonMethod(new seven_foo_one_degfunc(), -3, -8935.125);
            Console.WriteLine(simpsonMethod4.ComputeResult()+"\n@\n");
        }
    }
}

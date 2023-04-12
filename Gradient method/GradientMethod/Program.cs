using System;

namespace GradientMethod
{
    class ExampleFunction : Function
    {
        public ExampleFunction() { }
        /// <summary>
        /// 2X_1^2+7x_2^3
        /// gr: 4X_1 +21x_2^2
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double Function(double[]x)
        {
            return 2*Math.Pow(x[0], 2.0)+7*Math.Pow(x[1],3.0);
        }
        public double[] DerivativeFunction(double[] x)
        {
            return new double[] { 4 * x[0], 21 * Math.Pow(x[1], 2.0) };
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            double[] x0 = { 1, 1 };
            Function function = new ExampleFunction();
            GradientSpusk gradientSpusk = new GradientSpusk(function, x0, 0.00001);
            Console.WriteLine(gradientSpusk.Compute());
        }
    }
}

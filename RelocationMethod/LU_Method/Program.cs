using System;

namespace LU_Method
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] m = { 2, 4, 6, 8 };
            double[] y = { 2, 2 };
            MLU mLU = new MLU(m, y);
            if (mLU.SafeCompute())
            {
                Console.WriteLine("x=\n");
                foreach (var v in mLU.x_)
                    Console.WriteLine(v);
            }

        }
    }
}

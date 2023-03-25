using System;

namespace RelocationMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector[] v = new Vector[4];
            v[0] = new Vector(new decimal[] { 4, -1, -6, 0, 2 });
            v[1] = new Vector(new decimal[] { -5, -4, 10, 8, 21 });
            v[2] = new Vector(new decimal[] { 0, 9, 4, -2, -12 });
            v[3] = new Vector(new decimal[] { 1, 0, -7, 5, -6 });
            RelocationMethod relocationMethod = new RelocationMethod(v);
            if (relocationMethod.SupportSlay)
            {
                relocationMethod.ToPVid();
                relocationMethod.Iterations((decimal)0.00001);
                var x = relocationMethod.GetX();
                foreach (decimal l in x)
                    Console.WriteLine(l);
            }
        }
    }
}

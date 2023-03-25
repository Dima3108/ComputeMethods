using System;

namespace RelocationMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector[] v = new Vector[4];
            v[0] = new Vector(new double[] { 4, -1, -6, 0, 2 });
            v[1] = new Vector(new double[] { -5, -4, 10, 8, 21 });
            v[2] = new Vector(new double[] { 0, 9, 4, -2, -12 });
            v[3] = new Vector(new double[] { 1, 0, -7, 5, -6 });
            var v2 = new Vector[2]
            {
                new Vector(new double[3]{2.000,4.0000,2.000}),
                new Vector(new double[3]{6.0000,8.00000,2.0000})
            };
            
            var v3 = new Vector[3]
            {
                new Vector(new double[4]{4,3,4,30}),
                new Vector(new double[4]{-3,1,7,24}),
                new Vector(new double[4]{0,2,1,8})
            };
            Vector[][] superVect = new Vector[][]
            {
                v,v2,v3
            };
            foreach(var vectors in superVect)
            {
 RelocationMethod relocationMethod = new RelocationMethod(vectors);
            if (relocationMethod.SupportSlay)
            {
                relocationMethod.ToPVid();
                relocationMethod.Iterations(0.00001);
                var x = relocationMethod.GetX();
                foreach (var l in x)
                    Console.WriteLine(l);
            }
                Console.WriteLine();
            }
           
        }
    }
}

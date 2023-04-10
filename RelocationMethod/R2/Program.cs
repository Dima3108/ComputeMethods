using System;

namespace RelocationMethod
{
    public static class DecEx
    {
        public static decimal[]ToDecimal(this double[] m)
        {
            decimal[] d = new decimal[m.Length];
            for (int i = 0; i < d.Length; i++)
                d[i] = (decimal)m[i];
            return d;
        }
        public static double[]ToDouble(this decimal[] m)
        {
            var d = new double[m.Length];
            for (int i = 0; i < d.Length; i++)
                d[i] = (double)m[i];
            return d;
        }
        public static double[] ToDouble(this double[] m) => m;
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(sizeof(decimal));
            Console.WriteLine(sizeof(double));
            Console.WriteLine(sizeof(float));
            Console.WriteLine(sizeof(Single));
            Vector[] v = new Vector[4];
            v[0] = new Vector((new double[] { 4, -1, -6, 0, 2 }));
            v[1] = new Vector((new double[] { -5, -4, 10, 8, 21 }));
            v[2] = new Vector((new double[] { 0, 9, 4, -2, -12 }));
            v[3] = new Vector((new double[] { 1, 0, -7, 5, -6 }));
            var v2 = new Vector[2]
            {
                new Vector((new double[3]{2.000,4.0000,2.000})),
                new Vector((new double[3] { 6.0000, 8.00000, 2.0000 }))
            };
            
            var v3 = new Vector[3]
            {
                new Vector((new double[4] { 4, 3, 4, 30 })),
                new Vector((new double[4] { -3, 1, 7, 24 })),
                new Vector((new double[4] { 0, 2, 1, 8 }))
            };
            var v4 = new Vector[2]
            {
                new Vector(new double[]{3,2,7.50000000}),
                new Vector(new double[]{2,3,4})
            };
            var v5 = new Vector[3]
            {
                new Vector(new double[]{3,4,-4,-92}),
                new Vector(new double[]{4,-11,4,106}),
                new Vector(new double[]{-4,4,5,97})
            };
            var v6 = new Vector[4]
            {
                new Vector(new double[]{1,2,3,-1,112}),
                new Vector(new double[]{2,54,-5,1,478}),
                new Vector(new double[]{1,-5,-11,2,118.5}),
                new Vector(new double[]{-1,3,2,7,0.5})
            };
            Vector[][] superVect = new Vector[][]
            {
                v,v4,v5,v6
            };
            foreach(var vectors in superVect)
            {
 RelocationMethod relocationMethod = new RelocationMethod(vectors);
            if (relocationMethod.SupportSlay)
            {
                relocationMethod.ToPVid();
                relocationMethod.Iterations2(0.000000000000001);
                var x = relocationMethod.GetX();
                foreach (var l in x)
                    Console.WriteLine(l);
            }
                Console.WriteLine();
            }
           
        }
    }
}

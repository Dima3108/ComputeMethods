using System;

namespace MethodGaus
{
    class Program
    {
        static void Write(double[]t)
        {
            foreach (double v in t)
                Console.Write(v.ToString() + " ");
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            double[] v = { 0.33, 11.0 / 3.0, -15.0,0.0000000000000003*0.00798 };
            Vector vector = new Vector(v);
            Console.WriteLine(vector.ToString());
            vector.Reduce();
            Console.WriteLine(vector.ToString());
            double[][] matr = new double[3][]
            {
                new double []{1,2,3,3 },
                new double[]{ 3,5,7,0},
                new double[]{1,3,4,1 }
            };
            Matrix matrix = new Matrix(matr);
            matrix.GausM();
            double[] x_ = matrix.GetX_Vals();
            Write(x_);
            double[][] matr2 = new double[2][]
            {
                new double[]{2,4,2},
                new double[]{6,8,2}
            };
            Matrix matrix2 = new Matrix(matr2);
            matrix2.GausM();
            double[] x_2 = matrix2.GetX_Vals();
            Write(x_2);
            double[][] matr3 = new double[3][]
            {
                new double[]{4,3,4,30},
                new double[]{-3,1,7,24},
                new double[]{0,2,1,8}
            };
            Matrix matrix3 = new Matrix(matr3);
            matrix3.GausM();
            Write(matrix3.GetX_Vals());
            double[][] matr4 = new double[5][]
            {
                new double[]{5,23,0,1,2,2},
                new double[]{0.333,5,0.20000,3,7,-9.666666666666666666666666666666666666666},
                new double[]{2, 0.142, 0.500,1,-2,9.50000},
                new double[]{1,1,1,1,-1,9},
                new double[]{1,-1,-1,-1,7,-19}
            };
            //
            Matrix matrix4 = new Matrix(matr4);
            matrix4.GausM();
            Write(matrix4.GetX_Vals());
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}

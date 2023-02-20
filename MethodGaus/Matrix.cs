using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
namespace MethodGaus
{
   public class Matrix
    {
        public Vector[] strings;
        public Matrix(Vector[] vec)
        {
            strings = vec;
            
        }
        public Matrix(double[][] elem)
        {
            Vector[] v = new Vector[elem.Length];
            for (int i = 0; i < v.Length; i++)
                v[i] = new Vector(elem[i]);
            strings = v;
        }
        private double[] x_col;
        public void GausM()
        {x_col = new double[strings.Length];
            for(int i = 0; i < strings.Length - 1; i++)
            {
                BigInteger l = strings[i][i];
                for(int j = i + 1; j < strings.Length; j++)
                {
                    BigInteger w = strings[j][i];
                    strings[j] *= l;
                    strings[j] -= (strings[i]*w);
                    strings[j].Reduce();
                        
                }

            }
            strings[0].Reduce();
            for(int i = strings.Length - 1; i >= 1; i--)
            {
                BigInteger l = strings[i][i];
                for(int j = i - 1; j >= 0; j--)
                {
                    BigInteger w = strings[j][i];
                    strings[j] *= l;
                    strings[j] -=  strings[i]*w;
                    strings[j].Reduce();
                }
            }
            strings[strings.Length - 1].Reduce();
            for (int i = 0; i < strings.Length; i++)
                x_col[i] = (double)(strings[i][strings[i].Length - 1]) / (double)(strings[i][i]);

        }
        public double[] GetX_Vals() => x_col;
    }
}

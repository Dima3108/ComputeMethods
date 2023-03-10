using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU_Method
{
   public class MLU
    {
        private double[] matrica;//матрица A
        private double[] cesh_m;
        /// <summary>
        /// Вместо b в оригенале
        /// </summary>
        private double[] y_;
        /// <summary>
        /// Вместо y в оригенале
        /// </summary>
        private double[] b_;
        public double[] x_;
        private int N;
        public MLU(double[] d,double[]y)
        {
            if ((int)Math.Pow(2, (int)Math.Sqrt((double)d.Length)) != d.Length)
                throw new Exception("матрица не квадратная");
            cesh_m = new double[d.Length];
            matrica = new double[d.Length];
            N = (int)Math.Sqrt(d.Length);
            d.CopyTo(cesh_m, 0);
            y_ = new double[y.Length];
            y.CopyTo(y_, 0);
            b_ = new double[y_.Length];
            x_ = new double[y_.Length];
        }
        public void Compute(bool is_u = true)
        {
            double s = 0;
            if (is_u)
            {
                for(int i=0;i<N;i++)
                    for(int j = 0; j < N; j++)
                    {
                        if (i <= j)
                        {
                            matrica[(N * i) + j] = cesh_m[(N * i) + j];
                            s = 0;
                            for (int k = 0; k < i - 1; k++)
                                s += matrica[(i * N) + k] * matrica[(k * N) + j];
                            matrica[(N * i) + j] -= s;
                            
                        }
                        else
                        {
                            if (matrica[(N * i) + i] == 0)
                                throw new Exception("Найден нулевой минор");
                            s = 0;
                            for (int k = 0; k < j - 1; k++)
                                s += matrica[(N * i) + k] * matrica[(N * k) + j];
                            matrica[(N * i) + j] = 1.0 / matrica[(N * i) + i] * (cesh_m[(N * i) + j] - s);

                        }
                    }
                for(int i = 0; i < N; i++)
                {
                    s = 0;
                    for (int k = 0; k < i - 1; k++)
                        s += matrica[(N * i) + k] * b_[k];
                    b_[i] = y_[i] - s;
                }
                for(int i = N-1; N>=0; i--)
                {
                    s = 0;
                    for (int k = i + 1; k < N; k++)
                        s += matrica[(N * i) + k] * x_[k];
                    x_[i] = 1.0 / matrica[(N * i) + i] * (y_[i] - s);
                }
            }
        }
        public bool SafeCompute(bool is_u = true)
        {
            try
            {
                Compute(is_u);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
    }
}

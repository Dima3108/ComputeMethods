using System;
using System.Collections.Generic;
using System.Text;

namespace RelocationMethod
{
   public class RelocationMethod
    {
        private Vector[] data;
        private double[] R;
        private double[] X0;
        public double[] X_;
        public const double XoVal = 0;
            
        public RelocationMethod(Vector[] slay)
        {
            data = slay;
            bool sup = true;
            foreach(var e in data)
                if (e.Length < data.Length)
                {
                    sup = false;
                    break;
                }
            for(int i=0;i<data.Length;i++)
                for(int j=i+1;j<data.Length;j++)
                    if (data[i].Length != data[j].Length)
                    {
                        sup = false;
                        break;
                    }
            SupportSlay = sup;
            X0 = new double[data.Length];
            for (int i = 0; i < X0.Length; i++)
                X0[i] = (double)XoVal;
            X_ = new double[X0.Length];
        }
        public bool SupportSlay { get; private set; }
        
        public void ToPVid()
        {
            if (!SupportSlay)
                return;
            Double a_ii;
#if !false
            for(int i = 0; i < data.Length; i++)
            {
                a_ii = data[i][i];
                data[i] /= a_ii;
                data[i] *= -1;
                data[i][data[0].Length - 1] *= -1;// c_i
                
            }
#endif
            //for (int e = 0; e < data.Length; e++)
               // X0[e] = data[e][data[0].Length - 1];
#if false
            for(int i=0;i<data.Length;i++)
                if(data[i][i]!=-1)
                {
                    SupportSlay = false;
                    Console.WriteLine("a[i][i] не равно -1");
                    return;
                }  
#endif
            // R = GetR();
        }
        public double[] GetR()
        {
            var R = new double[data.Length];
            int len = data[0].Length;
            for(int i = 0; i < R.Length; i++)
            {
                R[i] = data[i][len - 1];
                R[i] -= X0[i];
                for(int j=0;j<data.Length;j++)
                    if (j != i)
                    {
                        R[i] += data[i][j]*X0[j];
                    }
            }
            return R;
        }
        public unsafe double[] GetR(double*X,int Length)
        {
            var R = new double[data.Length];
            int len = data[0].Length;
            for (int i = 0; i < R.Length; i++)
            {
                R[i] = data[i][len - 1];
                R[i] -= X[i];
                for (int j = 0; j < data.Length; j++)
                    if (j != i)
                    {
                        R[i] += data[i][j] * X[j];
                    }
            }
            return R;
        }
        public double GetMax(double[] m)
        {
            var v = Math.Abs(m[0]);
            for(int i = 0; i < m.Length; i++)
            {
                if (v < Math.Abs(m[i]))
                    v = Math.Abs(m[i]);
            }
            return v;
        }
        public int GetMaxPos(double[] m)
        {
            int p = 0;
            var v = m[0];
            for (int i = 0; i < m.Length; i++)
            {
                if (v < //Math.Abs(
                    m[i]
                    //)
                    )
                {
  v = //Math.Abs(
      m[i]
     // )
                        ;
                    p = i;
                }
                  
            }
            return p;
        }
        public bool IsMin(double[] d,double eps)
        {
            for (int i = 0; i < d.Length; i++)
                if (d[i] >= eps)
                    return false;

            return true;
        }
        private unsafe void NextX(double*pred_x,double*next_x,int Length,double w = 0.5)
        {
            var a_sumij=pred_x[0];
            for(int i = 0; i < Length; i++)
            {
                next_x[i] = (1 - w) * pred_x[i];
                a_sumij = 0;
                for(int j = 0; j < i; j++)
                {
                    a_sumij += data[i][j] * next_x[j];
                }
                for(int j = i + 1; j < Length; j++)
                {
                    a_sumij += data[i][j] * pred_x[j];
                }
                next_x[i] +=w/data[i][i]*(-1* data[i][data[0].Length - 1]-a_sumij);
            }
        }
        private unsafe double get_max_delta(double* x, double* x_next,int Length)
        {
            var delta = Math.Abs(x_next[0] - x[0]);
           var v=delta;
            for (int i = 1; i < Length; i++)
                if ((v = Math.Abs(x_next[i] - x[i])) > delta)
                    delta = v;
            return delta;
        }
        public static unsafe void UnsafeCopy(double*inp,double*outp,int Length,int input_offset=0,int output_offset = 0)
        {
            for (int i = 0; i < Length; i++)
                outp[output_offset + i] = inp[input_offset + i];
        }
        public static unsafe void UnsafeCopy(double* inp, double[] outp, int Length, int input_offset = 0, int output_offset = 0)
        {
            for (int i = 0; i < Length; i++)
                outp[output_offset + i] = inp[input_offset + i];
        }
        public unsafe void Iterations2(double eps)
        {
            //var X = X0;
            double* X = stackalloc double[X0.Length];
            double* X_NEXT = stackalloc double[X0.Length];
            //var X_NEXT = X;
            NextX(X, X_NEXT,X0.Length);
            var delta = X[0];
            var pred_delta=delta;
            delta = get_max_delta(X, X_NEXT,X0.Length);pred_delta = delta;
            int iter = 0;
            while (
                //((delta=get_max_delta(X, X_NEXT)) >= eps)
                GetMax(GetR(X_NEXT,X0.Length))>eps
                )
            {
                //X_NEXT.CopyTo(X, 0);
                UnsafeCopy(X_NEXT, X, X0.Length);
                NextX(X, X_NEXT,X0.Length);
                if (delta < pred_delta)
                    pred_delta = delta;
                if (iter < 3||iter==37)
                {
                    Console.WriteLine();
                    for(int p=0;p<X0.Length;p++)
                        Console.WriteLine(X_NEXT[p]);
                    Console.WriteLine();
                }
                iter++;
                if (iter > 90000000)
                    break;
            }
            Console.WriteLine("iterations:" + iter.ToString());
            X_ = X0;
            UnsafeCopy(X_NEXT, X_, X_.Length);
            //X_NEXT.CopyTo(X_, 0);
        }
        public void Iterations(double eps)
        {
            return;
            var R1 = new double[R.Length];
            int iter_count = 0;
            List<double[]> l = new List<double[]>();
            while (!IsMin(R.ToDouble(),eps) &&iter_count<200)
            {
                int pos = GetMaxPos(R.ToDouble());
                R1[pos] = 0;
                for(int i = 0; i < R1.Length; i++)
                {
                    if (i != pos)
                    {
                        R1[i] = (double)R[i];
                       // for (int j = 0; j < data.Length; j++)
                            //if(i!=j)
                            R1[i] +=(double)R[pos] * 
                           // 0.5*
                                   (double) data[i][pos];
                    }
                }
               
               
 l.Add(R); R1.CopyTo(R, 0);
                iter_count++;
                //if (iter_count == 38)
                 //   break;
            }
            if (iter_count >= 200)
                Console.WriteLine("iterations > 200");
            for(int i = 0; i < X0.Length; i++)
            {
                X_[i] = X0[i];
                for (int j = 0; j < l.Count; j++)
                    X_[i] += l[j][i];
            }
        }
        public double[] GetX() => X_;
    }
}

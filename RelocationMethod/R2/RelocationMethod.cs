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
                X0[i] = XoVal;
            X_ = new double[X0.Length];
        }
        public bool SupportSlay { get; private set; }
        
        public void ToPVid()
        {
            if (!SupportSlay)
                return;
            double a_ii;
            for(int i = 0; i < data.Length; i++)
            {
                a_ii = data[i][i];
                data[i] /= a_ii;
                data[i] *= -1;
                data[i][data[0].Length - 1] *= -1;// c_i
                
            }
            for (int e = 0; e < data.Length; e++)
                X0[e] = data[e][data[0].Length - 1];
            for(int i=0;i<data.Length;i++)
                if(data[i][i]!=-1)
                {
                    SupportSlay = false;
                    Console.WriteLine("a[i][i] не равно -1");
                    return;
                }    
             R = GetR();
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
        public void Iterations(double eps)
        {
            var R1 = new double[R.Length];
            int iter_count = 0;
            List<double[]> l = new List<double[]>();
            while (!IsMin(R,eps) &&iter_count<200)
            {
                int pos = GetMaxPos(R);
                R1[pos] = 0;
                for(int i = 0; i < R1.Length; i++)
                {
                    if (i != pos)
                    {
                        R1[i] = R[i];
                       // for (int j = 0; j < data.Length; j++)
                            //if(i!=j)
                            R1[i] +=R[pos] * 
                           // 0.5*
                                    data[i][pos];
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

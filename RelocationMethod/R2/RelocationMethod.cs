using System;
using System.Collections.Generic;
using System.Text;

namespace RelocationMethod
{
   public class RelocationMethod
    {
        private Vector[] data;
        private decimal[] R;
        private decimal[] X0;
        public decimal[] X_;
        public const decimal XoVal = (decimal)1;
            
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
            X0 = new decimal[data.Length];
            for (int i = 0; i < X0.Length; i++)
                X0[i] = XoVal;
            X_ = new decimal[X0.Length];
        }
        public bool SupportSlay { get; private set; }
        
        public void ToPVid()
        {
            if (!SupportSlay)
                return;
            decimal a_ii;
            for(int i = 0; i < data.Length; i++)
            {
                a_ii = data[i][i];
                data[i] /= a_ii;
                //data[i][data[0].Length - 1] *= -1;// c_i
                
            }
             R = GetR();
        }
        public decimal[] GetR()
        {
            decimal[] R = new decimal[data.Length];
            int len = data.Length;
            for(int i = 0; i < R.Length; i++)
            {
                R[i] = data[i][len - 1];
                R[i] -= X0[i];
                for(int j=0;j<len;j++)
                    if (j != i)
                    {
                        R[i] += data[i][j]*X0[j];
                    }
            }
            return R;
        }
        public decimal GetMax(decimal[] m)
        {
            decimal v = Math.Abs(m[0]);
            for(int i = 0; i < m.Length; i++)
            {
                if (v < Math.Abs(m[i]))
                    v = Math.Abs(m[i]);
            }
            return v;
        }
        public int GetMaxPos(decimal[] m)
        {
            int p = 0;
            decimal v = Math.Abs(m[0]);
            for (int i = 0; i < m.Length; i++)
            {
                if (v < Math.Abs(m[i]))
                {
  v = Math.Abs(m[i]);
                    p = i;
                }
                  
            }
            return p;
        }
        public bool IsMin(decimal[] d,decimal eps)
        {
            for (int i = 0; i < d.Length; i++)
                if (d[i] >= eps)
                    return false;

            return true;
        }
        public void Iterations(decimal eps)
        {
            decimal[] R1 = new decimal[R.Length];
            int iter_count = 0;
            List<decimal[]> l = new List<decimal[]>();
            while (!IsMin(R,eps) &&iter_count<200)
            {
                int pos = GetMaxPos(R1);
                R1[pos] = 0;
                for(int i = 0; i < R1.Length; i++)
                {
                    if (i != pos)
                    {
                        R1[i] = R[i];
                        for (int j = 0; j < data.Length; j++)
                            R1[i] += R[j] * data[i][j];
                    }
                }
               
                R1.CopyTo(R, 0);
 l.Add(R);
                iter_count++;
            }
            for(int i = 0; i < X0.Length; i++)
            {
                X_[i] = X0[i];
                for (int j = 0; j < l.Count; j++)
                    X_[i] += l[j][i];
            }
        }
        public decimal[] GetX() => X_;
    }
}

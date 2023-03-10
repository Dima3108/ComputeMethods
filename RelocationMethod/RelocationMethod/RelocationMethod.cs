using System;
using System.Collections.Generic;
using System.Text;

namespace RelocationMethod
{
   public class RelocationMethod
    {
        private Vector[] data;
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
                data[i][data[0].Length - 1] *= -1;
               /* for(int j = 0; j < data[0].Length-1;j++)
                {
                    data[i][j] = -(data[i][j] / a_ii);
                }
                data[i][data[0].Length - 1] = -(data[i][data[0].Length - 1] / a_ii);*/
            }
        }
    }
}

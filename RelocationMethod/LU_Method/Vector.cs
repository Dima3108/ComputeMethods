using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU_Method
{
    class Vector
    {

        public decimal[] elemets { get; set; }
        public int Length { get => elemets.Length; }
        public decimal this[int i]
        {
            get
            {
                if (i >= 0 && i < elemets.Length)
                    return elemets[i];
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (i >= 0 && i < elemets.Length)
                    elemets[i] = value;
            }
        }
        public Vector(decimal[] m)
        {
            elemets = m;
        }
        public void Add(decimal b)
        {
            decimal[] d2 = new decimal[elemets.Length + 1];
            elemets.CopyTo(d2, 0);
            d2[d2.Length - 1] = b;
            elemets = d2;
        }
        public static Vector operator *(Vector x, decimal y)
        {
            Vector r = x;
            for (int i = 0; i < x.Length; i++)
                r[i] *= y;
            return r;
        }
        public static Vector operator /(Vector x, decimal y)
        {
            Vector r = x;
            for (int i = 0; i < x.Length; i++)
                r[i] *= y;
            return r;
        }
        public static Vector operator -(Vector x, Vector y)
        {
            if (x.Length != y.Length)
                return null;
            Vector re = x;
            for (int i = 0; i < re.Length; i++)
                re[i] -= y[i];
            return re;
        }
        public static Vector operator +(Vector x, Vector y)
        {
            if (x.Length != y.Length)
                return null;
            Vector re = x;
            for (int i = 0; i < re.Length; i++)
                re[i] -= y[i];
            return re;
        }
    }
}


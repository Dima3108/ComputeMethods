using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime;
using System.Runtime.InteropServices;
namespace RelocationMethod
{
   public class Vector
    {
        private IntPtr elemets;
 private unsafe double* el { get => (double*)elemets; }
        public int Length { get; private set; }
        public unsafe double this[int i] { get
            {
               
                if (i >= 0 && i < Length)
                    return el[i];
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (i >= 0 && i < Length)
                    el[i] = value;
            }
        }
        public unsafe Vector(double[] m)
        {
            elemets = Marshal.AllocHGlobal(sizeof(double) * m.Length);
           // el = (double*)elemets;
            Marshal.Copy(m, 0, elemets, m.Length);
            Length = m.Length;
        }
        public void Add(double b)
        {
            var d2 = new double[Length + 1];
            /*elemets.CopyTo(d2, 0);
            d2[d2.Length - 1] = b;
            elemets = d2;*/
            unsafe
            {
                Marshal.Copy(elemets, d2, 0, d2.Length);
                d2[d2.Length - 1] = b;
                Marshal.FreeHGlobal(elemets);
                elemets = Marshal.AllocHGlobal(sizeof(double) * d2.Length);
                Marshal.Copy(d2, 0, elemets, d2.Length);
                Length = d2.Length;
            }
        }
        public static Vector operator*(Vector x,double y)
        {
            Vector r = x;
            for (int i = 0; i < x.Length; i++)
                r[i] *= y;
            return r;
        }
        public static Vector operator/(Vector x,double y)
        {
            Vector r = x;
            for (int i = 0; i < x.Length; i++)
                r[i] /= y;
            return r;
        }
        public static Vector operator-(Vector x,Vector y)
        {
            if (x.Length != y.Length)
                return null;
            Vector re = x;
            for (int i = 0; i < re.Length; i++)
                re[i] -= y[i];
            return re;
        }
        public static Vector operator+(Vector x, Vector y)
        {
            if (x.Length != y.Length)
                return null;
            Vector re = x;
            for (int i = 0; i < re.Length; i++)
                re[i] += y[i];
            return re;
        }
        ~Vector()
        {
            unsafe
            {
                Marshal.FreeHGlobal(elemets);

            }
        }
    }
}

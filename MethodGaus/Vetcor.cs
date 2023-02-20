using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Numerics;
namespace MethodGaus
{
    public class Vector
    {
        private static bool ContainsDouble(double[] v)
        {
            for(int i=0;i<v.Length;i++)
            {
                BigInteger bigInteger = new BigInteger(v[i]);
                if ((((double)bigInteger)) != v[i])
                    return true;
            }
            return false;
        }
        public Vector(double[] v)
        {
            elements = new List<BigInteger>(v.Length);
            bool is_stop = false;
            while (ContainsDouble(v)&&!is_stop)
            {
                for (int i = 0; i < v.Length; i++)
                {
 v[i] *= 10.0;
                    if (Double.MaxValue / 100.0 <= v[i])
                    {
                        is_stop = true;
                    }
                }
                   
            }
            for (int i = 0; i < v.Length; i++)
                elements.Add(new BigInteger(v[i]));
        }
        public List<BigInteger> elements { get; private set; }
        public int Length { get => elements.Count; }
        public Vector(int size){
            elements = new List<BigInteger>(size);
        }
        public Vector(BigInteger[] i)
        {
            elements = new List<BigInteger>();
            for (int j = 0; j < i.Length; j++)
                elements.Add( i[j]);
        }
        public BigInteger this[int i]
        {
            get
            {
                if (i >= 0 && i < Length)
                    return elements[i];
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (i >= 0 && i < Length)
                    elements[i] = value;
            }
        }
        public void Add(BigInteger i) => elements.Add(i);
        public static Vector operator*(Vector v,BigInteger y)
        {
            Vector vector = new Vector(v.elements.ToArray());
            for (int i = 0; i < v.Length; i++)
                vector[i] *= y;
            return vector;
        }
        public static Vector operator+(Vector v,Vector v2)
        {
            if (v.Length != v2.Length)
                return null;
            Vector r = new Vector(v.elements.ToArray());
            for (int i = 0; i < r.Length; i++)
                r[i] += v2[i];
            return r;
        }
        public static Vector operator -(Vector v, Vector v2)
        {
            if (v.Length != v2.Length)
                return null;
            Vector r = new Vector(v.elements.ToArray());
            for (int i = 0; i < r.Length; i++)
                r[i] -= v2[i];
            return r;
        }
        public override string ToString()
        {
            string s = "";
            foreach (var v in elements)
                s += v.ToString() + " ";
            return s;
        }
        public static bool IsDelitel(Vector v,BigInteger value)
        {
            foreach (var val in v.elements)
                if (val % value != 0)
                    return false;
            return true;
        }
        public static BigInteger GetMin(Vector v)
        {
            BigInteger v0 = v[0];
            for (int i = 1; i < v.Length; i++)
                if (v0 > v[i])
                    v0 = v[i];
            return v0;
        }

        public void Reduce()
        {
            var min_ = GetMin(this);
            if (min_ < 0)
                min_ *= -1;
            BigInteger d = 0;
           // int iter_count = 0;
            while (d != 1)
            {
                min_ = GetMin(this);
                if (min_ < 0)
                    min_ *= -1;
                d = 1;
 while (IsDelitel(this, d)&&d<=min_)
                d++;
                d--;
                if (d == 0)
                    d = 1;
            for (int i = 0; i < Length; i++)
                elements[i] /= d;
              //  iter_count++;
            }
           
        }
    }
}

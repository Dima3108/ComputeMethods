using System;
using System.Collections.Generic;
using System.Text;

namespace ODY_Lib
{
   public interface FunctionAndDerivativeInterface:FunctionInter
    {
        /// <summary>
        /// Производная функции
        /// </summary>
        /// <param name="m">массив значений вектора  x</param>
        /// <returns></returns>
        double FD(params double[] m);
       // double FD(double m);
    }
}

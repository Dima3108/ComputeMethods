using System;
using System.Collections.Generic;
using System.Text;

namespace ODY_Lib
{
  public  interface FunctionInter
    {
        /// <summary>
        /// Функция
        /// </summary>
        /// <param name="v">массив вектора значений x</param>
        /// <returns></returns>
         double F(params double[] v);
        
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GradientMethod
{
  public  interface Function
    {
        public double Function(double[] args);
        public double[] DerivativeFunction(double[] args);
    }
}

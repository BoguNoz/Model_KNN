using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2.distance
{
    public class Euklides :IDistance
    {
        public double CalcDist(double[] x, double[] y)
        {
            if (x.Length != y.Length) throw new Exception("Size dont match");

            double result = x.Zip(y, (a, b) => Math.Pow(b - a, 2)).Sum(a => a);

            return Math.Sqrt(result);
        }
    }
}

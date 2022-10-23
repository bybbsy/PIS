using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_2
{
    class Legendre
    {
        //public int x { get; set; }
        //public int i { get; set; }

        public Legendre()
        {
        }
        public double getP(double x, int i)
        {
            int iReversed = 0;
            List<double> parts = new List<double>();

            while (iReversed <= i)
            {
                if (iReversed == 0)
                {
                    parts.Add(1);
                    iReversed++;
                    continue;
                }
                else if (iReversed == 1)
                {
                    parts.Add(x);
                    iReversed++;
                    continue;
                }
                else
                {
                    double left = ((2 * (iReversed - 1) + 1) * x * parts[iReversed - 1]) / iReversed;
                    double right = ((iReversed - 1) * parts[iReversed - 2]) / iReversed;
                    double result = left - right;
                    parts.Add(result);
                    iReversed++;
                }
            }

            return parts.Last();
        }
        
        public double getSquare(int i, double x1, double y1, double x2, double y2)
        {
            double left = ((y1 * getP(x1, i)) + (y2 * getP(x2, i)));
            double right = (x2 - x1) / 2;

            return left * right;
        }
    }
}

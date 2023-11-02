using System;
using System.Text.RegularExpressions;

namespace Power
{
    public class PowerCalculations
    {
        public double SquareRoot(double number)
        {
/*            double start = 0;
            double end = number;

            while (end - start > 1e-10)
            {
                double mid = (start + end) / 2;
                double midSqr = mid * mid;

                if (midSqr == number)
                    return mid;
                else if (midSqr < number)
                    start = mid;
                else
                    end = mid;
            }

            return (start + end) / 2;*/

            return Math.Pow(number, 1/2);
        }

        public double Cube(double a)
        {
            return a * a * a;
        }

        public double CubeRoot(double a)
        {
            return Math.Pow(a, 1 / 3);
        }

        public double Power(double a, double power)
        {
            if (power == 0 || power == 1)
                return a;

            double result = 1;
            for (int i = 0; i < power; i++)
            {
                if(power == 0)
                {
                    return 1;
                }
                result *= a;
            }
            return result;
        }

        public double ExponentialByOne(double a, double power)
        {
            return Math.Pow(a, power);
        }
    }    
}

using System;

namespace Power
{
    public class PowerCalculations
    {
        public double SquareRoot(double a)
        {
            return Math.Sqrt(a);
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
            return Math.Pow(a, power);
        }

        public double ExponentialByOne(double a, double power)
        {
            return Math.Pow(a, 1/power);
        }
    }
}

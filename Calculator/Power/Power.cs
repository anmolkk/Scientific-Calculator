using System;
using System.Text.RegularExpressions;

namespace Power
{
    public class PowerCalculations
    {
        public double SquareRoot(double number)
        {
            int tryInt = 0;
            double tryValue = 0;
            double lastTryValue = 0;
            while (true)
            {
                double sumOfTryInt = tryInt * tryInt;
                if (sumOfTryInt == number)
                {
                    return tryInt;
                }
                else if (sumOfTryInt < number)
                {
                    tryValue = tryInt;
                    tryInt += 1;                    
                }
                else
                {
                    double sum = tryValue * tryValue;
                    if (sum == number)
                    {
                        return tryValue;
                    }
                    else if (sum < number)
                    {
                        lastTryValue = tryValue;
                        tryValue += 0.001;
                    }
                    else
                    {
                        return lastTryValue;
                    }
                }
            }
        }

        public double CubeRoot(double number)
        {
            int tryInt = 0;
            double tryValue = 0;
            double lastTryValue = 0;
            while (true)
            {
                double sumOfTryInt = tryInt * tryInt * tryInt;
                if (sumOfTryInt == number)
                {
                    return tryInt;
                }
                else if (sumOfTryInt < number)
                {
                    tryValue = tryInt;
                    tryInt += 1;
                }
                else
                {
                    double sum = tryValue * tryValue* tryValue;
                    if (sum == number)
                    {
                        return tryValue;
                    }
                    else if (sum < number)
                    {
                        lastTryValue = tryValue;
                        tryValue += 0.001;
                    }
                    else
                    {
                        return lastTryValue;
                    }
                }
            }
        }

        public double Power(double a, double power)
        {
            if (power == 0)
                return 1;

            double result = 1;
            for (int i = 0; i < power; i++)
            {
                result *= a;
            }
            return result;
        }

        public double ExponentialByOne(double a, double power)
        {
            return Math.Pow(a, power);
        }

        public double Factorial(double a)
        {
            if (a == 0)
                return 1;

            return a * Factorial(a-1);
        }
    }
}

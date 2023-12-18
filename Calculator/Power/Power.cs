using System;
using System.Text.RegularExpressions;

namespace Power
{
    public class PowerCalculations
    {
        public double SquareRoot(double number)
        {
            if(number < 0)
            {
                throw new Exception("Invalid Input");
            }
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
            bool negativeNumber = false;
            if (number < 0)
            {
                number *= -1;
                negativeNumber = true;
            }
            int tryInt = 0;
            double tryValue = 0;
            double lastTryValue = 0;
            while (true)
            {
                double sumOfTryInt = tryInt * tryInt * tryInt;
                if (sumOfTryInt == number)
                {
                    if(negativeNumber)
                        return tryInt *-1;
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
                        if (negativeNumber)
                            return tryValue * -1;
                        return tryValue;
                    }
                    else if (sum < number)
                    {
                        lastTryValue = tryValue;
                        tryValue += 0.001;
                    }
                    else
                    {
                        if (negativeNumber)
                            return lastTryValue * -1;
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

        static readonly int g = 7;
        static double[] p = { 0.99999999999980993, 676.5203681218851, -1259.1392167224028, 771.32342877765313, -176.61502916214059, 12.507343278686905, -0.13857109526572012, 9.9843695780195716e-6, 1.5056327351493116e-7 };
        public double Factorial(double a)
        {
            if(a%1 == 0)
            {
                if (a < 0)
                {
                    throw new ArgumentException("Factorial is not defined for negative numbers.");
                }

                if (a == 0 || a == 1)
                {
                    return 1;
                }

                double result = 1;
                for (int i = 2; i <= a; i++)
                {
                    result *= i;
                }

                return result;
            }
            else
            {              
                double MyGammaDouble(double z)
                {

                    if (z < 0.5)
                    {
                        return Math.PI / (Math.Sin(Math.PI * z) * MyGammaDouble(1 - z));
                    }
                    z -= 1;
                    double x = p[0];
                    for (var i = 1; i < g + 2; i++)
                    {
                        x += p[i] / (z + i);
                    }
                    double t = z + g + 0.5;
                    return Math.Sqrt(2 * Math.PI) * (Math.Pow(t, z + 0.5)) * Math.Exp(-t) * x;
                }
                return MyGammaDouble(a + 1);
            }
        }
    }
}

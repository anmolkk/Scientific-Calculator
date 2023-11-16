using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Division
{
    internal class DivisionUsingMultiplication
    {
        public static void MainEntry()
        {
            try
            {
                Console.Write("Enter the numerator: ");
                double numerator = double.Parse(Console.ReadLine());

                Console.Write("Enter the denominator:");
                double denominator = double.Parse(Console.ReadLine());

                double quotient = DivideUsingMultiplication(numerator, denominator);
                Console.WriteLine($"Result of {numerator} / {denominator} is: {quotient}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static double DivideUsingMultiplication(double numerator, double denominator)
        {
            double result = 0;
            int sign = (numerator < 0) ^ (denominator < 0) ? -1 : 1;

            numerator = Math.Abs(numerator);
            denominator = Math.Abs(denominator);
            double multiplier = 1;
            while (true)
            {
                if (numerator >= denominator * multiplier)
                {
                    numerator -= denominator * multiplier;
                    result += multiplier;
                }
                else
                {
                    if (numerator > 0 && multiplier > 0.0001)
                    {
                        multiplier *= 0.5;
                    }
                    else
                    {
                        return result * sign;
                    }
                }
            }
        }
    }
}


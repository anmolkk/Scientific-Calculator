using System;

namespace Arithmetic
{
    public class CalculateFloat
    {
        /// <summary>
        /// Adding tWo Double Numbers.
        /// </summary>
        /// <param name="a">Double Type </param>
        /// <param name="b">Double Type </param>
        /// <returns></returns>
        public double Addition(double a, double b)
        {
            return a + b;
        }

        /// <summary>
        /// Multiplying Two Numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public double Multiplication(double a, double b)
        {
            return a * b;
        }

        /// <summary>
        /// Dividing Two Number. Divide first Number by Second Number.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <exception cref="DivideByZeroException"></exception>
        public double Division(double a, double b)
        {
            double result = a / b;
            if (result == (1.0 / 0) || -result == (1.0 / 0))
            {
                throw new DivideByZeroException();
            }
            return result;
        }

        /// <summary>
        /// Check the Remainder of a Number.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public double Modulus(double a, double b)
        {
            return a % b;
        }
    }
}
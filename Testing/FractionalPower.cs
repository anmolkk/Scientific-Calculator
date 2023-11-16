using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Power
{
    internal class FractionalPower
    {
        public static void Squareroot()
        {
            try
            {
                Console.WriteLine(calculate());
            }
            catch(Exception e) 
            {
                Console.WriteLine("Exception Occured : " +e);
            }
        }

        static float calculate()
        {
            double num = 3;
            double tryValue = num / 2;
            double lastTryValue = 0;
            while (true)
            {
                double sum = tryValue * tryValue;
                if (sum == num)
                {
                    return (float)tryValue;
                }
                else if (sum < num)
                {
                    lastTryValue = tryValue;
                    tryValue += 0.001;
                }
                else
                {
                    return (float)lastTryValue;
                }
            }
        }
    }
}

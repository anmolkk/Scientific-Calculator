using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Overloading
{
    internal class FunctionOverLoading
    {
        public static void Entry()
        {
            Console.WriteLine("Addition of two Integers : " + Add((int)5, (int)6));
            Console.WriteLine("Addition of two Integers : " + Add((float)5.22, (float)6.22));
            Console.WriteLine("Addition of two Integers : " + Add((int)5.1, (int)6, (int)4));
            Console.WriteLine("Addition of two Integers : " + Add((double)5.22, (double)6.22, (double)4.66));
        }

        /*In this Example we have used the Function or Method Overloading Concept
         
         //This Concept states that function can be only Overloaded by the Number and Type of arguments passed to the function while calling the function
                
        // Return type of Function does not matter while overloading the function
        

        //Method Overloading is Checked at the Run time
        //But if we use the same variable but different return type, it will show a compile time error

         */


        /// <summary>
        /// Adding two integer numbers using this method
        /// </summary>
        /// <param name="num1">integer value</param>
        /// <param name="num2">integer value</param>
        /// <returns></returns>
        public static double Add(int num1, int num2)
        {
            return (num1 + num2);
        }


        /*        public double Add(ref int num1, int num2)
                {
                    num1 = num2;
                    return num2;
                }
                public double Add(in char num1, int num2)
                {

                    return num1 + num2;
                }*/

        public static double Add(double num1, double num2)
        {
            return (num1 + num2);
        }

        public static double Add(float num1, float num2)
        {
            return (num1 + num2);
        }



        public static double Add(int num1, int num2, int num3)
        {
            return (num1 + num2 + num3);
        }

        public static double Add(double num1, double num2, double num3)
        {
            return (num1 + num2 + num3);
        }
    }
}

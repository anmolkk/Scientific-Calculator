using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitWise
{
    internal class BitwiseOperator
    {
        public static void MainEntry()
        {
            int val1 = 0, val2 = 0, shiftBy = 0;
            string value2 = "", valueShiftBy = "";


        operation:
            Console.WriteLine("Select Any option from menu below by entering the number : ");
            Console.WriteLine("1. Bitwise AND");
            Console.WriteLine("2. Bitwise OR");
            Console.WriteLine("3. Bitwise Complement");
            Console.WriteLine("4. Left SHift");
            Console.WriteLine("5. Right Shift");
            Console.Write("Enter the Number : ");
            string operation = Console.ReadLine();

            if (operation.Length <= 0 || operation.Length > 1)
            {
                Console.WriteLine("please enter valid operation.");
                goto operation;
            }
        valueInsert:
            try
            {
                Console.Write("Please Enter First Number : ");
                string value1 = Console.ReadLine();
                val1 = Convert.ToInt32(value1);

                if (operation[0] == '1' || operation[0] == '2')
                {
                    Console.Write("Please Enter Second Value : ");
                    value2 = Console.ReadLine();
                    val2 = Convert.ToInt32(value2);
                }
                else if (operation[0] == '4' || operation[0] == '5')
                {
                    Console.Write("Enter Shift Value : ");
                    valueShiftBy = Console.ReadLine();
                    shiftBy = Convert.ToInt32(valueShiftBy);
                }
            }
            catch
            {
                Console.WriteLine("Please Enter Number as Value.");
                goto valueInsert;
            }

            switch (operation[0])
            {
                case '1':
                    Console.WriteLine($"Bitwise 'AND' of {val1} and {val2} is : {val1 & val2}");
                    break;
                case '2':
                    Console.WriteLine($"Bitwise 'OR' of {val1} and {val2} is : {val1 | val2}");
                    break;
                case '3':
                    Console.WriteLine($"Bitwise Complement of {val1} is : {~val1}");
                    break;
                case '4':
                    Console.WriteLine($"left shift of of {val1} is : {val1 << shiftBy}");
                    break;
                case '5':
                    Console.WriteLine($"Right Shift of {val1} is : {val1 >> shiftBy}");
                    break;
                default:
                    Console.WriteLine("Please Enter number Between 1 And 5");
                    goto operation;
            }
        }
    }
}

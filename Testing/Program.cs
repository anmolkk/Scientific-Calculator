using System;
using Optional;
using Overloading;
using Power;
using OverloadTrueFalse;
using System.Threading;
using AttributeExample;
using Division;

namespace Testing
{
    internal class Program
    {
        static void Main()
        {
            while (true){
                DivisionUsingMultiplication.MainEntry();
            }

            

            //Attributes.Entry();


/*        start:
            int i = Console.ReadKey().KeyChar - 48;
            Console.WriteLine();
            switch (i){
                case 1:
                case 2:
                    Console.WriteLine(i);
                    break;
                case 3:
                    Console.WriteLine(i);
                    goto start;
                    break;
                default:
                    Console.WriteLine(i);
                    goto start;
                    break;
            }*/
            //LaunchStatusTest.MainE();
            //FractionalPower.Squareroot();
            //FunctionOverLoading.Entry();
        }
        static void MainE()        
        {
            Console.Write("Enter the Size of Array : ");
            string size = Console.ReadLine();
            int sizeInInt;
            if (!int.TryParse(size, out sizeInInt))
            {
                Console.WriteLine("You have Entered Wrong Size.");
                return;
            }

            Console.Write("Enter Input for Array Index of 0 :");
            String input = Console.ReadLine();

            int inputInInt;
            double inputAsDouble;
            bool inputAsBoolean;
            if (int.TryParse(input, out inputInInt))
            {
                int[] intArray = new int[sizeInInt];
                GenericMethod(inputInInt, intArray, sizeInInt);
            }
            else if (double.TryParse(input, out inputAsDouble))
            {
                double[] doubleArray = new double[sizeInInt];
                GenericMethod(inputAsDouble, doubleArray, sizeInInt);
            }
            else if (bool.TryParse(input, out inputAsBoolean))
            {
                bool[] booleanArray = new bool[sizeInInt];
                GenericMethod(inputAsBoolean, booleanArray, sizeInInt);
            }
            else
            {
                Console.WriteLine("You can not Enter charcter or String in array");
            }
        }

        public static void GenericMethod<T>(T input, T[] array, int size)
        {
            array[0] = input;
            for (int i = 1; i < size; i++)
            {
                try
                {
                    Console.Write($"Enter Input for Array Index of {i} : ");
                    array[i] = (T)Convert.ChangeType(Console.ReadLine(), input.GetType());
                }
                catch (Exception e)
                {
                    Console.WriteLine("You have Entered Wrong Input which causes Exception of {0}" + e.Message);
                    i--;
                    e = null;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Array has Values As : ");
            foreach (var value in array)
            {
                Console.WriteLine(" " + value);
            }
        }
    }
}


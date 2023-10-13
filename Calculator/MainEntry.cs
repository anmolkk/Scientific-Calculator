using System;
using Arithmetic;
using Power;

namespace TestCalculator
{
    internal class Program1
    {
        public static void MainEntry()
        {
            Console.Write("Please Enter the Equation : ");
            string equation = Console.ReadLine();

            int equationLength = equation.Length;
            int[] parserInput = new int[equationLength];
            int integer = 0;
            int counter = 0;
            bool hasnumber = false;
            int countOfOperators = 0;
            for (int index = 0; index < equationLength; index++)
            {
                var input = equation[index];
                if (input >= 48 && input <= 57)
                {
                    hasnumber = true;
                    if (integer == 0)
                    {
                        integer = input - 48;
                    }
                    else
                    {
                        integer *= 10;
                        integer += input - 48;
                    }

                    if (index < equationLength - 1)
                    {
                        if (equation[index + 1] < 48 || equation[index + 1] > 57)
                        {
                            parserInput[counter] = integer;
                            counter++;
                            integer = 0;
                        }
                    }
                    else
                    {
                        parserInput[counter] = integer;
                        counter++;
                        integer = 0;
                    }
                }
                else if ((input == 42 || input == 43 || input == 45 || input == 47 || input == 94 || input == 37) && hasnumber)
                {
                    if (countOfOperators >= 1)
                    {
                        if (countOfOperators >= 2)
                        {
                            return;
                        }
                        else if (input == 47)
                        {
                            hasnumber = false;
                            parserInput[counter] = input;
                            counter++;
                            countOfOperators++;
                        }
                        else
                        {
                            Console.WriteLine("Please Enter the correct Equation");
                            return;
                        }
                    }
                    else
                    {
                        countOfOperators++;
                        hasnumber = false;
                        parserInput[counter] = input;
                        counter++;
                    }
                }
                else if (!hasnumber)
                {
                    Console.WriteLine("Please Enter the correct equation");
                    return;
                }
            }

            if (countOfOperators == 0)
            {
                Console.WriteLine("You have Not entered any operator");
                return;
            }
            else if ((countOfOperators == 1 && parserInput.Length != 3) || (countOfOperators == 2 && parserInput.Length != 5))
            {
                return;
            }

            if (parserInput[1] == 42 || parserInput[1] == 43 || parserInput[1] == 45 || parserInput[1] == 47 || parserInput[1] == 37)
            {
                ArithemeticCalculations(parserInput);
            }
            else
            {
                PowerCalculations(parserInput, countOfOperators);
            }

        }

        static void ArithemeticCalculations(int[] parserInput)
        {
            int firstNumber = parserInput[0];
            int secondNumber = parserInput[2];

            CalculateFloat arithmeticCalculator = new CalculateFloat();
            float? result = null;
            switch (parserInput[1])
            {
                case 43:
                    result = arithmeticCalculator.Addition(firstNumber, secondNumber);
                    break;

                case 45:
                    result = arithmeticCalculator.Subtraction(firstNumber, secondNumber);
                    break;

                case 42:
                    result = arithmeticCalculator.Multiplication(firstNumber, secondNumber);
                    break;

                case 47:
                    result = arithmeticCalculator.Division(firstNumber, secondNumber);
                    break;

                case 37:
                    result = arithmeticCalculator.Modulus(firstNumber, secondNumber);
                    break;

                default:
                    Console.WriteLine("You have not Entered the correct Equation.");
                    break;
            }

            if (result.HasValue)
                Console.WriteLine("Result Of Calculation is : " + result);


        }

        static void PowerCalculations(int[] parserInput, int countOfOperators)
        {
            PowerCalculations powerCalculator = new PowerCalculations();

            double firstNumber = (double)parserInput[0];
            double secondNumber = (double)parserInput[2];
            double? result = null;

            if (parserInput[1] == 94)
            {
                if (countOfOperators == 1)
                {
                    if (parserInput[2] == 3)
                    {
                        result = powerCalculator.Cube(firstNumber);
                    }
                    else
                    {
                        result = powerCalculator.Power(firstNumber, secondNumber);
                    }
                }
                else if (countOfOperators == 2)
                {
                    double thirdNumber = parserInput[4];
                    if (secondNumber ==1 && parserInput[4] == 2)
                    {
                        result = powerCalculator.SquareRoot(firstNumber);
                    }
                    else if(secondNumber == 1 && parserInput[4] == 3)
                    {
                        result = powerCalculator.CubeRoot(firstNumber);
                    }
                    else if (secondNumber == 1)
                    {
                        result = powerCalculator.ExponentialByOne(firstNumber, thirdNumber);
                    }
                    else
                    {
                        Console.WriteLine("Please Enter Correct Equation");
                    }

                }
            }

            if (result.HasValue)
                Console.WriteLine("Result Of Calculation is : " + result);
        }
    }
}

using Arithmetic;
using Power;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace CalculatorEntry
{    
    internal class Entry 
    {
        static Stack operatorStack;
        static Stack operandStack;
        static CalculateFloat arithmeticCalculator;
        static PowerCalculations powerCalculator;

        public Entry()
        {

            arithmeticCalculator = new CalculateFloat();
            powerCalculator = new PowerCalculations();
        }
        ~Entry()
        {
            arithmeticCalculator = null;
            powerCalculator = null;
        }
        public void CalculatorWithStack()
        {
            operatorStack = new Stack();
            operandStack = new Stack();
            Console.Write("Please Enter the Equation : ");
            string equation = Console.ReadLine();

            int equationLength = equation.Length;
            int integer = 0;
            bool hasnumber = false;
            double result = 0;

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
                            operandStack.Push(input - 48);
                            integer = 0;
                        }
                    }
                    else
                    {
                        operandStack.Push(input - 48);
                        integer = 0;
                    }
                }
                else if ((input == 42 || input == 43 || input == 45 || input == 47 || input == 94 || input == 37) && hasnumber)
                {
                    if (input == 43 || input == 45)
                    {
                        operatorStack.Push(input);
                        hasnumber = false;
                    }
                    else if (input == 94)
                    {
                        double firstNumber;
                        double secondNumber;
                        try
                        {
                            firstNumber = Convert.ToDouble(operandStack.Pop());
                            secondNumber = Convert.ToDouble(equation[index + 1] - 48);
                            index++;
                            double doubleResult = powerCalculator.Power(firstNumber, secondNumber);
                            operandStack.Push(doubleResult);
                            hasnumber = true;
                        }
                        catch
                        {
                            return;
                        }
                        
                    }
                    else
                    {
                        int firstNumber;
                        int secondNumber;
                        try
                        {
                            firstNumber = Convert.ToInt32(operandStack.Pop());
                            secondNumber = equation[index + 1] - 48;
                            index++;
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            Console.WriteLine(e.Message);
                            return;
                        }

                        if (input == 42)
                        {
                            result = arithmeticCalculator.Multiplication(firstNumber, secondNumber);
                        }
                        else if (input == 47)
                        {
                            try
                            {
                                result = arithmeticCalculator.Division(firstNumber, secondNumber);
                            }
                            catch (DivideByZeroException e)
                            {
                                Console.WriteLine(e.Message);
                                return;
                            }
                        }
                        else if (input == 37)
                        {
                            result = arithmeticCalculator.Modulus(firstNumber, secondNumber);
                        }
                        operandStack.Push(result);
                        hasnumber = true;
                    }
                }
                else if (input == 40)
                {
                    try
                    {
                        double solution = CalculateParenthesis(ref index, equation);
                        if (solution >= 0)
                        {
                            operandStack.Push(solution);
                        }
                        else
                        {
                            return;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return;
                    }
                    hasnumber = true;
                }
                else if (!hasnumber && input != ' ')
                {
                    Console.WriteLine("Please Enter the correct equation");
                    return;
                }
                else if (input == '=')
                {
                    break;
                }
                else
                {
                    Console.WriteLine(input); return;
                }
            }

            result = 0;
            for (int index = 0; index < operatorStack.Count; index++)
            {
                char operators = Convert.ToChar(operatorStack.Pop());
                int secondNumber = Convert.ToInt32(operandStack.Pop());
                int firstNumber = Convert.ToInt32(operandStack.Pop());
                if (operators == '+')
                {
                    result = arithmeticCalculator.Addition(firstNumber, secondNumber);
                }
                else if (operators == '-')
                {
                    result = arithmeticCalculator.Subtraction(firstNumber, secondNumber);
                }
                operandStack.Push(result);
            }
            if (operandStack.Count == 1)
            {
                Console.WriteLine("Result of Equation is as : " + operandStack.Pop());
            }
            else
            {
                Console.WriteLine("No output");
            }
            operatorStack = null;
            operandStack = null;
        }



        public static double CalculateParenthesis(ref int i, string equation)
        {
            Stack operatorStackForParenthesis = new Stack();
            Stack operandStackForParenthesis = new Stack();
            i++;
            int integer = 0;
            int equationLength = equation.Length;
            bool hasnumber = true;
            double result = 0;
            for (int index = i; index <equationLength; index++)
            {
                if (equation[index] == ')')
                {
                    i = index +1;
                    break;
                }
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
                            operandStackForParenthesis.Push(input - 48);
                            integer = 0;
                        }
                    }
                    else
                    {
                        operandStackForParenthesis.Push(input - 48);
                        integer = 0;
                    }
                }
                else if ((input == 42 || input == 43 || input == 45 || input == 47 || input == 94 || input == 37) && hasnumber)
                {
                    if (input == 43 || input == 45)
                    {
                        operatorStackForParenthesis.Push(input);
                        hasnumber = false;
                    }
                    else if (input == 94)
                    {
                        double firstNumber;
                        double secondNumber;
                        try
                        {
                            firstNumber = Convert.ToDouble(operandStackForParenthesis.Pop());
                            secondNumber = Convert.ToDouble(equation[index + 1] - 48);
                            index++;
                            double doubleResult = powerCalculator.Power(firstNumber, secondNumber);
                            operandStackForParenthesis.Push(doubleResult);
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        int firstNumber;
                        int secondNumber;
                        try
                        {
                            firstNumber = Convert.ToInt32(operandStackForParenthesis.Pop());
                            secondNumber = equation[index + 1] - 48;
                            index++;
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            Console.WriteLine(e.Message);
                            return -1;
                        }

                        if (input == 42)
                        {
                            result = arithmeticCalculator.Multiplication(firstNumber, secondNumber);
                        }
                        else if (input == 47)
                        {
                            try
                            {
                                result = arithmeticCalculator.Division(firstNumber, secondNumber);
                            }
                            catch (DivideByZeroException e)
                            {
                                Console.WriteLine(e.Message);
                                return -1;
                            }
                        }
                        else if (input == 37)
                        {
                            result = arithmeticCalculator.Modulus(firstNumber, secondNumber);
                        }
                        operandStackForParenthesis.Push(result);
                        hasnumber = true;
                    }

                }
                else if (input == 40)
                {
                    try
                    {
                        double solution = CalculateParenthesis(ref index, equation);
                        if (solution >= 0)
                        {
                            operandStackForParenthesis.Push(solution);
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    hasnumber = false;
                }
                else if (!hasnumber && input != ' ')
                {
                    throw new Exception("You have not Entered the Right Equation");
                }
                else
                {
                    Console.WriteLine(input);
                    return -1;
                }
            }

            result = 0;
            for (int index = 0; index < operatorStackForParenthesis.Count; index++)
            {
                char operators = Convert.ToChar(operatorStackForParenthesis.Pop());
                int secondNumber = Convert.ToInt32(operandStackForParenthesis.Pop());
                int firstNumber = Convert.ToInt32(operandStackForParenthesis.Pop());
                if (operators == '+')
                {
                    result = arithmeticCalculator.Addition(firstNumber, secondNumber);
                }
                else if (operators == '-')
                {
                    result = arithmeticCalculator.Subtraction(firstNumber, secondNumber);
                }
                operandStackForParenthesis.Push(result);
            }

            if (operandStackForParenthesis.Count == 1)
            {
                double solution = Convert.ToDouble(operandStackForParenthesis.Pop());
                return solution;
            }
            else
            {
                return -1;
            }
        }
    }



    /* public static void MainEntry()
     {






         
     }


     public static bool CheckPrecision(int currentOperator, int? lastCheckedOperator)
     {
         if (lastCheckedOperator != null && (currentOperator == 42 || currentOperator == 47 || currentOperator == 37))
         {
             return true;
         }

         return false;
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
                 if (secondNumber == 1 && parserInput[4] == 2)
                 {
                     result = powerCalculator.SquareRoot(firstNumber);
                 }
                 else if (secondNumber == 1 && parserInput[4] == 3)
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
     }*/
}

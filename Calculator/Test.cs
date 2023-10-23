using Arithmetic;
using Power;
using System;

namespace CalculatorTest
{
    internal class TestCase
    {
        static CalculateFloat arithmeticCalculator;
        static PowerCalculations powerCalculator;

        public TestCase()
        {
            arithmeticCalculator = new CalculateFloat();
            powerCalculator = new PowerCalculations();
        }
        ~TestCase()
        {
            arithmeticCalculator = null;
            powerCalculator = null;
        }
        public void Calculator()
        {
            Console.Write("Please Enter the Equation : ");
            string equation = Console.ReadLine();

            int equationLength = equation.Length;
            int integer = 0;
            bool hasnumber = false;
            float result = 0;

            int positionForOperand = 0;
            int positionForOperator = 0;
            double[] operands = new double[equationLength];
            char[] operators = new char[equationLength];

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
                            Push(operands, ref positionForOperand, input-48);
                            integer = 0;
                        }
                    }
                    else
                    {
                        Push(operands, ref positionForOperand, input-48);
                        integer = 0;
                    }
                }
                else if ((input == 42 || input == 43 || input == 45 || input == 47 || input == 94 || input == 37) && hasnumber)
                {
                    if (input == 43 || input == 45)
                    {
                        Push(operators, ref positionForOperator, input);
                        hasnumber = false;
                    }
                    else if (input == 94)
                    {
                        double firstNumber;
                        double secondNumber;
                        try
                        {
                            firstNumber = Pop(operands, ref positionForOperand);
                            secondNumber = Convert.ToDouble(equation[index + 1] - 48);
                            index++;
                            double doubleResult = powerCalculator.PowerAll(firstNumber, secondNumber);
                            Push(operands,ref positionForOperand, doubleResult);
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
                            firstNumber = Convert.ToInt32(Pop(operands,ref positionForOperand));
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
                        Push(operands, ref positionForOperand, result);
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
                            Push(operands, ref positionForOperand, solution);
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
                    Console.WriteLine(input); 
                    return;
                }
            }

            result = 0;
            for (int index = 0; index <= positionForOperator; index++)
            {
                char op = Pop(operators, ref positionForOperator);
                int secondNumber = Convert.ToInt32(Pop(operands, ref positionForOperand));
                int firstNumber = Convert.ToInt32(Pop(operands, ref positionForOperand));
                if (op == '+')
                {
                    result = arithmeticCalculator.Addition(firstNumber, secondNumber);
                }
                else if (op == '-')
                {
                    result = arithmeticCalculator.Subtraction(firstNumber, secondNumber);
                }
                Push(operands, ref positionForOperand, result);
            }
            if (positionForOperand == 1)
            {
                Console.WriteLine("Result of Equation is as : " + Pop(operands,ref positionForOperand));
            }
            else
            {
                Console.WriteLine("No output");
            }
        }


        public static double CalculateParenthesis(ref int i, string equation)
        {
            int positionForOperand = 0;
            int positionForOperator = 0;
            int integer = 0;
            int equationLength = equation.Length;
            bool hasnumber = true;
            float result = 0;
            int index;
            i++;

            double[] operandForParenthesis = new double[equationLength];
            char[] operatorForParenthesis = new char[equationLength];

            for (index = i; index < equationLength; index++)
            {
                if (equation[index] == ')')
                {
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
                            Push(operandForParenthesis, ref positionForOperand, input - 48);
                            integer = 0;
                        }
                    }
                    else
                    {
                        Push(operandForParenthesis, ref positionForOperand, input - 48);
                        integer = 0;
                    }
                }
                else if ((input == 42 || input == 43 || input == 45 || input == 47 || input == 94 || input == 37) && hasnumber)
                {
                    if (input == 43 || input == 45)
                    {
                        Push(operatorForParenthesis, ref positionForOperator, input);
                        hasnumber = false;
                    }
                    else if (input == 94)
                    {
                        double firstNumber;
                        double secondNumber;
                        try
                        {
                            firstNumber = Pop(operandForParenthesis, ref positionForOperand);
                            secondNumber = Convert.ToDouble(equation[index + 1] - 48);
                            index++;
                            double doubleResult = powerCalculator.PowerAll(firstNumber, secondNumber);
                            Push(operandForParenthesis, ref positionForOperand, doubleResult);
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
                            firstNumber = Convert.ToInt32(Pop(operandForParenthesis, ref positionForOperand));
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
                        Push(operandForParenthesis, ref positionForOperand, result);
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
                            Push(operandForParenthesis, ref positionForOperand, solution);
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

            i = index + 1;
            result = 0;
            for (index = 0; index < positionForOperator; index++)
            {
                char operators = Pop(operatorForParenthesis, ref positionForOperator);
                int secondNumber = Convert.ToInt32(Pop(operandForParenthesis, ref positionForOperand));
                int firstNumber = Convert.ToInt32(Pop(operandForParenthesis, ref positionForOperand));
                if (operators == '+')
                {
                    result = arithmeticCalculator.Addition(firstNumber, secondNumber);
                }
                else if (operators == '-')
                {
                    result = arithmeticCalculator.Subtraction(firstNumber, secondNumber);
                }
                Push(operandForParenthesis, ref positionForOperand, result);
            }

            if (positionForOperand == 1)
            {
                double solution = Pop(operandForParenthesis, ref positionForOperand);
                return solution;
            }
            else
            {
                return -1;
            }
        }

        public static void Push<T>(T[] array, ref int position, T data)
        {
            array[position] = data;
            position++;            
        }

        public static T Pop<T>(T[] array, ref int position)
        {
            position--;        
            return array[position];
        }
    }
}

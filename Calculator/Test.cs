using Arithmetic;
using Power;
using System;
using System.Linq.Expressions;

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

        public void Calculate()
        {
            Console.Write("Please Enter the Equation : ");
            string equation = "";
            bool gettingInput = true;
            while (gettingInput)
            {
                char input = Console.ReadKey().KeyChar;
                if (input == '=' || input == 13)
                {
                    if(equation == "")
                    {
                        Console.WriteLine("Invalid Input");
                        return;
                    }
                    gettingInput = false;
                }
                else if(input == 8)
                {
                    Console.WriteLine("Backspace is not Available");
                    return;
                }
                else
                {
                    equation += input;
                }
            }
            Console.WriteLine("\v");
            int index = -1;
            double result;
            try
            {
                result = Calculator(ref index, equation, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            Console.WriteLine("Result of Equation is As : " + result);
        }

        public double Calculator(ref int i, string equation, bool isMain = false)
        {
            int equationLength = equation.Length;
            int positionForOperand = 0;
            int positionForOperator = 0;
            int index;
            double decimalIndex = 0;
            double integer = 0;
            double result;
            double[] operands = new double[equationLength];
            char[] operators = new char[equationLength];
            bool negativeSignedNumber = false;
            bool closingBracket = false;
            bool isDecimal = false;            
            bool hasnumber = false;
            i++;

            for (index = i; index < equationLength; index++)
            {
                var input = equation[index];
                if (input == '-' || input == '+')
                {
                    if (input == '-')
                    {
                        negativeSignedNumber = true;
                    }

                    if(index == i)
                    {
                        continue;
                    }                    
                }
                if (equation[index] == ')')
                {
                    if (!isMain)
                    {
                        closingBracket = true;
                        break;
                    }
                }

                if (input == '.')
                {
                    if (isDecimal)
                    {
                        throw new Exception("Invalid Input");
                    }
                    isDecimal = true;
                    decimalIndex = 0.1;
                }
                else if (input >= '0' && input <= '9')
                {
                    hasnumber = true;
                    if (integer == 0)
                    {
                        if (isDecimal)
                        {
                            integer = decimalIndex * (input - 48);
                            decimalIndex *= 0.1;
                        }
                        else
                        {
                            integer = input - 48;
                        }
                    }
                    else
                    {
                        if (isDecimal)
                        {
                            integer += decimalIndex * (input - 48);
                            decimalIndex *= 0.1;
                        }
                        else
                        {
                            integer *= 10;
                            integer += input - 48;
                        }

                    }

                    if (index < equationLength - 1)
                    {
                        if (!(equation[index + 1] >= '0' && equation[index + 1] <= '9') && equation[index + 1] != '.')
                        {
                            if (negativeSignedNumber)
                            {
                                integer = -integer;
                                negativeSignedNumber = false;
                            }
                            Push(operands, ref positionForOperand, integer);
                            integer = 0;
                            isDecimal = false;
                        }
                    }
                    else
                    {
                        if (negativeSignedNumber)
                        {
                            integer = -integer;
                            negativeSignedNumber = false;
                        }
                        Push(operands, ref positionForOperand, integer);
                        integer = 0;
                        isDecimal = false;
                    }
                }
                else if ((input == '+' || input == '-' || input == '*' || input == '/' || input == '%' || input == '^') && hasnumber && !isDecimal)
                {
                    if(input == '-')
                    {
                        Push(operators, ref positionForOperator, '+');
                    }
                    else
                    {
                        Push(operators, ref positionForOperator, input);                        
                    }
                    hasnumber = false;
                }
                else if (input == '(' && !isDecimal)
                {
                    try
                    {
                        if (index > 0 && equation[index - 1] >= '0' && equation[index - 1] <= '9')
                        {
                            Push(operators, ref positionForOperator, '*');
                        }
                        double solution = Calculator(ref index, equation);
                        Push(operands, ref positionForOperand, solution);
                        hasnumber = true;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }                    
                }
                else if (input == ' ')
                {
                    continue;
                }
                else
                {
                    throw new Exception("Invalid Input");
                }
            }

            if (!isMain && !closingBracket)
            {
                throw new Exception("Equation does not contains Closing Bracket");
            }
            i = index;
            char op;
            double[] operandsStackForPrecedence = new double[positionForOperand];
            char[] operatorStackForPrecedence = new char[positionForOperator];
            int newPositionForOperand = 0;
            int newPositionForOperator = 0;
            int precedence = 0;
            bool secondStackAccessed = false;
            for (index = positionForOperator; index >= newPositionForOperator || index >= positionForOperator; index--)
            {
                if (index == 0 && precedence < 2)
                {
                    precedence++;
                    secondStackAccessed = false;
                    if (precedence == 2)
                    {
                        index = positionForOperator;
                    }
                    else
                    {
                        index = newPositionForOperator;
                    }
                }
                if (positionForOperator == 0 && newPositionForOperator == 0)
                {
                    break;
                }

                double firstNumber, secondNumber;
                if (precedence == 0)
                {                   
                    if (!secondStackAccessed)
                    {
                        secondNumber = Pop(operands, ref positionForOperand);
                        secondStackAccessed = true;
                    }
                    else
                    {
                        secondNumber = Pop(operandsStackForPrecedence, ref newPositionForOperand);
                    }
                    op = Pop(operators, ref positionForOperator);
                    firstNumber = Pop(operands, ref positionForOperand);

                    if (op == '^')
                    {
                        if (secondNumber == (1.0 / 2.0))
                            result = Math.Round(powerCalculator.SquareRoot(firstNumber), 4);
                        else if (secondNumber == (1.0 / 3.0))
                            result = Math.Round(powerCalculator.CubeRoot(firstNumber), 4);
                        else if (secondNumber % 2 == 1 || secondNumber % 2 == 0)
                            result = powerCalculator.Power(firstNumber, secondNumber);
                        else
                            result = powerCalculator.ExponentialByOne(firstNumber, secondNumber);
                    }
                    else
                    {
                        Push(operatorStackForPrecedence, ref newPositionForOperator, op);
                        Push(operandsStackForPrecedence, ref newPositionForOperand, secondNumber);
                        Push(operandsStackForPrecedence, ref newPositionForOperand, firstNumber);
                        continue;
                    }
                    Push(operandsStackForPrecedence, ref newPositionForOperand, result);
                }
                if (precedence == 1)
                {                    
                    if (!secondStackAccessed)
                    {
                        firstNumber = Pop(operandsStackForPrecedence, ref newPositionForOperand);                        
                        secondStackAccessed = true;
                    }
                    else
                    {
                        firstNumber = Pop(operands, ref positionForOperand);
                    }

                    op = Pop(operatorStackForPrecedence, ref newPositionForOperator);
                    secondNumber = Pop(operandsStackForPrecedence, ref newPositionForOperand);

                    if (op == '*')
                    {
                        result = arithmeticCalculator.Multiplication(firstNumber, secondNumber);
                    }
                    else if (op == '/')
                    {
                        result = arithmeticCalculator.Division(firstNumber, secondNumber);
                    }
                    else if (op == '%')
                    {
                        result = arithmeticCalculator.Modulus(firstNumber, secondNumber);
                    }
                    else
                    {
                        Push(operators, ref positionForOperator, op);
                        Push(operands, ref positionForOperand, firstNumber);
                        Push(operands, ref positionForOperand, secondNumber);
                        continue;
                    }
                    Push(operands, ref positionForOperand, result);
                }
                else if (precedence == 2)
                {
                    if (!secondStackAccessed)
                    {
                        secondNumber = Pop(operands, ref positionForOperand);
                        secondStackAccessed = true;
                    }
                    else
                    {
                        secondNumber = Pop(operandsStackForPrecedence, ref newPositionForOperand);                        
                    }
                    op = Pop(operators, ref positionForOperator);
                    firstNumber = Pop(operands, ref positionForOperand);

                    if (op == '+')
                    {
                        result = arithmeticCalculator.Addition(firstNumber, secondNumber);
                    }
                    else
                    {
                        Push(operatorStackForPrecedence, ref newPositionForOperator, op);
                        Push(operandsStackForPrecedence, ref newPositionForOperand, secondNumber);
                        Push(operandsStackForPrecedence, ref newPositionForOperand, firstNumber);
                        continue;
                    }
                    Push(operandsStackForPrecedence, ref newPositionForOperand, result);
                }
            }

            if (newPositionForOperand == 1)
            {
                double solution = Pop(operandsStackForPrecedence, ref newPositionForOperand);
                return solution;
            }
            else if (positionForOperand == 1)
            {
                double solution = Pop(operands, ref positionForOperand);
                return solution;
            }
            else
            {
                throw new Exception("Invalid Input");
            }
        }

        /// <summary>
        /// Stack Method to push value at top of array and incremnt the pointer value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="position"></param>
        /// <param name="data"></param>
        public static void Push<T>(T[] array, ref int position, T data)
        {
            Console.WriteLine("Pushed : " + data);
            array[position] = data;
            position++;
        }

        /// <summary>
        /// Stack Method to pop value from top of array and decrement the pointer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static T Pop<T>(T[] array, ref int position)
        {
            position--;
            Console.WriteLine($"Popped value : {array[position]}");
            return array[position];
        }
    }
}
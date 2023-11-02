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

        public void Calculate()
        {
            Console.Write("Please Enter the Equation : ");
            string equation = Console.ReadLine();
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
            double integer = 0;
            bool hasnumber = false;
            double result = 0;
            double decimalIndex = 0;
            i++;
            int index;
            bool closingBracket = false;
            bool isDecimal = false;
            int positionForOperand = 0;
            int positionForOperator = 0;
            double[] operands = new double[equationLength];
            char[] operators = new char[equationLength];
            bool negativeSignedNumber = false;

            for (index = i; index < equationLength; index++)
            {
                closingBracket = false;
                var input = equation[index];
                if (index == 0 && (input == '-' || input == '+'))
                {
                    if (input == '-')
                    {
                        negativeSignedNumber = true;
                    }
                    continue;
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
                else if (input >= '0' && input <= '9' || input == '.')
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
                        if (!(equation[index + 1] >= 48 || equation[index + 1] <= 57) || equation[index + 1] != '.')
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
                else if ((input == 42 || input == 43 || input == 45 || input == 47 || input == 94 || input == 37) && hasnumber && !isDecimal)
                {
                    Push(operators, ref positionForOperator, input);
                    hasnumber = false;
                }
                else if (input == '(')
                {
                    try
                    {
                        if (index > 0 && equation[index - 1] >= '0' && equation[index - 1] <= '9')
                        {
                            Push(operators, ref positionForOperator, '*');
                        }
                        double solution = Calculator(ref index, equation);
                        Push(operands, ref positionForOperand, (double)solution);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    hasnumber = true;
                }
                else if (input == ' ')
                {
                    continue;
                }
                else if (input == '=' && isMain)
                {
                    break;
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
            bool valueInsertedInSecondStack = false;
            for (index = positionForOperator; index >= newPositionForOperator || index >= positionForOperator; index--)
            {
                if (index == 0 && precedence < 2)
                {
                    precedence++;
                    index = newPositionForOperator;
                }
                if (positionForOperator == 0 && newPositionForOperator == 0)
                {
                    break;
                }

                double firstNumber, secondNumber;

                if (precedence == 0)
                {
                    if (!valueInsertedInSecondStack)
                    {
                        secondNumber = Pop(operands, ref positionForOperand);
                        valueInsertedInSecondStack = true;
                    }
                    else
                    {
                        secondNumber = Pop(operandsStackForPrecedence, ref newPositionForOperand);
                    }
                    op = Pop(operators, ref positionForOperator);
                    firstNumber = Pop(operands, ref positionForOperand);

                    if (op == '^')
                    {
                        if (secondNumber == (1 / 2))
                            result = powerCalculator.SquareRoot(firstNumber);
                        else if (secondNumber == (1 / 3))
                            result = powerCalculator.CubeRoot(firstNumber);
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
                }
                if (precedence == 1)
                {
                    op = Pop(operatorStackForPrecedence, ref newPositionForOperator);
                    firstNumber = Pop(operandsStackForPrecedence, ref newPositionForOperand);
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
                        Push(operatorStackForPrecedence, ref newPositionForOperator, op);
                        Push(operandsStackForPrecedence, ref newPositionForOperand, firstNumber);
                        Push(operandsStackForPrecedence, ref newPositionForOperand, secondNumber);
                        continue;
                    }
                }
                else if (precedence == 2)
                {
                    op = Pop(operatorStackForPrecedence, ref newPositionForOperator);
                    firstNumber = Pop(operandsStackForPrecedence, ref newPositionForOperand);
                    secondNumber = Pop(operandsStackForPrecedence, ref newPositionForOperand);

                    if (op == '+')
                    {
                        result = arithmeticCalculator.Addition(firstNumber, secondNumber);
                    }
                    else if (op == '-')
                    {
                        result = arithmeticCalculator.Subtraction(firstNumber, secondNumber);
                    }
                    else
                    {
                        Push(operatorStackForPrecedence, ref newPositionForOperator, op);
                        Push(operandsStackForPrecedence, ref newPositionForOperand, firstNumber);
                        Push(operandsStackForPrecedence, ref newPositionForOperand, secondNumber);
                        continue;
                    }
                }
                Push(operandsStackForPrecedence, ref newPositionForOperand, result);
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
            //Console.WriteLine("Pushed : " + data);
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
            //Console.WriteLine($"Popped value : {array[position]}");
            return array[position];
        }
    }
}




/*
 result = 0;
            char op;
            int precedence = 0;
            int indexOfFirstNum = 0;
            int indexOfSecondNum = 1;
            for (index = 0; index <= positionForOperator; index++)
            {
                if (positionForOperator == 0)
                {
                    break;
                }
                /               op = Pop(operators, ref positionForOperator);             
                                double secondNumber = Pop(operands, ref positionForOperand);
                                double firstNumber = Pop(operands, ref positionForOperand);
op = operators[index];
double firstNumber = operands[indexOfFirstNum];
double secondNumber = operands[indexOfSecondNum];

if (precedence == 0)
{
    if (op == '*')
    {
        result = arithmeticCalculator.Multiplication(firstNumber, secondNumber);
    }
    else if (op == '/')
    {
        result = arithmeticCalculator.Division(firstNumber, secondNumber);
    }
    else if (op == '^')
    {
        if (secondNumber == (1 / 2))
            result = powerCalculator.SquareRoot(firstNumber);
        else if (secondNumber == (1 / 3))
            result = powerCalculator.CubeRoot(firstNumber);
        else if (secondNumber < 1)
            result = powerCalculator.ExponentialByOne(firstNumber, secondNumber);
        else
            result = powerCalculator.Power(firstNumber, secondNumber);
    }
    else
    {
        continue;
    }
}
else if (precedence == 1)
{
    if (op == '+')
    {
        result = arithmeticCalculator.Addition(firstNumber, secondNumber);
    }
    else if (op == '-')
    {
        result = arithmeticCalculator.Subtraction(firstNumber, secondNumber);
    }
    else
    {
        continue;
    }
}
if (result != 0)
{
    Pop(operators, ref index);
    Pop(operands, ref indexOfFirstNum);
    Pop(operands, ref indexOfSecondNum);
    Push(operands, ref positionForOperand, result);
}

result = 0;
indexOfFirstNum++;
indexOfSecondNum++;
if (index == positionForOperator && precedence < 1)
{
    precedence++;
    index = -1;
    indexOfFirstNum = 0;
    indexOfSecondNum = 1;
}
*/


/*
         public static double? CalculateParenthesis(ref int i, string equation)
        {
            int positionForOperand = 0;
            int positionForOperator = 0;
            int integer = 0;
            int equationLength = equation.Length;
            bool hasnumber = true;

            double result = 0;
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
                            Push(operandForParenthesis, ref positionForOperand, integer);
                            integer = 0;
                        }
                    }
                    else
                    {
                        Push(operandForParenthesis, ref positionForOperand, integer);
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
                            if (equation[index + 1] == 40)
                            {
                                Push(operatorForParenthesis, ref positionForOperator, input);
                                continue;
                            }
                            firstNumber = Pop(operandForParenthesis, ref positionForOperand);
                            secondNumber = equation[index + 1] - 48;
                            index++;
                            double doubleResult = powerCalculator.Power(firstNumber, secondNumber);
                            Push(operandForParenthesis, ref positionForOperand, doubleResult);
                        }
                        catch
                        {
                            return null;
                        }
                    }
                    else
                    {
                        double firstNumber, secondNumber;
                        try
                        {
                            if (equation[index + 1] == 40)
                            {
                                Push(operatorForParenthesis, ref positionForOperator, input);
                                continue;
                            }
                            firstNumber = Pop(operandForParenthesis, ref positionForOperand);
                            secondNumber = equation[index + 1] - 48;
                            index++;
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            throw e;
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
                                throw e;
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
                        double? solution = CalculateParenthesis(ref index, equation);
                        if (solution.HasValue)
                        {
                            Push(operandForParenthesis, ref positionForOperand, (double)solution);
                        }
                        else if(solution == null)
                        {
                            return null;
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
                    return null;
                }
            }
            if(equation[index] != ')')
            {
                throw new Exception("Equation does not contains Closing Bracket");
            }
            i = index + 1;

            result = 0;
            for (index = 0; index <= positionForOperator; index++)
            {
                if (positionForOperator == 0)
                {
                    break;
                }
                char operators = Pop(operatorForParenthesis, ref positionForOperator);
                double secondNumber = Pop(operandForParenthesis, ref positionForOperand);
                double firstNumber = Pop(operandForParenthesis, ref positionForOperand);
                if (operators == '+')
                {
                    result = arithmeticCalculator.Addition(firstNumber, secondNumber);
                }
                else if (operators == '-')
                {
                    result = arithmeticCalculator.Subtraction(firstNumber, secondNumber);
                }
                else if (operators == '*')
                {
                    result = arithmeticCalculator.Multiplication(firstNumber, secondNumber);
                }
                else if( operators == '/')
                {
                    result = arithmeticCalculator.Division(firstNumber, secondNumber);
                }
                else if(operators == '^')
                {
                    if (secondNumber == (1 / 2))
                        result = powerCalculator.SquareRoot(firstNumber);
                    else if (secondNumber == (1 / 3))
                        result = powerCalculator.CubeRoot(firstNumber);
                    else if (secondNumber < 1)
                        result = powerCalculator.ExponentialByOne(firstNumber, secondNumber);
                    else
                        result = powerCalculator.Power(firstNumber, secondNumber);
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
                return null;
            }
        }
 */
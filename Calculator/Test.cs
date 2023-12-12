using Arithmetic;
using Power;
using System;
using StackOperations;
using System.Text;

namespace CalculatorTest
{
    internal class TestCase : Stack
    {
        static CalculateFloat arithmeticCalculator;
        static PowerCalculations powerCalculator;
        static double memory;
        static StringBuilder outputThirdLine;
        public TestCase()
        {
            arithmeticCalculator = new CalculateFloat();
            powerCalculator = new PowerCalculations();
            outputThirdLine = new StringBuilder("DEG            E-F         ");
            memory = 0;
        }
        ~TestCase()
        {
            arithmeticCalculator = null;
            powerCalculator = null;
        }
        public void Calculate()
        {
            double? result = null;
            Console.Title = "Scientific Calculator";
            Console.CursorVisible = false;
            while (true)
            {
                int index = -1;
                try
                {
                    Input(out string equation, result);
                    result = Calculator(ref index, equation, true);
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                    continue;
                }
                Console.WriteLine(PrintResult(result.Value));
            }
        }

        static void Input(out string equation, double? result)
        {
            equation = "";
            bool gettingInput = true;
            bool decimalCounted = false;
            Console.Write(0);
            Console.SetCursorPosition(0, 2);
            Console.WriteLine(outputThirdLine);
            Console.SetCursorPosition(0, 17);
            Console.WriteLine("Press 'Ctrl + H' For Help.");
            Console.SetCursorPosition(0, 0);
            while (gettingInput)
            {
                ConsoleKeyInfo cki = Console.ReadKey();
                if ((cki.Modifiers & ConsoleModifiers.Control) != 0)
                {
                    if (cki.Key == ConsoleKey.M)
                    {
                        string eq = equation;
                        try
                        {
                            memory = GetLastNum(ref eq);
                        }
                        catch
                        {
                            memory = result.Value;
                        }

                        if (!(outputThirdLine.ToString()).Contains("M"))
                        {
                            outputThirdLine.Append("M");
                        }
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.L)
                    {
                        memory = 0;
                        outputThirdLine.Replace(" M", " ");
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.P)
                    {
                        string eq = equation;
                        memory += GetLastNum(ref eq);
                    }
                    else if (cki.Key == ConsoleKey.Q)
                    {
                        string eq = equation;
                        memory -= GetLastNum(ref eq);
                    }
                    else if (cki.Key == ConsoleKey.R)
                    {
                        if ((outputThirdLine.ToString()).Contains("M"))
                        {
                            equation += memory;
                            ClearInput(equation);
                        }
                    }
                    else if (cki.Key == ConsoleKey.G)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += powerCalculator.Power(10, lastNum);
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.S)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Sinh(GetAngle(lastNum));
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.T)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Tanh(GetAngle(lastNum));
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.O)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Cosh(GetAngle(lastNum));
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.U)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Cosh(1 / GetAngle(lastNum));
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.I)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Sinh(1 / GetAngle(lastNum));
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.J)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Tanh(1 / GetAngle(lastNum));
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.H)
                    {
                        PrintHelp();
                        ConsoleKeyInfo ckiH = Console.ReadKey();
                        ClearInput(equation);
                    }
                }
                else if ((cki.Modifiers & ConsoleModifiers.Shift) != 0)
                {
                    if (cki.Key == ConsoleKey.D1 || cki.Key == ConsoleKey.D5 || cki.Key == ConsoleKey.D6 || cki.Key == ConsoleKey.D8 || cki.Key == ConsoleKey.D9 || cki.Key == ConsoleKey.D0 || cki.Key == ConsoleKey.OemPlus)
                    {
                        if (equation == "")
                        {
                            if (result == null && !(cki.KeyChar == '+' || cki.KeyChar == '-'))
                            {
                                throw new Exception("");
                            }
                            equation += result;
                        }
                        equation += cki.KeyChar;
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.D3)
                    {
                        equation += "^3";
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.D2)
                    {
                        equation += "^0.5";
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.S)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Asin(GetAngle(lastNum));
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.T)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Atan(GetAngle(lastNum));
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.O)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Acos(GetAngle(lastNum));
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.U)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Acos(1 / GetAngle(lastNum));
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.I)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Asin(1 / GetAngle(lastNum));
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.J)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Atan(1 / GetAngle(lastNum));
                        ClearInput(equation);
                    }
                    else if (cki.KeyChar == '|')
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Abs(lastNum);
                        ClearInput(equation);
                    }
                }
                else if (cki.Key == ConsoleKey.Escape)
                {
                    equation = "";
                    ClearInput(equation);
                }
                else if (cki.Key == ConsoleKey.Y)
                {
                    equation += "^";
                    ClearInput(equation);
                }
                else if (cki.Key == ConsoleKey.Q)
                {
                    equation += "^2";
                    ClearInput(equation);
                }
                else if (cki.Key == ConsoleKey.P)
                {
                    equation += Math.PI;
                    ClearInput(equation);
                }
                else if (cki.Key == ConsoleKey.R)
                {
                    double lastValue = GetLastNum(ref equation);
                    equation += "1/" + lastValue;
                    ClearInput(equation);
                }
                else if (cki.Key == ConsoleKey.F9)
                {
                    double lastNum = GetLastNum(ref equation);
                    if (equation.Length > 0)
                    {
                        equation = equation.Remove(equation.Length - 1);
                    }
                    lastNum *= -1;
                    if (lastNum >= 0)
                    {
                        equation += "+";
                    }
                    equation += lastNum;
                    ClearInput(equation);
                }
                else if (cki.Key == ConsoleKey.G)
                {
                    double lastNum = GetLastNum(ref equation);
                    equation += powerCalculator.Power(2, lastNum);
                    ClearInput(equation);
                }
                else if (cki.Key == ConsoleKey.L)
                {
                    double lastNum = GetLastNum(ref equation);
                    equation += Math.Log(lastNum);
                    ClearInput(equation);
                }
                else if (cki.Key == ConsoleKey.S)
                {
                    double lastNum = GetLastNum(ref equation);
                    equation += Math.Sin(GetAngle(lastNum));
                    ClearInput(equation);

                }
                else if (cki.Key == ConsoleKey.T)
                {
                    double lastNum = GetLastNum(ref equation);
                    equation += Math.Tan(GetAngle(lastNum));
                    ClearInput(equation);
                }
                else if (cki.Key == ConsoleKey.O)
                {
                    double lastNum = GetLastNum(ref equation);
                    equation += Math.Cos(GetAngle(lastNum));
                    ClearInput(equation);
                }
                else if (cki.Key == ConsoleKey.U)
                {
                    double lastNum = GetLastNum(ref equation);
                    equation += Math.Cos(1 / GetAngle(lastNum));
                    ClearInput(equation);
                }
                else if (cki.Key == ConsoleKey.I)
                {
                    double lastNum = GetLastNum(ref equation);
                    equation += Math.Sin(1 / GetAngle(lastNum));
                    ClearInput(equation);
                }
                else if (cki.Key == ConsoleKey.J)
                {
                    double lastNum = GetLastNum(ref equation);
                    equation += Math.Tan(1 / GetAngle(lastNum));
                    ClearInput(equation);
                }
                else if (cki.Key == ConsoleKey.F4)
                {
                    if ((outputThirdLine.ToString()).Contains("DEG"))
                    {
                        outputThirdLine.Replace("DEG", "RAD");
                    }
                    else if ((outputThirdLine.ToString()).Contains("GRAD"))
                    {
                        outputThirdLine.Replace("GRAD", "RAD");
                    }
                    ClearInput(equation);
                }
                else if (cki.Key == ConsoleKey.F5)
                {
                    if ((outputThirdLine.ToString()).Contains("DEG"))
                    {
                        outputThirdLine.Replace("DEG", "GRAD");
                    }
                    else if ((outputThirdLine.ToString()).Contains("RAD"))
                    {
                        outputThirdLine.Replace("RAD", "GRAD");
                    }
                    ClearInput(equation);
                }
                else if (cki.Key == ConsoleKey.F3)
                {
                    if ((outputThirdLine.ToString()).Contains("RAD"))
                    {
                        outputThirdLine.Replace("RAD", "DEG");
                    }
                    else if ((outputThirdLine.ToString()).Contains("GRAD"))
                    {
                        outputThirdLine.Replace("GRAD", "DEG");
                    }
                    ClearInput(equation);
                }
                else if (cki.Key == ConsoleKey.E)
                {
                    equation += Math.E;
                    ClearInput(equation);
                }
                else if (cki.Key == ConsoleKey.N)
                {
                    double lastNum = GetLastNum(ref equation);
                    equation += Math.Log(lastNum, Math.E);
                    ClearInput(equation);
                }
                else if (cki.Key == ConsoleKey.V)
                {
                    if ((outputThirdLine.ToString()).Contains("F-E"))
                    {
                        outputThirdLine.Replace("F-E", "E-F");                        
                    }
                    else
                    {
                        outputThirdLine.Replace("E-F", "F-E");
                    }
                    ClearInput(equation);
                    if (equation =="" && result.HasValue)
                    {
                        Console.SetCursorPosition(0, 1);
                        Console.Write(PrintResult(result.Value));
                        Console.SetCursorPosition(equation.Length, 0);
                    }
                }
                else if (cki.KeyChar == '[')
                {
                    double lastNum = GetLastNum(ref equation);
                    equation += Math.Floor(lastNum);
                    ClearInput(equation);
                }
                else if (cki.KeyChar == ']')
                {
                    double lastNum = GetLastNum(ref equation);
                    equation += Math.Ceiling(lastNum);
                    ClearInput(equation);
                }
                else
                {
                    char input = cki.KeyChar;
                    if (equation == "")
                    {
                        ClearInput(input.ToString());
                    }
                    if (input == '=' || input == 13)
                    {
                        if (equation == "")
                        {
                            throw new Exception("");
                        }
                        else
                        {
                            if (input == 13)
                            {
                                ClearInput(equation + "=");
                            }
                            gettingInput = false;
                        }
                    }
                    else if (input == 8 || cki.Key == ConsoleKey.Delete)
                    {
                        equation = equation.Remove(equation.Length - 1);
                        ClearInput(equation);
                    }
                    else if (input == ' ' || (input == '.' && decimalCounted))
                    {
                        continue;
                    }
                    else
                    {
                        if (input == '+' || input == '-' || input == '*' || input == '/' || input == '%' || input == '^')
                        {
                            if (equation == "")
                            {
                                if (result == null && !(input == '+' || input == '-'))
                                {
                                    throw new Exception("");
                                }
                                equation += result;
                                ClearInput(result + "" + input);
                            }
                            decimalCounted = false;
                        }
                        else if (input == '.')
                        {
                            decimalCounted = true;
                        }

                        equation += input;
                    }
                }
            }
            Console.WriteLine();
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
                    if (index == i)
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
                else if (input == '!')
                {
                    double val = Pop(operands, ref positionForOperand);
                    result = powerCalculator.Factorial(val);
                    Push(operands, ref positionForOperand, result);
                }
                else if ((input == '+' || input == '-' || input == '*' || input == '/' || input == '%' || input == '^') && hasnumber && !isDecimal)
                {
                    if (input == '-')
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
                        if (solution < 0 && negativeSignedNumber)
                        {
                            solution *= -1;
                        }
                        Push(operands, ref positionForOperand, solution);
                        hasnumber = true;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
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

            double[] operandsStackForPrecedence = new double[positionForOperand];
            char[] operatorStackForPrecedence = new char[positionForOperator];
            double firstNumber, secondNumber;
            bool secondStackAccessed = false;
            int newPositionForOperator = 0;
            int newPositionForOperand = 0;
            int precedence = 0;
            i = index;
            char op;

            for (index = positionForOperator; index >= newPositionForOperator || index >= positionForOperator; index--)
            {
                if (index == 0 && precedence < 2)
                {
                    precedence++;
                    secondStackAccessed = false;
                    if (precedence % 2 == 0)
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
                        Push(operandsStackForPrecedence, ref newPositionForOperand, result);
                    }
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

        public static void ClearInput(string equation)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            if(equation == "")
            {
                Console.Write(0);
                Console.SetCursorPosition(0, 0);
            }
            else
            {
                Console.Write(equation);
            }
            Console.SetCursorPosition(0, 2);
            Console.WriteLine(outputThirdLine);
            Console.SetCursorPosition(0, 17);
            Console.WriteLine("Press 'Ctrl + H' For Help.");
            Console.SetCursorPosition(equation.Length, 0);
        }

        public static double GetLastNum(ref string equation)
        {
            if (equation.Length == 0 || (equation[equation.Length - 1] < '0' || equation[equation.Length - 1] > '9'))
            {
                throw new Exception("");
            }
            int i;
            string sub = "";
            for (i = equation.Length - 1; i >= 0; i--)
            {
                if (equation[i] < '0' || equation[i] > '9')
                {
                    if (equation[i] == '-')
                    {
                        sub += "-";
                    }
                    else if (equation[i] == '.')
                    {
                        continue;
                    }
                    break;
                }
            }
            sub += equation.Substring(i + 1);
            equation = equation.Remove(i + 1);

            return Convert.ToDouble(sub);
        }

        public static double GetAngle(double num)
        {
            if ((outputThirdLine.ToString()).Contains("DEG"))
            {
                return (num * (Math.PI / 180));
            }
            else if ((outputThirdLine.ToString()).Contains("RAD"))
            {
                return num;
            }
            else
            {
                return (num * (200 / 180));
            }
        }

        public static void PrintHelp()
        {
            string printLine = @"
CTRL + M    - Store in memory, in Standard mode, Scientific mode
CTRL + P    - Add to memory, in Standard mode, Scientific mode
CTRL + Q    - Subtract from memory, in Standard mode, Scientific mode
CTRL + R    - Recall from memory, in Standard mode, Scientific mode
CTRL + L    - Clear memory
DELETE      - Clear current input (select CE)
ESC         - Fully clear input (select C)
ENTER       - Selects = in Standard mode, Scientific mode
F9          - Select +/- in Standard mode, Scientific mode,
R           - Select 1/x in Standard mode and Scientific mode
@           - Select 2√x in Standard mode and Scientific mode
F3          - Select DEG in Scientific mode
F4          - Select RAD in Scientific mode
F5          - Select GRAD in Scientific mode
G           - Select 2^x in Scientific mode
CTRL + G    - Select 10xin Scientific mode
S           - Select 10xin in Scientific mode
SHIFT + S   - Select sin-1 in Scientific mode
CTRL + S    - Select sinh in Scientific mode
T           - Select tan in Scientific mode
SHIFT + T   - Select tan-1 in Scientific mode
CTRL + T    - Select tanh in Scientific mode
O           - Select cos in Scientific mode
SHIFT + O   - Select cos-1 in Scientific mode
CTRL + O    - Select cosh in Scientific mode
U           - Select sec in Scientific mode
SHIFT + U   - Select sec-1 in Scientific mode
CTRL + U    - Select sech in Scientific mode 
I           - Select csc in Scientific mode 
SHIFT + I   - Select csc-1 in Scientific mode
CTRL + I    - Select csch in Scientific mode
J           - Select cot in Scientific mode 
SHIFT + J   - Select cot-1 in Scientific mode
CTRL + J    - Select coth in Scientific mode 
SHIFT + \   - Select |x| in Scientific mode
[           - Select &lfloor;x&rfloor; in Scientific mode 
]           - Select &lceil;x&rceil; in Scientific mode
L           - Select log in Scientific mode 
P           - Select Pi in Scientific mode
Q           - Select x^2 in Standard mode and Scientific mode
Y, ^        - Select x^y in Scientific mode
#           - Select x^3 in Scientific mode 
!           - Select n! in Scientific mode
%           - Select mod in Scientific mode
E           - Select Value Of E












Press Any Key To return Back to Calculator Window
                ";
            Console.WriteLine(printLine);

        }

        public static string PrintResult(double result)
        {
            if ((outputThirdLine.ToString()).Contains("F-E"))
            {
                string resultInString = result.ToString();
                int i;
                int count = 0;
                for (i = resultInString.Length - 1; i >= 0; i--)
                {
                    if (resultInString[i] != '0')
                    {
                        break;
                    }
                    count++;
                }
                return String.Format(resultInString.Remove(i + 1) + "E+" + count);
            }
            else
            {
                return result.ToString();
            }
        }
    }
}
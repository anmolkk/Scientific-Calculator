using Arithmetic;
using Power;
using System;
using StackOperations;
using System.Text;
using System.Linq;

namespace CalculatorTest
{
    internal class TestCase : Stack
    {
        static CalculateFloat arithmeticCalculator;
        static PowerCalculations powerCalculator;
        static StringBuilder outputThirdLine;
        static StringBuilder outputSecondline;
        static StringBuilder outputFirstLine;
        static short historyPointer;
        static string[] history;
        static double memory;
        public TestCase()
        {
            arithmeticCalculator = new CalculateFloat();
            powerCalculator = new PowerCalculations();
            outputThirdLine = new StringBuilder("DEG            E-F         ");
            outputSecondline = new StringBuilder("");
            outputFirstLine = new StringBuilder("");
            history = new string[short.MaxValue - 1];
            historyPointer = 0;
            memory = 0;
        }
        ~TestCase()
        {
            arithmeticCalculator = null;
            powerCalculator = null;
        }
        public void Calculate()
        {
            string result = null;
            Console.Title = "Scientific Calculator";
            Console.CursorVisible = false;
            Console.BufferWidth = Console.LargestWindowWidth;
            while (true)
            {
                int index = -1;
                try
                {
                    Input(out string equation, result);
                    result = Calculator(ref index, equation, true).ToString();
                    outputSecondline.Clear();
                    ClearInput(equation);
                    //outputFirstLine.Replace(outputFirstLine.ToString(), "0");
                }
                catch (Exception e)
                {
                    Console.Clear();
                    outputSecondline.Clear();
                    Console.WriteLine(e.Message);
                    continue;
                }
                Console.SetCursorPosition(0, 1);
                Console.Write(PrintResult(result));
            }
        }

        static void Input(out string equation, string result)
        {
            equation = "";
            int index = -1;
            bool gettingInput = true;
            bool decimalCounted = false;
            bool lastOperator = false;
            bool MathFunctionCalled = false;
            outputFirstLine.Clear();
            outputSecondline.Clear();
            Console.SetCursorPosition(0, 2);
            Console.WriteLine(outputThirdLine);
            Console.SetCursorPosition(0, 17);
            Console.WriteLine("Press 'Ctrl + H' For Help.");
            Console.SetCursorPosition(0, 0);
            while (gettingInput)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
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
                            memory = Convert.ToDouble(result);
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
                            outputSecondline.Replace(outputSecondline.ToString(), memory.ToString());
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
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Sinh(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("Sinh(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
                        equation += Math.Sinh(lastNum);
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.T)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Tanh(lastNum);
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Tanh(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("Tanh(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.O)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Cosh(lastNum);
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Cosh(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("Cosh(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.U)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Cosh(1 / lastNum);
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Sech(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("Sech(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.I)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Sinh(1 / lastNum);
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Csch(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("Csch(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.J)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Tanh(1 / lastNum);
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Coth(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("Coth(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.H)
                    {
                        PrintHelp();
                        ConsoleKeyInfo ckiH = Console.ReadKey();
                        ClearInput(equation);
                    }

                    else if (cki.Key == ConsoleKey.N)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Exp(lastNum);
                        outputFirstLine.Append(Math.Exp(lastNum));
                        MathFunctionCalled = true;
                        outputSecondline.Clear();
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
                        else if (cki.KeyChar == '+' || cki.KeyChar == '-')
                        {
                            if (equation != "" && !equation.Contains('(') && !equation.Contains(')') && !lastOperator)
                            {
                                index = -1;
                                outputSecondline.Replace(outputSecondline.ToString(), Calculator(ref index, equation, true).ToString());
                            }
                        }
                        else if (cki.KeyChar == '*' || cki.KeyChar == '^' || cki.KeyChar == '%')
                        {
                            outputFirstLine.Append(outputSecondline.ToString() + cki.KeyChar);
                            outputSecondline.Clear();
                        }
                        else
                        {

                        }
                        equation += cki.KeyChar;
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.D3)
                    {
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Cube(" + GetLastValue() + ")");
                        }
                        else
                        {
                            string eq = equation;
                            outputFirstLine.Append("Cube(" + GetLastNum(ref eq) + ")");
                            MathFunctionCalled = true;
                        }
                        equation += "^3";
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.D2)
                    {
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Sqrt(" + GetLastValue() + ")");
                        }
                        else
                        {
                            string eq = equation;
                            outputFirstLine.Append("Sqrt(" + GetLastNum(ref eq) + ")");
                            MathFunctionCalled = true;
                        }
                        equation += "^0.5";
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.C)
                    {
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("cuberoot(" + GetLastValue() + ")");
                        }
                        else
                        {
                            string eq = equation;
                            outputFirstLine.Append("cuberoot(" + GetLastNum(ref eq) + ")");
                            MathFunctionCalled = true;
                        }
                        equation += "^0.3333333333";
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.S)
                    {
                        double lastNum = GetLastNum(ref equation);
                        if(lastNum < -1 || lastNum > 1)
                        {
                            throw new Exception("Invalid Input");
                        }
                        double angle = Math.Asin(GetAngle(lastNum));
                        equation += angle * (180/Math.PI);
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Asin(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("Asin(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.T)
                    {
                        double lastNum = GetLastNum(ref equation);
                        if (lastNum < -1 || lastNum > 1)
                        {
                            throw new Exception("Invalid Input");
                        }
                        equation += Math.Atan(GetAngle(lastNum));
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Atan(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("Atan(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.O)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Acos(GetAngle(lastNum));
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Acos(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("Acos(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.U)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Acos(1 / GetAngle(lastNum));                        
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Asec(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("Asec(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.I)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Asin(1 / GetAngle(lastNum));
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Acsc(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("Acsc(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.J)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Atan(1 / GetAngle(lastNum));
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Acot(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("Acot(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.KeyChar == '|')
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Abs(lastNum);
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Abs(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("Abs(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.KeyChar == '(' || cki.KeyChar == ')')
                    {
                        equation += cki.KeyChar;
                        outputFirstLine.Append(cki.KeyChar);
                        ClearInput(equation);
                    }
                }
                else
                {
                    if (cki.Key == ConsoleKey.Escape)
                    {
                        equation = "";
                        outputSecondline.Clear();
                        outputFirstLine.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.Y)
                    {
                        equation += "^";
                        outputSecondline.Replace(outputSecondline.ToString(), "0");
                        outputFirstLine.Append("^");
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.Q)
                    {
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Sqr(" + GetLastValue() + ")");
                        }
                        else
                        {
                            string eq = equation;
                            outputFirstLine.Append("Sqr(" + GetLastNum(ref eq) + ")");
                            MathFunctionCalled = true;
                        }
                        equation += "^2";
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.P)
                    {
                        equation += Math.PI;
                        outputFirstLine.Append(Math.PI);
                        MathFunctionCalled = true;
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.R)
                    {
                        double lastValue = GetLastNum(ref equation);
                        equation += "1/" + lastValue;
                        outputFirstLine.Append("1/" + lastValue);
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.F9)
                    {
                        double lastNum = GetLastNum(ref equation);
                        if (equation.Length > 0)
                        {
                            equation = equation.Remove(equation.Length - 1);
                            outputFirstLine.Remove(outputFirstLine.Length - 1, 1);
                        }
                        lastNum *= -1;
                        if (lastNum >= 0)
                        {
                            outputFirstLine.Append("+" + lastNum);
                            equation += "+";
                        }
                        else
                        {
                            outputFirstLine.Append(lastNum);
                        }
                        equation += lastNum;
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.G)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += powerCalculator.Power(2, lastNum);
                        outputFirstLine.Append(powerCalculator.Power(2, lastNum));
                        MathFunctionCalled = true;
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.L)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Log(lastNum);
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Log(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("Log(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.S)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Sin(GetAngleUsingMathFunction(lastNum));
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Sin(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("Sin(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.T)
                    {
                        double lastNum = GetLastNum(ref equation);
                        if (lastNum == 90 || lastNum == 270)
                        {
                            throw new Exception("Invalid Input");
                        }
                        equation += Math.Tan(GetAngleUsingMathFunction(lastNum));
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Tan(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("Tan(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.O)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Cos(GetAngleUsingMathFunction(lastNum));
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Cos(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("Cos(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.U)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += 1 / Math.Cos(GetAngleUsingMathFunction(lastNum));
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Sec(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("Sec(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.I)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += 1 / Math.Sin(GetAngleUsingMathFunction(lastNum));
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Csc(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("Csc(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
                        outputSecondline.Clear();
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.J)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += 1 / Math.Tan(GetAngleUsingMathFunction(lastNum));
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Cot(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("Cot(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
                        outputSecondline.Clear();
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
                        if ((outputThirdLine.ToString()).Contains("GRAD"))
                        {
                            outputThirdLine.Replace("GRAD", "DEG");
                        }
                        else if ((outputThirdLine.ToString()).Contains("RAD"))
                        {
                            outputThirdLine.Replace("RAD", "DEG");
                        }
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.E)
                    {
                        equation += Math.E;
                        outputSecondline.Clear();
                        outputFirstLine.Append(Math.E);
                        MathFunctionCalled = true;
                        ClearInput(equation);
                    }
                    else if (cki.Key == ConsoleKey.N)
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Log(lastNum, Math.E);
                        outputSecondline.Replace(outputSecondline.ToString(), (Math.Log(lastNum, Math.E)).ToString());
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("ln(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("ln(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
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
                        if (equation == "" && result != null)
                        {
                            outputSecondline.Replace(outputSecondline.ToString(), PrintResult(result.ToString()));
                        }
                    }
                    else if (cki.Key == ConsoleKey.UpArrow)
                    {
                        if (historyPointer > 0)
                        {
                            historyPointer--;
                            equation = history[historyPointer];
                            outputFirstLine.Replace(outputFirstLine.ToString(), equation);
                            outputSecondline.Replace(outputSecondline.ToString(), Calculator(ref index, equation, true).ToString());
                            ClearInput(equation);
                        }
                        else
                        {
                            equation = "";
                            outputFirstLine.Clear();
                            outputSecondline.Clear();
                            ClearInput(equation);
                        }
                    }
                    else if (cki.Key == ConsoleKey.DownArrow)
                    {
                        historyPointer++;
                        if (history[historyPointer] != null)
                        {
                            equation = history[historyPointer];
                            outputFirstLine.Replace(outputFirstLine.ToString(), equation);
                            outputSecondline.Replace(outputSecondline.ToString(), Calculator(ref index, equation, true).ToString());
                            ClearInput(equation);
                        }
                        else
                        {
                            historyPointer--;
                            outputSecondline.Clear();
                            equation = "";
                            outputFirstLine.Clear();
                            ClearInput(equation);
                        }
                    }
                    else if (cki.Key == ConsoleKey.Z)
                    {
                        history = null;
                        historyPointer = 0;
                        history = new string[short.MaxValue - 1];
                    }
                    else if (cki.KeyChar == '[')
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Floor(lastNum);
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("floor(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("floor(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
                        ClearInput(equation);
                    }
                    else if (cki.KeyChar == ']')
                    {
                        double lastNum = GetLastNum(ref equation);
                        equation += Math.Ceiling(lastNum);
                        if (MathFunctionCalled)
                        {
                            outputFirstLine.Append("Ceil(" + GetLastValue() + ")");
                        }
                        else
                        {
                            outputFirstLine.Append("Ceil(" + lastNum + ")");
                            MathFunctionCalled = true;
                        }
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
                                if (!MathFunctionCalled)
                                {
                                    outputFirstLine.Append(outputSecondline.ToString() + "=");
                                }
                                else
                                {
                                    outputFirstLine.Append("=");
                                }
                                gettingInput = false;
                            }
                            if (lastOperator)
                            {
                                equation += outputSecondline.ToString();
                            }
                            ClearInput(equation + "=");
                            history[historyPointer] = equation;
                            historyPointer++;
                            outputSecondline.Clear();
                        }
                        else if (input == 8 || cki.Key == ConsoleKey.Delete)
                        {
                            if (outputSecondline.Length == 0 || lastOperator || equation.Length == 0)
                                continue;

                            outputSecondline.Remove(outputSecondline.Length - 1, 1);
                            equation = equation.Remove(equation.Length - 1);
                            ClearInput(equation);
                        }
                        else if (input == ' ' || (input == '.' && decimalCounted))
                        {
                            continue;
                        }
                        else
                        {
                            if (!(input == '*' || input == '/' || input == '%' || input == '^'))
                            {
                                if (outputSecondline.ToString() == "0")
                                {
                                    outputSecondline.Replace("0", input.ToString());
                                }
                                else if (index == -1)
                                {
                                    outputSecondline.Append(input);
                                }
                                else
                                {
                                    outputSecondline.Replace(outputSecondline.ToString(), input.ToString());
                                    index = -1;
                                }
                            }
                            else
                            {
                                if (!MathFunctionCalled && (outputSecondline.Length > 0))
                                {
                                    if (outputFirstLine.ToString() == "0")
                                        outputFirstLine.Replace("0", outputSecondline.ToString());
                                    else
                                        outputFirstLine.Append(outputSecondline);
                                }
                                outputSecondline.Clear();
                            }
                            if (input == '+' || input == '-' || input == '*' || input == '/' || input == '%' || input == '^')
                            {
                                if (!MathFunctionCalled && (outputSecondline.Length > 0))
                                {
                                    if (outputFirstLine.ToString() == "0")
                                        outputFirstLine.Replace("0", outputSecondline.ToString());
                                    else
                                        outputFirstLine.Append(outputSecondline);
                                }

                                if (equation.Length > 0 && equation.Last() == '.')
                                {
                                    equation += "0";
                                }
                                if (equation == "")
                                {
                                    if (result == null && !(input == '+' || input == '-'))
                                    {
                                        throw new Exception("");
                                    }
                                    else if (result != null)
                                    {
                                        equation += result;
                                        outputSecondline.Replace(outputSecondline.ToString(), result.ToString());
                                    }
                                }
                                else if (input == '+' || input == '-')
                                {
                                    if (equation != "" && !equation.Contains('(') && !equation.Contains(')') && !lastOperator)
                                    {
                                        index = -1;
                                        outputSecondline.Replace(outputSecondline.ToString(), Calculator(ref index, equation, true).ToString());
                                    }
                                    if (MathFunctionCalled)
                                    {
                                        outputFirstLine.Append(input);
                                    }
                                }
                                else
                                {
                                    outputFirstLine.Append(input);
                                }
                                if (lastOperator)
                                {
                                    equation = equation.Remove(equation.Length - 1);
                                }
                                lastOperator = true;
                                decimalCounted = false;
                            }
                            else if (input == '.')
                            {
                                if (MathFunctionCalled)
                                {
                                    GetLastNum(ref equation);
                                    GetLastValue();
                                }
                                decimalCounted = true;
                                lastOperator = false;
                            }
                            else
                            {
                                if (MathFunctionCalled)
                                {
                                    GetLastNum(ref equation);
                                    GetLastValue();
                                }
                                lastOperator = false;
                            }
                            equation += input;
                            ClearInput(equation);
                            MathFunctionCalled = false;
                        }
                    }
                }
            }
            Console.WriteLine();
        }

        //BODMAS || BOPS
        public static double Calculator(ref int i, string equation, bool isMain = false)
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
                            Push(operands, ref positionForOperand, Math.Round(integer, 10));
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
                        Push(operands, ref positionForOperand, Math.Round(integer, 10));
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
                    throw new Exception("");
                }
            }

            if (!isMain && !closingBracket)
            {
                throw new Exception("");
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
                        else if (secondNumber == (1.0 / 3.0) || secondNumber == 0.3333333333)
                        {
                            result = Math.Round(powerCalculator.CubeRoot(firstNumber), 5);
                        }
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
                throw new Exception("");
            }
        }

        public static void ClearInput(string equation)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            if (outputFirstLine.Length == 0)
            {
                Console.Write(0);
            }
            else
            {
                Console.WriteLine(outputFirstLine);
            }
            Console.SetCursorPosition(0, 1);
            if (outputSecondline.Length == 0)
            {
                Console.WriteLine(0);
            }
            else
            {
                Console.WriteLine(outputSecondline);
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
                return num * ((22 / 7) / 180);
            }
            else if ((outputThirdLine.ToString()).Contains("GRAD"))
            {
                return num * ((22 / 7) / 200);
            }
            else
            {
                return num;
            }
        }

        public static double GetAngleUsingMathFunction(double num)
        {
            if ((outputThirdLine.ToString()).Contains("DEG"))
            {
                return num * (Math.PI / 180);
            }
            else if ((outputThirdLine.ToString()).Contains("GRAD"))
            {
                return num * (Math.PI / 200);
            }
            else
            {
                return num;
            }
        }

        public static void PrintHelp()
        {
            string printLine =
@"CTRL + M    - Store in memory, in Standard mode, Scientific mode
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
CTRL + G    - Select 10^x in Scientific mode
S           - Select Sin in in Scientific mode
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
V           - Toggles on/off F-E buttonin Scientific mode
N           - Select ln in Scientific mode
UP ARROW    - Move up in history list
DOWN ARROW  - Move down in history list
Z           - Clear History
CTRL + N    - Select e^x in Scientific mode











Press Any Key To return Back to Calculator Window
";
            Console.WriteLine(printLine);
        }

        public static string PrintResult(string result)
        {
            if (result == "NaN")
            {
                return "0";
            }
            if ((outputThirdLine.ToString()).Contains("F-E"))
            {
                string resultInString = Convert.ToDouble(result).ToString("e");
                StringBuilder returnString = new StringBuilder("");
                foreach (char ch in resultInString)
                {
                    if (ch == '0')
                    {
                        continue;
                    }
                    else
                    {
                        returnString.Append(ch);
                    }
                }
                return returnString.ToString();
            }
            else
            {
                return result;
            }
        }

        public static string GetLastValue()
        {
            if (outputFirstLine.Length == 0)
                return "";

            else
            {
                string sub = "";
                int i;
                int count = 0;
                for (i = outputFirstLine.Length - 1; i >= 0; i--)
                {
                    if (outputFirstLine[i] == '*' || outputFirstLine[i] == '/' || outputFirstLine[i] == '-' || outputFirstLine[i] == '+' || outputFirstLine[i] == '^' || outputFirstLine[i] == '%')
                    {
                        break;
                    }
                    count++;
                }
                sub = (outputFirstLine.ToString()).Substring(i + 1, count);
                outputFirstLine.Remove(i + 1, count);
                return sub;
            }
        }
    }
}
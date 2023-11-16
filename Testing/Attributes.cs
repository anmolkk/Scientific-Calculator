using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;


namespace AttributeExample
{
    [System.AttributeUsage(System.AttributeTargets.All)]
    public class StudentAttribute : System.Attribute
    {
        public string Name;
        public int Age;

        public string GetName() => Name;

        public int GetAge() => Age;
    }

    internal class Attributes
    {
        [Conditional("DEBUG")]
        public static void FunctionForDebugMode()
        {
            Console.WriteLine("This is Debug Mode ");
        }


        [Student(Name="Anmol", Age=20)]
        public static void UsingStudentAttribute()
        {
            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(typeof(StudentAttribute));

            Console.WriteLine("ENter");
            foreach (var attr in attrs)
            {
                
                if (attr is StudentAttribute a)
                {
                    Console.WriteLine("ENter in loop");
                    Console.WriteLine($"Name:   {a.GetName()}, Age: {a.GetAge()}");
                }
            }
        }

        public static void Entry()
        {
            UsingStudentAttribute();
            FunctionForDebugMode();
            
        }

    }
}

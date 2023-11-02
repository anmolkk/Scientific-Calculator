using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Optional
{
    internal class OptionalParams
    {
        public static void Option(int[] val , [Optional] float num) 
        {
            for (int i=0; i < val.Length; i++)
            {
                Console.WriteLine(val[i]);  
            }
            Console.WriteLine(num);
        }
    }
}

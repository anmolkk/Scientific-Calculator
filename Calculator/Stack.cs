using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOperations
{
    internal class Stack
    {
        /// <summary>
        /// Stack Method to push value at top of array and incremnt the pointer value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="position"></param>
        /// <param name="data"></param>
        protected static void Push<T>(T[] array, ref int position, T data)
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
        protected static T Pop<T>(T[] array, ref int position)
        {
            position--;
            //Console.WriteLine($"Popped value : {array[position]}");
            return array[position];
        }
    }
}

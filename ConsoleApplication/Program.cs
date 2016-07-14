using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iterator;
namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

            CustomIterator iterator = new CustomIterator();
            foreach (var item in iterator.GetPrimes(5))
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }
}

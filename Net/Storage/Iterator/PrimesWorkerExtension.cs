using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterator
{
    public static class PrimesWorkerExtension
    {
        public static bool IsPrime(this int number)
        {
            if (number < 2)
            {
                return false;
            }
            
            if (number % 2 == 0 && number != 2)
            {
                return false;
            } 

            for (int i = 3; i < number; i += 2)
            {
                if (number % i == 0)
                {
                    return false;
                }                
            }

            return true;            
        }
    }
}

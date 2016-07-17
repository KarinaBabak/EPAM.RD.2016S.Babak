using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterator
{
    public static class PrimesWorker
    {
        public static bool IsPrime(this Int32 number)
        {
            if(number < 2)
            {
                return false;
            }

            int sqrt = (int)Math.Sqrt(number);

            for (int i = 2; i <= sqrt; i++)
            {
                if (number % i == 0)
                    return false;
            }

            return true;            
        }
    }
}

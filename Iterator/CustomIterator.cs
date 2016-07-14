using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Iterator
{
    public class CustomIterator: ICustomerIterator
    {
        public int Current
        {
            get; private set;
        }

        public CustomIterator()
        {
            Current = 1;
        }

        public int GetNext()
        {
            if (MoveNext())
                return Current;
            return -2;
        }

        public void Reset()
        {
            Current = 1;
        }

        public bool MoveNext()
        {
            for (int number = Current; number <= Int32.MaxValue; number++)
            {
                if (number.IsPrime())
                {
                    Current++;
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<int> GetPrimes(int limit)
        {
            if (limit <= 0)
            {
                throw new ArgumentException("Limit is below zero");
            }

            for (int i = 2; i <= limit; i++)
            {
                if (i.IsPrime())
                {
                    yield return i;
                }
            }
        }

        
    }
}

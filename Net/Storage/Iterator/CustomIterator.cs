using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Iterator
{
    public class CustomIterator: ICustomIterator
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

        public IEnumerable<int> GetPrimes()
        {
            for (int i = 2; i <= Int32.MaxValue; i++)
            {
                if (i.IsPrime())
                {
                    yield return i;
                }
            }
        }

        
    }
}

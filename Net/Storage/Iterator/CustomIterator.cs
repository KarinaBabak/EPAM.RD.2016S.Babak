using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Iterator
{
    [Serializable]
    public class CustomIterator : ICustomIterator
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
            if (this.MoveNext())
                return this.Current;
            return -2;
        }

        public void Reset()
        {
            this.Current = 1;
        }

        public bool MoveNext()
        {
            for (int number = this.Current; number <= int.MaxValue; number++)
            {
                if (number.IsPrime())
                {
                    this.Current++;
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<int> GetPrimes()
        {
            for (int i = 2; i <= int.MaxValue; i++)
            {
                if (i.IsPrime())
                {
                    yield return i;
                }
            }
        }

        
    }
}

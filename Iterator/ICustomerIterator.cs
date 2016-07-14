using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterator
{
    public interface ICustomerIterator
    {
        int Current { get; }
        bool MoveNext();
        int GetNext();
        void Reset();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterator
{
    public interface ICustomIterator
    {
        int Current { get; set; }
        bool MoveNext();
        int GetNext();
        void Reset();
    }
}

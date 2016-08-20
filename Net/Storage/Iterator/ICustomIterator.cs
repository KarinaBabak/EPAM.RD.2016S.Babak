using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterator
{
    /// <summary>
    /// Determination of base functionality of iterator
    /// </summary>
    public interface ICustomIterator
    {
        /// <summary>
        /// Current element
        /// </summary>
        int Current { get; set; }

        /// <summary>
        /// Check of existing the next element
        /// </summary>
        /// <returns>true if the iteration is possible</returns>
        bool MoveNext();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int GetNext();
        void Reset();
    }
}

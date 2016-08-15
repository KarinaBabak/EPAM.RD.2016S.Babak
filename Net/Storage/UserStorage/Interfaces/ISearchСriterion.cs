using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.Interfaces
{
    public interface ISearchСriterion<T>
    {
        bool MatchByCriterion(User user);
    }
}

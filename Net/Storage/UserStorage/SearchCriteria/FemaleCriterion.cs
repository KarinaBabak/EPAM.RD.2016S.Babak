using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorage.Interfaces;

namespace UserStorage.SearchCriteria
{
    [Serializable]
    public class FemaleCriterion : ISearchСriterion<User>
    {
        public bool MatchByCriterion(User user)
        {
            if(user!= null)
            {
                return user.UserGender == Gender.Female;
            }
            return false;
        }
    }
}

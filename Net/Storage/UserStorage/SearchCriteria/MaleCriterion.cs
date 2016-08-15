using System;
using System.Collections.Generic;
using System.Linq;
using UserStorage.Interfaces;

namespace UserStorage.SearchCriteria
{
    [Serializable]
    public class MaleCriterion : ISearchСriterion<User>
    {
        public bool MatchByCriterion(User user)
        {
            return user.UserGender == Gender.Male;
        }
    }
}

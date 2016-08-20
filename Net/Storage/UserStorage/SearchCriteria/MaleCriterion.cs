using System;
using System.Collections.Generic;
using System.Linq;
using UserStorage.Interfaces;

namespace UserStorage.SearchCriteria
{
    /// <summary>
    /// Search criterion is male 
    /// </summary>
    [Serializable]
    public class MaleCriterion : ISearchСriterion<User>
    {
        /// <summary>
        /// Matching male criterion with user's gender
        /// </summary>
        /// <param name="user">user for searching</param>
        /// <returns>true if the gender of user is male</returns>
        public bool MatchByCriterion(User user)
        {
            return user.UserGender == Gender.Male;
        }
    }
}

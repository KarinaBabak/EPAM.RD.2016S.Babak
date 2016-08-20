using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorage.Interfaces;

namespace UserStorage.SearchCriteria
{
    /// <summary>
    /// Search criterion is female 
    /// </summary>
    [Serializable]
    public class FemaleCriterion : ISearchСriterion<User>
    {
        /// <summary>
        /// Matching female criterion with user's gender
        /// </summary>
        /// <param name="user">user for searching</param>
        /// <returns>true if the gender of user is female</returns>
        public bool MatchByCriterion(User user)
        {
            if (user != null)
            {
                return user.UserGender == Gender.Female;
            }

            return false;
        }
    }
}

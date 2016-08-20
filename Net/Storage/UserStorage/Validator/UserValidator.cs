using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.Validator
{
    /// <summary>
    /// Description of user validation
    /// </summary>
    [Serializable]
    public class UserValidator : IValidator<User>
    {
        /// <summary>
        /// Check whether entity is valid
        /// </summary>
        /// <param name="newUser">user for checking</param>
        /// <returns>true if user is valid, else - false</returns>
        public bool Validate(User newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException("New user is not exist");
            }

            if (newUser.DateOfBirth.Year < 1800 && newUser.DateOfBirth.Year != 1)
            {
                return false;
            }

            return true;
        }
    }
}

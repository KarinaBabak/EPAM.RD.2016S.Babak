using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.Validator
{
    [Serializable]
    public class UserValidator : IValidator<User>
    {
        public bool Validate(User newUser)
        {
            if (newUser == null)
                throw new ArgumentNullException("New user is not exist");           

            if (newUser.DateOfBirth.Year < 1900 && newUser.DateOfBirth.Year != 1)
            {
                return false;
            }

            return true;
        }
    }
}

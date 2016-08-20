using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Iterator;

namespace UserStorage
{
    /// <summary>
    /// User class
    /// </summary>
    [Serializable]
    public class User
    {      
        #region ctors
        /// <summary>
        /// Default constrictor
        /// </summary>
        public User()
        {            
            VisaRecords = new List<VisaRecord>();            
        }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="firstName">first name of user</param>
        /// <param name="lastName">last name of user</param>
        /// <param name="dateOfBirth">birth of user</param>
        /// <param name="gender">gender of user</param>
        /// <param name="visaRecords">visa records of user</param>
        public User(string firstName, string lastName, DateTime dateOfBirth, Gender gender, List<VisaRecord> visaRecords = null)
        {                      
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            UserGender = gender;
            VisaRecords = visaRecords;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets Id of user
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a first name of user
        /// </summary>
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a first name of user
        /// </summary>
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets birth of user
        /// </summary>
        public DateTime DateOfBirth
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Gender of user
        /// </summary>
        public Gender UserGender
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Visa records of user
        /// </summary>
        public List<VisaRecord> VisaRecords { get; set; }
        #endregion

        /// <summary>
        /// Override Equals
        /// </summary>
        /// <param name="obj">another user</param>
        /// <returns>true if the users are equal</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            User user = obj as User;
            return Equals(user);
        }

        /// <summary>
        /// Instance equals
        /// </summary>
        /// <param name="user">another user</param>
        /// <returns>true if the objects are equal</returns>
        public bool Equals(User user)
        {
            if (ReferenceEquals(user, null))
            {
                return false;
            }

            return this.FirstName == user.FirstName && this.LastName == user.LastName && this.DateOfBirth == user.DateOfBirth;
        }

        /// <summary>
        /// Override GetHashCode
        /// </summary>
        /// <returns>hash code of the user</returns>
        public override int GetHashCode()
        {           
            unchecked
            {
                if (FirstName != null && LastName != null && DateOfBirth != null)
                {
                    var hashCode = (FirstName.Length ^ 15) + (LastName.Length * 111) + DateOfBirth.GetHashCode();
                    if (VisaRecords != null)
                    {
                        hashCode /= VisaRecords.Count();
                    }

                    return hashCode;
                }                
            }

            return base.GetHashCode();
        }
        
        /// <summary>
        /// Getting necessary information about user 
        /// </summary>
        /// <returns>string with necessary user information</returns>
        public override string ToString()
        {
            string userInfo = Id + " " + FirstName + " " + LastName + " " + DateOfBirth + " " + UserGender;
            return userInfo;
        }     
    }
}

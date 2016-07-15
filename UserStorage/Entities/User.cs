using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iterator;

namespace UserStorage
{
    [Serializable]
    public class User
    {
        private ICustomerIterator iterator;  
        #region Properties

        public int Id { get; set; }
        public string FirstName
        {
            get; set;
        }

        public string LastName
        {
            get; set;
        }             

        public DateTime DateOfBirth
        {
            get; set;
        }

        public Gender UserGender
        {
            get; set;
        }

        public IEnumerable<VisaRecord> VisaRecords { get; set; }
        #endregion

        #region ctors
        public User()
        {            
            VisaRecords = new List<VisaRecord>();            
        }

        public User(string firstName, string lastName, DateTime dateOfBirth, Gender gender, VisaRecord[] visaRecords = null)
        {                      
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            UserGender = gender;
            VisaRecords = visaRecords;
        }
        #endregion

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            User user = obj as User;
            return Equals(user);
        }

        public bool Equals(User user)
        {
            if (ReferenceEquals(user, null)) return false;
            return (this.FirstName == user.FirstName && 
                this.LastName == user.LastName && 
                this.DateOfBirth == user.DateOfBirth);
        }

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
        
        public override string ToString()
        {
            string userInfo = Id + " " + FirstName + " " + LastName + " " + DateOfBirth + " " + UserGender;
            return userInfo;
        }      

    }
}

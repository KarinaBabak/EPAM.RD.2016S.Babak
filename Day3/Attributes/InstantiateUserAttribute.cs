using System;
using System.ComponentModel;

namespace Attributes
{
    // Should be applied to classes only.
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class InstantiateUserAttribute : Attribute
    {
        public int Id { get; private set; }
        public string ProperyFirstName { get; set; }
       
        public string PropertyLastName { get; set; }

        #region ctors
        public InstantiateUserAttribute() { }
        public InstantiateUserAttribute(int id, string firstName, string lastName)
        {
            Id = id;
            ProperyFirstName = firstName;
            PropertyLastName = lastName;
        }

        public InstantiateUserAttribute(string firstName, string lastName)            
        {
            Type userclass = typeof(User);
            MatchParameterWithPropertyAttribute[] matchParameter =
                (MatchParameterWithPropertyAttribute[])Attribute.GetCustomAttributes(userclass.GetConstructors()[0], typeof(MatchParameterWithPropertyAttribute));

            var proper = userclass.GetProperty(matchParameter[0].Value2);
            DefaultValueAttribute[] propertyAttributes =
                (DefaultValueAttribute[])Attribute.GetCustomAttributes(proper, typeof(DefaultValueAttribute));
            
            Id = (int)propertyAttributes[0].Value;
            ProperyFirstName = firstName;
            PropertyLastName = lastName;
        }        
        #endregion
    }
}

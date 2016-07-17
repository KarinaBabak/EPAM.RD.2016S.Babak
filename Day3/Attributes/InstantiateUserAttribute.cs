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

        public InstantiateUserAttribute(string firstName, string lastName)
        {
            Type userclass = typeof(User);
            InstantiateUserAttribute[] instantiateUserAttributes =
                (InstantiateUserAttribute[])Attribute.GetCustomAttributes(userclass, typeof(InstantiateUserAttribute));

            MatchParameterWithPropertyAttribute[] matchParameter =
                (MatchParameterWithPropertyAttribute[])Attribute.GetCustomAttributes(userclass.GetConstructors()[0], typeof(MatchParameterWithPropertyAttribute));

            AttributeCollection attributes = TypeDescriptor.GetProperties(this)["Id"].Attributes;
            DefaultValueAttribute myAttribute = (DefaultValueAttribute)attributes[typeof(DefaultValueAttribute)];
            
            Id = (int)myAttribute.Value;
            //User user = new User(Id);
            ProperyFirstName = firstName;
            PropertyLastName = lastName;
        }

        public InstantiateUserAttribute(int id, string firstName, string lastName)
        {
            Id = id;
            //User user = new User(id);
            ProperyFirstName = firstName;
            PropertyLastName = lastName;
        }
        #endregion
    }
}

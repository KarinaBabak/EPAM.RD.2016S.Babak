using System;

namespace Attributes    
{
    // Should be applied to assembly only.
    [AttributeUsage(AttributeTargets.Assembly)]
    public class InstantiateAdvancedUserAttribute : InstantiateUserAttribute
    {
        private int externalId;      

        public InstantiateAdvancedUserAttribute(int id,  string firstName, string lastName, int externalId)
            :base(id, firstName, lastName)
        {
            this.externalId = externalId;
        }
    }
}

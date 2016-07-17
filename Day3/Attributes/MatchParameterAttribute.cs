using System;

namespace Attributes
{
    // Should be applied to .ctors.
    // Matches a .ctor parameter with property. Needs to get a default value from property field, and use this value for calling .ctor.
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor, AllowMultiple = true)]
    public class MatchParameterWithPropertyAttribute : Attribute
    {
        public string Value1 { get; private set; }
        public string Value2 { get; private set; }

        public MatchParameterWithPropertyAttribute(string v1, string v2)
        {
            this.Value1 = v1;
            this.Value2 = v2;
        }
    }
}

using System;

namespace Attributes
{
    // Should be applied to properties and fields.
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IntValidatorAttribute : Attribute
    {
        public int Min { get; set; }
        public  int Max { get; set; }

        public IntValidatorAttribute() { }
        public IntValidatorAttribute(int n1, int n2)
        {
            Min = n1;
            Max = n2;
            
        }      
    }
}

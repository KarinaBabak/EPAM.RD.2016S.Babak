using System;

namespace Attributes
{
    // Should be applied to properties and fields.
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IntValidatorAttribute : Attribute
    {
        private int min;
        private int max;

        public IntValidatorAttribute() { }
        public IntValidatorAttribute(int n1, int n2)
        {
            min = n1;
            max = n2;
            
        }      
    }
}

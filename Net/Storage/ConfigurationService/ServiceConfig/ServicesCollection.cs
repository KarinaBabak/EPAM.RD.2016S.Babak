using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConfigurationService    
{
    /// <summary>
    /// It provides interaction with collection of elements described in the app.config.
    /// </summary>
    public class ServicesCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Getting Service elemnt by index 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ServiceElement this[int index]
        {
            get { return (ServiceElement)BaseGet(index); }
        }

        /// <summary>
        /// Create new service element
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ServiceElement();
        }

        /// <summary>
        /// Gets service element key
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServiceElement)element).Login;
        }        
    }
}

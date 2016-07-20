using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConfigurationService
{
    /// <summary>
    /// Describe the element of service
    /// </summary>
    public class ServiceElement: ConfigurationElement
    {
        /// <summary>
        /// Get/Set type of service
        /// </summary>
        [ConfigurationProperty("serviceType", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string ServiceType
        {
            get { return ((string)(base["serviceType"])); }
            set { base["serviceType"] = value; }
        }

        /// <summary>
        /// Count of services
        /// </summary>
        [ConfigurationProperty("login", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string Login
        {
            get { return ((string)(base["login"])); }
            set { base["login"] = value; }
        }
    }
}

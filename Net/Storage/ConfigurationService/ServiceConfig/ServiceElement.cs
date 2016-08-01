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
    public class ServiceElement : ConfigurationElement
    {
        /// <summary>
        /// Get/Set type of service
        /// </summary>
        [ConfigurationProperty("serviceType", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string ServiceType
        {
            get { return ((string)(base["serviceType"])); }
            set { base["serviceType"] = value; }
        }

        /// <summary>
        /// Count of services
        /// </summary>
        [ConfigurationProperty("login", DefaultValue = "", IsKey = true, IsRequired = false)]
        public string Login
        {
            get { return (string)(base["login"]); }
            set { base["login"] = value; }
        }

        /// <summary>
        /// IP address of service
        /// </summary>
        [ConfigurationProperty("address", DefaultValue = "127.0.0.1", IsKey = true, IsRequired = false)]
        public string Address
        {
            get { return (string)(base["address"]); }
            set { base["address"] = value; }
        }

        /// <summary>
        /// Port of service
        /// </summary>
        [ConfigurationProperty("port", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Port
        {
            get { return (string)(base["port"]); }
            set { base["port"] = value; }
        }
    }
}

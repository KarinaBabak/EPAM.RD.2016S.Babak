using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConfigurationService.NetworkConfig
{
    public class NetworkElement : ConfigurationElement
    {
        [ConfigurationProperty("address", DefaultValue = "127.0.0.1", IsKey = true, IsRequired = true)]
        public string Address
        {
            get { return ((string)(base["address"])); }
            set { base["address"] = value; }
        }

        
        [ConfigurationProperty("port", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string Port
        {
            get { return ((string)(base["port"])); }
            set { base["port"] = value; }
        }

        
    }
}

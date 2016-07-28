using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConfigurationService.NetworkConfig
{
    [ConfigurationCollection(typeof(NetworkElement), AddItemName = "Channel")]
    public class NetworkCollection : ConfigurationElementCollection
    {
        public NetworkElement this[int index]
        {
            get { return (NetworkElement)BaseGet(index); }
        }
       
        protected override ConfigurationElement CreateNewElement()
        {
            return new NetworkElement();
        }
       
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((NetworkElement)(element)).Port;
        }
    }
}

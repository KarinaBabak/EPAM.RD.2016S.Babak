using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConfigurationService.NetworkConfig
{
    public class NetworkConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("NetworkConfig")]
        public NetworkCollection NetworkItems
        {
            get { return ((NetworkCollection)(base["NetworkChannels"])); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConfigurationService
{    
    /// <summary>
    /// Describe a section in the configuration file
    /// </summary>
    public class ServiceConfigSection : ConfigurationSection
    {
       
        [ConfigurationProperty("Services")]
        public ServicesCollection ServiceItems
        {
            get { return ((ServicesCollection)(base["Services"])); }
        }
    }
}

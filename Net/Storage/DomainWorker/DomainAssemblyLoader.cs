using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorage.Interfaces;

namespace DomainWorker
{    
    /// <summary>
    /// Class for creating service
    /// </summary>
    public class DomainAssemblyLoader : MarshalByRefObject
    {
        /// <summary>
        /// Creating a new service
        /// </summary>
        /// <param name="typeService">string contains the type if service for creating</param>
        /// <returns>create service</returns>
        public UserService CreateService(string typeService)
        {
            if (typeService.Contains("Master"))
            {
                return new MasterService();
            }

            if (typeService.Contains("Slave"))
            {
                return new SlaveService();
            }

            return null;
        }
    }
}

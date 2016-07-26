﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.Interfaces
{
    public class DomainAssemblyLoader : MarshalByRefObject
    {
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

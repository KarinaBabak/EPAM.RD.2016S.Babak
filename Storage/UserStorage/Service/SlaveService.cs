using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Configuration;

using UserStorage;
using UserStorage.Interfaces;
using UserStorage.Interfaces.Observer;
using ConfigurationService;


namespace UserStorage.Interfaces
{
    public class SlaveService : UserService, IObserver
    {        
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
                            
        public SlaveService()
        {                
        }

        public new void Add(User user)
        {
            logger.Error("Slaves can not add new user");
            throw new InvalidOperationException();
        }

        public new void Delete(User user)
        {
            logger.Error("Slaves can not delete user");
            throw new InvalidOperationException();
        }        

        public void Update(MessageService message)
        {
            Repository.Add(message.UserData);
        }        
    }
}

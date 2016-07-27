using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorage.Interfaces;

namespace UserStorage.Interfaces.Observer
{
    public interface IObserver
    {
        void Update(MessageService massage);
    }
}

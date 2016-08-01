using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.NetworkWorker
{
    [Serializable]
    public class DataUpdatedEventArgs : EventArgs
    {
        public User User { get; set; }
    }
}

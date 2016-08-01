using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.NetworkWorker
{
    [Serializable]
    public enum MethodType
    {
        Add = 0,
        Delete = 1
    }

    [Serializable]
    public class Message
    {
        public User UserData { get; set; }

        public MethodType Operation { get; set; }
    }    
}

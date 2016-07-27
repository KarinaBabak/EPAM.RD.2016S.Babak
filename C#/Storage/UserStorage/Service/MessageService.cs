using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.Interfaces
{
    [Serializable]
    public class MessageService
    {
        public User UserData { get; set; }
        public MethodType Operation { get; set; }
    }

    [Serializable]
    public enum MethodType
    {
        Add,
        Delete
    }
}

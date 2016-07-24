using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.Service
{
    [Serializable]
    public class MessageService
    {
        public User UserData { get; set; }
        public Operation Operation { get; set; }
    }

    [Serializable]
    public enum Operation
    {
        Add,
        Delete
    }
}

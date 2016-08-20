using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.NetworkWorker
{
    /// <summary>
    /// Method's type
    /// </summary>
    [Serializable]
    public enum MethodType
    {
        /// <summary>
        /// Method Add
        /// </summary>
        Add = 0,

        /// <summary>
        /// Method Delete
        /// </summary>
        Delete = 1
    }

    /// <summary>
    /// Description of message entity
    /// </summary>
    [Serializable]
    public class Message
    {
        /// <summary>
        /// Gets or sets user that add or delete
        /// </summary>
        public User UserData { get; set; }

        /// <summary>
        /// Gets or sets method's type
        /// </summary>
        public MethodType Operation { get; set; }
    }    
}

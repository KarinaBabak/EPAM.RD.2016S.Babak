using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.NetworkWorker
{
    /// <summary>
    /// Event for update user information
    /// </summary>
    [Serializable]
    public class DataUpdatedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets User 
        /// </summary>
        public User User { get; set; }
    }
}

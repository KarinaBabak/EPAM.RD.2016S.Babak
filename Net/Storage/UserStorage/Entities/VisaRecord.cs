using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace UserStorage
{
    /// <summary>
    /// Visa records description
    /// </summary>
    [Serializable]   
    public struct VisaRecord
    {       
        /// <summary>
        /// Gets or sets country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets Date start of visa record
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets Date end of visa record
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}

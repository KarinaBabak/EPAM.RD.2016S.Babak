using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace UserStorage
{
    [Serializable]   
    public struct VisaRecord
    {       
        public string Country { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}

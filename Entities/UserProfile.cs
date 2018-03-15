using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G1Maestro.Entities
{
    [Serializable]
    public class UserProfile
    {
        public string Name { get; set; }

        public string CompanyName { get; set; }
    }
}
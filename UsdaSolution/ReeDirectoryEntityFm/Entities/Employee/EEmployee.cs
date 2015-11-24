using System.Collections.Generic;
using ReeDirectoryEntityFm.Entities.Base;
using ReeDirectoryEntityFm.Entities.General;

namespace ReeDirectoryEntityFm.Entities.Employee
{
    public class EEmployee : EBase
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string Street1 { get; set; }
        public string Street2 { get; set; }

        public string Zip { get; set; }
        public ECity City { get; set; }
    }
}
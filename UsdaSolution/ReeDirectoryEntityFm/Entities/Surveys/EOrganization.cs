using System.Collections.Generic;
using ReeDirectoryEntityFm.Entities.Base;

namespace ReeDirectoryEntityFm.Entities.Surveys
{
    public class EOrganization : EBase
    {
        public string Name { get; set; }
        public List<EOrganization> Organizations { get; set; }
    }
}

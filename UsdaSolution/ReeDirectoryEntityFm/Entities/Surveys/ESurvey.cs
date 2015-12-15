using System;
using System.Collections.Generic;
using ReeDirectoryEntityFm.Entities.Base;
using ReeDirectoryEntityFm.Entities.Security;

namespace ReeDirectoryEntityFm.Entities.Surveys
{
    public class ESurvey: EBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public EUser User { get; set; }
        public List<ESection> Sections { get; set; }
    }
}

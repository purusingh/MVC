using System.Collections.Generic;
using ReeDirectoryEntityFm.Entities.Base;

namespace ReeDirectoryEntityFm.Entities.Surveys
{
    public class ESection : EBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<EQuestion> Questions { get; set; }
        public ESurvey Survey { get; set; }
    }
}

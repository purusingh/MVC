using System.Collections.Generic;
using ReeDirectoryEntityFm.Entities.Base;

namespace ReeDirectoryEntityFm.Entities.Surveys
{
    public class EQuestionType :EBase
    {
        public string Name { get; set; }
        public string IsMultiple { get; set; }
        public List<EQuestion> Questions { get; set; }
    }
}

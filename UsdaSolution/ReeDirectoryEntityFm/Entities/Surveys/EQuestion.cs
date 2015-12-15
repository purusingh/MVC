using System.Collections.Generic;
using ReeDirectoryEntityFm.Entities.Base;

namespace ReeDirectoryEntityFm.Entities.Surveys
{
    public class EQuestion : EBase
    {
        public string Name { get; set; }
        public bool IsAnswerRequired { get; set; }
        public string PossibleAnswers { get; set; }
        public ESection Section { get; set; }
        public EQuestionType QuestionType { get; set; }
        public List<EAnswer> Answers { get; set; }
    }
}

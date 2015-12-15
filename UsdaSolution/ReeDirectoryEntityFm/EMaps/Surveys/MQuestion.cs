using ReeDirectoryEntityFm.EMaps.Base;
using ReeDirectoryEntityFm.Entities.Surveys;

namespace ReeDirectoryEntityFm.EMaps.Surveys
{
    public class MQuestion: MBase<EQuestion>
    {
        public MQuestion()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(360);
            Property(p => p.IsAnswerRequired).IsRequired();
        }
    }
}

using ReeDirectoryEntityFm.EMaps.Base;
using ReeDirectoryEntityFm.Entities.Surveys;

namespace ReeDirectoryEntityFm.EMaps.Surveys
{
    public class MSection :MBase<ESection>
    {
        public MSection()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(120);
            Property(p => p.Description).IsRequired().HasMaxLength(360);
        }
    }
}

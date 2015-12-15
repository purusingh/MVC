using ReeDirectoryEntityFm.Entities.Base;

namespace ReeDirectoryEntityFm.Entities.Surveys
{
    public class EAnswer : EBase
    {
        public string Name { get; set; }
        public EQuestion Question { get; set; }
    }
}

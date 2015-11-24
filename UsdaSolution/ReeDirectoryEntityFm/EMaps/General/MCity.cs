using ReeDirectoryEntityFm.Entities.General;
using ReeDirectoryEntityFm.EMaps.Base;


namespace ReeDirectoryEntityFm.EMaps.General
{
    public class MCity :MBase<ECity>
    {
        public MCity(){
            Property(p => p.Name).HasMaxLength(120);
        }
    }
}
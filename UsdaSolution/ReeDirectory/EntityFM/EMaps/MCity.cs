using ReeDirectory.EntityFM.Entities;

namespace ReeDirectory.EntityFM.EMaps
{
    public class MCity :MBase<ECity>
    {
        public MCity(){
            Property(p => p.Name).HasMaxLength(120);
        }
    }
}
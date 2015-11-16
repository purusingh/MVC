using System.Data.Entity.ModelConfiguration;
using ReeDirectory.EntityFM.Entities;

namespace ReeDirectory.EntityFM.EMaps
{
    public abstract class MBase<T>:EntityTypeConfiguration<T> where T: EBase
    {
        public MBase()
        {
            this.HasKey(k => k.Id);
        }
    }
}
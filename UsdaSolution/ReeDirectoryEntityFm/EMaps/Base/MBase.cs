using System.Data.Entity.ModelConfiguration;

using System.Collections.Generic;
using ReeDirectoryEntityFm.Entities.Base;

namespace ReeDirectoryEntityFm.EMaps.Base
{
    public abstract class MBase<T>:EntityTypeConfiguration<T> where T: EBase
    {
        public MBase()
        {
            this.HasKey(k => k.Id);
        }
    }
}
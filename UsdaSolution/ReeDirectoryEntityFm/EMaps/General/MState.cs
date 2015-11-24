using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using ReeDirectoryEntityFm.EMaps.Base;
using ReeDirectoryEntityFm.Entities.General;

namespace ReeDirectoryEntityFm.EMaps.General
{
    public class MState : MBase<EState>
    {
        public MState()
        {
            Property(p => p.Abbrivation).IsRequired().HasMaxLength(20).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
            Property(p => p.Name).IsRequired().HasMaxLength(120).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
        }
    }
}
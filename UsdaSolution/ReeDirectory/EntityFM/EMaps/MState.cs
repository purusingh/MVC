using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using ReeDirectory.EntityFM.Entities;

namespace ReeDirectory.EntityFM.EMaps
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
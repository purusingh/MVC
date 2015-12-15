using ReeDirectoryEntityFm.EMaps.Base;
using ReeDirectoryEntityFm.Entities.Surveys;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace ReeDirectoryEntityFm.EMaps.Surveys
{
    public class MOrganization : MBase<EOrganization>
    {
        public MOrganization()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(240).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute { IsUnique = true }));
        }
    }
}

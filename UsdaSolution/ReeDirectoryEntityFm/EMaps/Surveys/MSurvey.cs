using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using ReeDirectoryEntityFm.EMaps.Base;
using ReeDirectoryEntityFm.Entities.Surveys;

namespace ReeDirectoryEntityFm.EMaps.Surveys
{
    public class MSurvey : MBase<ESurvey>
    {
        public MSurvey()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(240).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute { IsUnique = true }));
            Property(p => p.CreateDate).IsRequired();
        }
    }
}

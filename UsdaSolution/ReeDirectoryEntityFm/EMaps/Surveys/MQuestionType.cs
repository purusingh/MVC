using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReeDirectoryEntityFm.EMaps.Base;
using ReeDirectoryEntityFm.Entities.Surveys;

namespace ReeDirectoryEntityFm.EMaps.Surveys
{
    public class MQuestionType: MBase<EQuestionType>
    {
        public MQuestionType()
        {
           
            Property(p => p.Name).IsRequired().HasMaxLength(120).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            
        }
    }
}

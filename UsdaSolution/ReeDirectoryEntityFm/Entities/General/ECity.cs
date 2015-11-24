using System.ComponentModel.DataAnnotations;
using ReeDirectoryEntityFm.Entities.Base;

namespace ReeDirectoryEntityFm.Entities.General
{
    public class ECity : EBase
    {
        [Required(ErrorMessage="Name is required.")]
        [Display(Name="Name")]
        public string Name { get; set; }

        public EState State { get; set; }
    }
}
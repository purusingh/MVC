
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ReeDirectory.EntityFM.Entities
{
    public class ECountry : EBase
    {
        [Required(ErrorMessage = "Abbrivation is required.")]
        [Display(Name = "Abbrivation")]
        public string Abbrivation { get; set; }

        [Required(ErrorMessage="Name is required.")]
        [Display(Name="Name")]
        public string Name { get; set; }

        public List<EState> States { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReeDirectoryEntityFm.Entities.Base;

namespace ReeDirectoryEntityFm.Entities.General
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
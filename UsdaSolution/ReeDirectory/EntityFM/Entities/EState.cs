using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReeDirectory.EntityFM.Entities
{
    public class EState : EBase
    {
        public EState()
        {
            Country = new ECountry();
        }
        [Required(ErrorMessage="Abbriviation is required.")]
        [Display(Name="Abbrivation")]
        public string Abbrivation { get; set; }

        [Required(ErrorMessage="Name is required.")]
        [Display(Name="Name")]
        public string Name { get; set; }


        public ECountry Country { get; set; }
        public List<ECity> Cities { get; set; }
    }
}
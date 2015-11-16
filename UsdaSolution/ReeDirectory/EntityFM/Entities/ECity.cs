
using System.ComponentModel.DataAnnotations;
namespace ReeDirectory.EntityFM.Entities
{
    public class ECity : EBase
    {
        [Required(ErrorMessage="Name is required.")]
        [Display(Name="Name")]
        public string Name { get; set; }

        public EState State { get; set; }
    }
}
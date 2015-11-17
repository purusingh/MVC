using System.ComponentModel.DataAnnotations;


namespace ReeDirectory.EntityFM.Entities
{

    public abstract class EBase
    {
        [Key]
        public int Id { get; set; }
    }
}
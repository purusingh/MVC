using System.ComponentModel.DataAnnotations;


namespace ReeDirectoryEntityFm.Entities.Base
{

    public abstract class EBase
    {
        [Key]
        public int Id { get; set; }
    }
}
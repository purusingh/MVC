
namespace ReeDirectory.EntityFM.Entities.Security
{
    public class ERoleController : EBase
    {
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Print { get; set; }

        public int Role_Id { get; set; }        
        public ERole Role { get; set; }
        
        public int Controller_Id { get; set; }        
        public EController Controller { get; set; }
    }
}
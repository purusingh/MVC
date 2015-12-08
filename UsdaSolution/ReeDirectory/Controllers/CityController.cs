using ReeDirectoryEntityFm.Entities.General;
using ReeDirectory.Models;
using Ninject;

namespace ReeDirectory.Controllers
{
    public class CityController : BaseController<City,ECity>
    {
        [Inject]
        public CityController()
        { 

        }
    }
}
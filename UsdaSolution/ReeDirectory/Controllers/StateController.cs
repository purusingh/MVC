using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReeDirectoryEntityFm.Contexts;
using ReeDirectoryEntityFm.Entities.General;
using ReeDirectory.Models;

namespace ReeDirectory.Controllers
{
    public class StateController : BaseController<State,EState>
    {
        public ActionResult Lists(int countryId)
        {

            IEnumerable<EState> entities = db.States.Where(ent => ent.Country.Id == countryId);
            //if (HttpContext.Request.IsAjaxRequest())
                return Json(new SelectList(entities, "Id", "Name"), JsonRequestBehavior.AllowGet);
            return View(entities);
        }

        protected override void PreCreate()
        {
     
            ViewBag.Countries = new SelectList(db.Countries.ToList(), "Id", "Name");
        }


        protected override void PreCreate(EState model)
        {
            model.Country = db.Countries.FirstOrDefault(e=>e.Id == model.Country.Id);
        }
        
    }
}
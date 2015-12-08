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

            IEnumerable<EState> entities = db.ReeDbSet.Where(ent => ent.Country.Id == countryId).ToList();
            //if (HttpContext.Request.IsAjaxRequest())
                return Json(new SelectList(entities, "Id", "Name"), JsonRequestBehavior.AllowGet);
            return View(entities);
        }

        protected override void PreCreate()
        {
     
            ViewBag.Countries = new SelectList(db.SelectAll(), "Id", "Name");
        }


        protected override void PreCreate(EState model)
        {
            model.Country = db.GetExternalDbSet<ECountry>().FirstOrDefault(e=>e.Id == model.Country.Id);
        }
        
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReeDirectory.EntityFM.Context;
using ReeDirectory.EntityFM.Entities;
using ReeDirectory.Models;

namespace ReeDirectory.Controllers
{
    public class StateController : BaseController<State,EState>
    {
        public ActionResult Lists(int countryId)
        {
            ReeDbContext db = new ReeDbContext();
            IEnumerable<EState> entities = db.States.Where(ent => ent.Country.Id == countryId);
            //if (HttpContext.Request.IsAjaxRequest())
                return Json(new SelectList(entities, "Id", "Name"), JsonRequestBehavior.AllowGet);
            return View(entities);
        }

        protected override void PreCreate()
        {
            ReeDbContext db = new ReeDbContext();
            ViewBag.Countries = new SelectList(db.Countries.ToList(), "Id", "Name");
        }

        
        protected override void PreCreate(ReeDbContext db, EState model)
        {
            model.Country = db.Countries.FirstOrDefault(e=>e.Id == model.Country.Id);
        }
        
    }
}
using System.Web.Mvc;
using System.Linq;
using ReeDirectory.Models;
using ReeDirectory.EntityFM.Entities;
using ReeDirectory.EntityFM.Context;
using System.Linq.Dynamic;
using System.Data.Entity;

namespace ReeDirectory.Controllers
{
    public abstract class BaseController<T, E> : Controller where E: EBase, new() where T : Base<E>, new()
    {

        public ActionResult Index()
        {            
            T model = new T();
            model.CurrentPage = 1;
            model.SortByName = "Id";
            model.SortByOperation = "Desc";
            return Sort(model);
        }

        [HttpPost]
        public ActionResult Sort(T model)
        {
            ReeDbContext db = new ReeDbContext();
            IQueryable<E> queriable = db.Set<E>();

            if(!string.IsNullOrEmpty(model.FilterByValue))
                queriable= queriable.Where(string.Format("{0}.Contains(@0)", model.FilterBy), model.FilterByValue);

            model.TotolRecords = queriable.Count();

            if (!string.IsNullOrEmpty(model.SortByName))
                queriable = queriable.OrderBy(string.Format("{0} {1}", model.SortByName, model.SortByOperation));

            if (model.CurrentPage < 1) //When there is no record
                model.CurrentPage = 1;

            model.Entities = queriable.Skip(model.CurrentPage * model.NoOfRecord - model.NoOfRecord).Take(model.NoOfRecord).ToList();

            return View("Index",model);
        }


        protected virtual void PreCreate()
        {
        }

        [HttpGet]
        public ActionResult Create()
        {
            PreCreate();
            return View();
        }


        protected virtual void PreCreate(ReeDbContext db,E entity)
        { 
        }

        [HttpPost]
        public ActionResult Create(E entity)
        {
            //if (ModelState.IsValid)
           // {
                try
                {
                    ReeDbContext db = new ReeDbContext();
                    PreCreate(db, entity);
                    db.Entry<E>(entity).State = EntityState.Added;
                    
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("Duplicate", "Duplicate entry.");
                }
            //}
            PreCreate();
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int iD)
        {
            try
            {
                ReeDbContext db = new ReeDbContext();
                E entity = db.Set<E>().FirstOrDefault(e => e.Id == iD);
                db.Set<E>().Remove(entity);
                db.SaveChanges();
            }
            catch
            { 
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int iD, FormCollection collection)
        {
            try
            {
                ReeDbContext db = new ReeDbContext();
                E entity = db.Set<E>().FirstOrDefault(ent => ent.Id == iD);
                db.Entry<E>(entity).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("someThingWentWrong", "Error");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int iD)
        {
            try
            {
                ReeDbContext db = new ReeDbContext();
                E entity = db.Set<E>().FirstOrDefault(e => e.Id == iD);
                return View(entity);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(int iD, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ReeDbContext db = new ReeDbContext();
                    E entity = new E();
                    UpdateModel(entity);
                    db.Entry<E>(entity).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("Error", "Something went wrong.");
                }
            }

            return View();
        }        
    }
}
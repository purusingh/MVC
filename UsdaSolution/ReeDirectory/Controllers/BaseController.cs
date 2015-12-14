using System.Web.Mvc;
using ReeDirectory.Models;
using ReeDirectoryEntityFm.Entities.Base;
using ReeDirectory.ActionFilters;
using ReeDirectoryEntityFm.ExternalEntity;
using System.Data.SqlClient;
using System.Data.Entity.Validation;
using System.IO;
using Microsoft.Reporting.WebForms;
using Ninject;
using ReeDirectoryEntityFm.Repositories;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace ReeDirectory.Controllers
{
    public abstract class BaseController<T, E> : System.Web.Mvc.Controller
        where E : EBase, new()
        where T : Base<E>, new()
    {
        #region Properties        
        [Inject]
        public virtual IReeRepository<E> db { protected get; set; }
        #endregion Properties

      
        #region Security
        private ESecurity security;
        protected virtual ESecurity Security
        {
            get
            {
                if (security == null)
                    security = db.SqlQuery<ESecurity>("[dbo].[PrPermission] @controllerName, @login", new SqlParameter("controllerName", this.GetType().Name), new SqlParameter("login", HttpContext.User.Identity.Name)).FirstOrDefault();
                return security;
            }            
        }
        #endregion security
        
        #region VirtualMethods
        protected virtual void PreCreate()
        {
        }

        protected virtual void PreCreate(E model)
        {
        }
        #endregion Virtualmethods

        #region ActionMethods
        [ReeAuthorizeAttribute]
        public ActionResult Index()
        {
            T model = new T();
            model.CurrentPage = 1;
            model.SortByName = "Id";
            model.SortByOperation = "Desc";
            return Index(model);
        }

        private string GetPartialVies(string partialView)
        {
            return string.Format("{0}/__{1}", this.ControllerContext.RouteData.Values["controller"], partialView);
        }
        private void GetData(T model, ref int? totalRecord)
        {
            if (model.CurrentPage < 1) //When there is no record
                model.CurrentPage = 1;
            model.Entities = db.SelectAll(model.Includes(), model.FilterBy, model.FilterByValue, ref totalRecord, model.SortByName, model.SortByOperation, model.NoOfRecord, model.CurrentPage);
        }

        [HttpPost]
        //[MultipleButton(Name = "action", Argument = "Index")]
        public ActionResult Index(T model)
        {
            int? totalRecord = 0;
            GetData(model, ref totalRecord);
            model.TotolRecords = totalRecord.Value;
            ViewBag.Security = Security;
            if(HttpContext.Request.IsAjaxRequest())
                return PartialView(GetPartialVies("index"));
            return View("Index", model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            PreCreate();
            if (HttpContext.Request.IsAjaxRequest())
                return PartialView(GetPartialVies("Create"));
            return View();
        }


        private void ClearModelState()
        {
            var modelStateKeys = ModelState.Keys;
            var formKeys = Request.Form.Keys.Cast<string>();
            var allKeys = modelStateKeys.Concat(formKeys).Distinct().ToList();

            var culture = CultureInfo.CurrentUICulture;

            foreach (var key in allKeys)
            {
                if (ModelState[key] != null)
                    ModelState[key].Errors.Clear();// = new ModelState { Value = new ValueProviderResult(null, null, culture) };
            }
        }

        [HttpPost]
        [EncryptedActionAttribute]
        public ActionResult Create(E model)
        {            
            try
            {
                PreCreate(model);
                db.Insert(model);                
                if (HttpContext.Request.IsAjaxRequest())
                {
                    if (ModelState.ContainsKey("{key}"))
                        ModelState["{key}"].Errors.Clear();

                    ModelState.AddModelError("saved", "Success");
                    PreCreate();
                    return PartialView(GetPartialVies("Create"));
                }
                return RedirectToAction("");
            }            
            catch (DbEntityValidationException ex)
            {
                ClearModelState(); 
                foreach (DbEntityValidationResult result in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError error in result.ValidationErrors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
            }
            PreCreate();
            return View();
        }

        [HttpGet]
        [EncryptedActionAttribute]
        public ActionResult Delete(int iD)
        {
            try
            {
                db.Delete(iD);
            }
            catch
            {
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [EncryptedActionAttribute]
        public ActionResult Delete(int iD, FormCollection collection)
        {
            try
            {
                db.Delete(iD);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("someThingWentWrong", "Error");
            }
            return View();
        }

        [HttpGet]
        [EncryptedActionAttribute]
        public ActionResult Edit(int iD)
        {
            try
            {
                PreCreate();                
                if (HttpContext.Request.IsAjaxRequest())
                    return PartialView(GetPartialVies("Edit"), db.GetByID(iD, new T().Includes()));

                return View(db.GetByID(iD, new T().Includes()));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [EncryptedActionAttribute]
        public ActionResult Edit(int iD, FormCollection collection)
        {
            try
            {
                E entity = new E();
                UpdateModel(entity);
                db.Update(entity);
                PreCreate(entity);                
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException ex)
            {
                ClearModelState();
                foreach (DbEntityValidationResult result in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError error in result.ValidationErrors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
            } 
            catch          
            {
            }
            return View();
        }

        [HttpGet]
        [EncryptedActionAttribute]
        public FileStreamResult Print(int iD)
        {
            T model = new T();
            model.Entities = db.GetByIDAsList(iD);

            return PreparePrint(model);
        }

        [HttpPost]
        //[MultipleButton(Name = "action",Argument="Print")]
        public FileStreamResult Print(T model)
        {
            int? totalRecords = null;
            GetData(model, ref totalRecords);            
            return PreparePrint(model);
        }

        [HttpGet]
        public FileStreamResult PrintAll()
        {
            T model = new T();

            model.Entities = db.SelectAll(model.Includes());
            return PreparePrint(model);
        }

        private FileStreamResult PreparePrint(T model)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;
            
            LocalReport report = new LocalReport();
            report.ReportPath = "Reports/OneItem.rdlc";
            report.DataSources.Add(new ReportDataSource("DsOneItem", model.Entities));

            byte[] bytes = report.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

            return new FileStreamResult(new MemoryStream(bytes), "application/pdf");
        }

        #endregion Actionmethods
    }
}
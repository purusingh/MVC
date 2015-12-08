using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using ReeDirectoryEntityFm.Entities.Security;
using ReeDirectory.Models;

namespace ReeDirectory.Controllers
{
    public class RoleController : BaseController<Role,ERole>
    {

        private void AddRights(ERole model, string param)
        {
            string rights = Request.Params[param];
            if (rights != null)
            {
                string[] arrRights = rights.Split(new char[] { ',' });

                foreach (string str in arrRights)
                {
                    int controllId = Convert.ToInt32(str);
                    ERoleController roleController = model.RoleControllers.Find(ent => ent.Controller_Id == controllId);
                    PropertyInfo property = roleController.GetType().GetProperty(param);
                    property.SetValue(roleController, 1);
                }
            }
        }
        protected override void PreCreate()
        {
            ViewBag.Controllers = db.SelectAll();
        }

        protected override void PreCreate(ERole model)
        {
            db.ExecuteSqlCommand("delete from ERoleController where Role_Id = @RoleId", new SqlParameter("RoleId",model.Id));
            List<EController> controllers = db.GetExternalDbSet<EController>().ToList();
            
            foreach(EController controller in controllers)
            {
                model.RoleControllers.Add(new ERoleController{ Controller_Id= controller.Id});
            }

            AddRights(model, "Add");
            AddRights(model,"Edit");
            AddRights(model, "Delete");
            AddRights(model, "Print");
        }
    }
}

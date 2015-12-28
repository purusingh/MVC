using System;
using System.Web.Mvc;

namespace ReeDirectory.ActionFilters
{
    public class AjaxActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                //if(f)
                var result = filterContext.Result as ViewResultBase;
                if (result != null && result.Model != null && String.IsNullOrEmpty(result.ViewName))
                {
                    filterContext.Result = new JsonResult
                    {
                        Data = result.Model,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }
        }
    }
}
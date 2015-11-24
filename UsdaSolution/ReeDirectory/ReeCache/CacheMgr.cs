using System.Web;
using System.Web.Configuration;

namespace ReeDirectory.ReeCache
{
    public static class CacheMgr
    {
        public static string GetEncKey()
        {
            string key = "getEncKey";
            if (HttpContext.Current.Cache[key] == null)
                HttpContext.Current.Cache[key] = WebConfigurationManager.AppSettings["getEncKey"].ToString();
            return HttpContext.Current.Cache[key].ToString();
        }
    }
}
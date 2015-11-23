using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ReeDirectory.EncyDyc
{
    public static class EncryptedActionLink
    {

        public static MvcHtmlString EncodedActionLink(this HtmlHelper htmlHelper, string linkText,string actionName, object routeValues, object htmlAttributes)
        {
            return EncodedActionLink(htmlHelper, linkText, actionName, "", routeValues, htmlAttributes);
        }

        public static MvcHtmlString EncodedActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            string queryString = string.Empty;
            string htmlAttributesString = string.Empty;
            if (routeValues != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(routeValues);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    if (i > 0)
                    {
                        queryString += "?";
                    }
                    queryString += d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }

            if (htmlAttributes != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(htmlAttributes);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    htmlAttributesString += " " + d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }

            //<a href="/Answer?questionId=14">What is Entity Framework??</a>
            StringBuilder ancor = new StringBuilder();
            ancor.Append("<a ");
            if (htmlAttributesString != string.Empty)
            {
                ancor.Append(htmlAttributesString);
            }
            ancor.Append(" href='");
            if (string.IsNullOrEmpty(controllerName))
            {
                controllerName = htmlHelper.ViewContext.RouteData.GetRequiredString("Controller");                
            }
            ancor.Append("/" + controllerName);

            if (actionName != "Index")
            {
                ancor.Append("/" + actionName);
            }
            if (queryString != string.Empty)
            {
                ancor.Append("?q=" + EncryptDecrypt.Encrypt(queryString));
            }
            ancor.Append("'");
            ancor.Append(">");
            ancor.Append(linkText);
            ancor.Append("</a>");
            return new MvcHtmlString(ancor.ToString());
        }

        

       
    }
}
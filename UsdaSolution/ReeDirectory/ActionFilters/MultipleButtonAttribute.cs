using System.Web.Mvc;

namespace ReeDirectory.ActionFilters
{
    public class MultipleButtonAttribute : ActionNameSelectorAttribute
    {
        public string Name { get; set; }
        public string Argument { get; set; }
        public override bool IsValidName(ControllerContext controllerContext, string actionName, System.Reflection.MethodInfo methodInfo)
        {
            var isValidName = false;
            var keyValue = Name;
            if (!string.IsNullOrEmpty(Argument))
                keyValue = string.Format("{0}:{1}", Name, Argument);

            var value = controllerContext.Controller.ValueProvider.GetValue(keyValue);

            if (value != null)
            {
                controllerContext.Controller.ControllerContext.RouteData.Values[Name] = string.IsNullOrEmpty(Argument) ? Name : Argument;
                isValidName = true;
            }

            return isValidName;

        }
    }
}
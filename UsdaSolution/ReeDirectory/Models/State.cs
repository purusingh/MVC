using System.Collections.Generic;
using System.Web.Mvc;
using ReeDirectoryEntityFm.Entities.General;
using ReeDirectory.Models.Filter;


namespace ReeDirectory.Models
{
    public class State : Base<EState>
    {

        override public SelectList FilterColumns
        {
            get
            {
                FilterItems items = new FilterItems();
                items.Add(new FilterItem { Name = "Id", EntityPath = "Id" });
                items.Add(new FilterItem { Name = "Name", EntityPath = "Name" });
                items.Add(new FilterItem { Name = "Country", EntityPath = "Country.Name" });

                return new SelectList(items, "EntityPath", "Name", FilterBy);
            }
        }

        public override System.Collections.Generic.List<string> Includes()
        {
            List<string> strings =  base.Includes();
            strings.Add("Country");
            return strings;
        }
        
    }
}
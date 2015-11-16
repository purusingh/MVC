using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ReeDirectory.EntityFM.Entities;

namespace ReeDirectory.Models
{
    public abstract class Base<T> where T: EBase
    {

        public List<T> Entities { get; set; }

        public int CurrentPage { get; set; }
        public int TotolRecords { get; set; }

        public string SortByName { get; set; }
        public string SortByOperation { get; set; }

        public string FilterBy { get; set; }
        public string FilterByValue { get; set; }
        virtual public SelectList Filtercolumns {
            get
            {
                PropertyInfo[] properties = typeof(T).GetProperties(0);
                return new SelectList(properties, "Name", "Name", FilterBy);
            }
        }

        public SelectList Pages
        {
            get
            {
                List<int> ints = new List<int>();
                double totalPages = (TotolRecords/10) +( (TotolRecords%10)>0? 1:0);
                for (int i = 1; i <= totalPages; i++)
                {
                    ints.Add(i);
                }
                return new SelectList(ints, CurrentPage);
            }
        }
    }
}
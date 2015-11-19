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
        public int NoOfRecord
        {
            get
            {
                return 10;
            }
        }
        public List<T> Entities { get; set; }

        public int CurrentPage { get; set; }
        public int TotolRecords { get; set; }

        public string SortByName { get; set; }
        public string SortByOperation { get; set; }

        public string FilterBy { get; set; }
        public string FilterByValue { get; set; }

        virtual public SelectList FilterColumns {
            get
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                return new SelectList(properties, "Name", "Name", FilterBy);
            }
        }

        public SelectList Pages
        {
            get
            {
                List<int> ints = new List<int>();
                double totalPages = (TotolRecords / NoOfRecord) + ((TotolRecords % NoOfRecord) > 0 ? 1 : 0);
                for (int i = 1; i <= totalPages; i++)
                {
                    ints.Add(i);
                }
                return new SelectList(ints, CurrentPage);
            }
        }
    }
}
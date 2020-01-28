using System;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.Web.ViewModels
{
    public class ListViewModel
    {
        public IEnumerable<dynamic> Items { get; set; }
        public Func<dynamic, object> ItemTemplate {get; set;}
        public string ListClass {get; set;} = null;
        public string ListItemClass {get; set;} = null;
        public bool Unordered {get; set; } = false;
    }
}
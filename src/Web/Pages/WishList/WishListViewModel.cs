using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.eShopWeb.Web.Pages.WishList
{
    public class WishListViewModel
    {
        public int Id { get; set; }
        public List<WishListItemViewModel> Items { get; set; } = new List<WishListItemViewModel>();
        public string BuyerId { get; set; }


    }
} 
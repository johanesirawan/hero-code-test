using SimpleBookingWidget.Core.Models;
using System.Collections.Generic;

namespace SimpleBookingWidget.Core.ViewModels
{
    public class SearchProductViewModel
    {
        public SearchProductViewModel()
        {
            Products = new List<SearchProductModel>();
        }

        public SearchProductParameter Parameters { get; set; }
        public ICollection<SearchProductModel> Products { get; set; }
    }
}

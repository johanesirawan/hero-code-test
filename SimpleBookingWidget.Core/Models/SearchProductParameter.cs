using SimpleBookingWidget.Commons;

namespace SimpleBookingWidget.Core.Models
{
    public class SearchProductParameter
    {
        public SearchProductParameter()
        {
            Lat = 1;
            Lng = 1;
        }
        public string Q { get; set; }
        public ProductCategory? Cat { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public double? Rad { get; set; }
    }
}

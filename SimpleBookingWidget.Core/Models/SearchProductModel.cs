using SimpleBookingWidget.Commons;

namespace SimpleBookingWidget.Core.Models
{
    public class SearchProductModel
    {
        public long Id { get; set; }
        public double Score { get; set; }
        public string Name { get; set; }
        public int NumberOfNights { get; set; }
        public string FormattedAddressName { get; set; }
        public string ShortDescription { get; set; }
        public string SupplierName { get; set; }
        public string BranchName { get; set; }
        public ProductCategory Category { get; set; }
    }
}

using System.Collections.Generic;

namespace SimpleBookingWidget.Core.Models
{
    public class ProductModel
    {
        public double SearchTime { get; set; }
        public int NumberResults { get; set; }
        public ICollection<ProductDetailModel> Products { get; set; }
    }

    public class ProductDetailModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }
        public string SupplierName { get; set; }
        public string SupplierBranchName { get; set; }
        public ICollection<ProductImageModel> ImageUrls { get; set; }
    }

    public class ProductImageModel
    {
        public int Type { get; set; }
        public string Url { get; set; }
    }
}

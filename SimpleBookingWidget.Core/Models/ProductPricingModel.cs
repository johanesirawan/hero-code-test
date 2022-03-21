using System;

namespace SimpleBookingWidget.Core.Models
{
    public class ProductPricingModel
    {
        public long ProductId { get; set; }
        public int Availbility { get; set; }
        public DateTime DateCheckIn { get; set; }
        public double AdvertisedPrice { get; set; }
        public double Rrp { get; set; }
        public double Levy { get; set; }
        public double Commission { get; set; }
        public string CurrencyIso { get; set; }
        public double TotalAdvertisedPrice { get; set; }
        public double TotalRrp { get; set; }
        public double TotalRrpBeforeDiscount { get; set; }
        public double TotalLevy { get; set; }
        public double Discount => Commission * 0.5;
        public double DiscountedPrice => TotalAdvertisedPrice - Discount;
    }
}

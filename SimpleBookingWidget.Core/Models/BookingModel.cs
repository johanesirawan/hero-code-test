using SimpleBookingWidget.Commons;
using System.Collections.Generic;

namespace SimpleBookingWidget.Core.Models
{
    public class BookingModel
    {
        public BookingModel()
        {
            BookingProducts = new List<BookingProductModel>();
        }

        public string Id { get; set; }
        public string Created { get; set; }
        public double Rrp { get; set; }
        public double Paid { get; set; }
        public double Payable { get; set; }
        public double Commission { get; set; }
        public double Adjustment { get; set; }
        public BookingStatus Status { get; set; }
        public ICollection<BookingProductModel> BookingProducts { get; set; }
        public double Price => Rrp - Adjustment;
        public double Discount => Commission * 0.5;
        public double DiscountedPrice => Price - Discount;
        public double PayableAfterDiscount => Payable - Discount;
        public bool PaidOff => Payable <= 0;
    }
}

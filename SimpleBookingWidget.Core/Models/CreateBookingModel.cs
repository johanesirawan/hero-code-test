using System;
using System.Collections.Generic;

namespace SimpleBookingWidget.Core.Models
{
    public class CreateBookingModel
    {
        public string BookingName { get; set; }

        public ICollection<BookingProductModel> BookingProducts { get; set; }
    }

    public class BookingProductModel
    {
        public long ProductId { get; set; }
        public List<string> PaxId { get; set; }
        public DateTime DateCheckIn { get; set; }
    }
}

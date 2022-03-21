using SimpleBookingWidget.Commons;

namespace SimpleBookingWidget.Core.Models
{
    public class CreatePaymentModel
    {
        public string ReceiptNumber { get; set; }
        public string BookingId { get; set; }
        public string PaxId { get; set; }
        public double Amount { get; set; }
        public PaymentMethod? Method { get; set; }
        public bool IsFinal { get; set; }
    }
}

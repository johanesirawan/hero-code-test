using SimpleBookingWidget.Commons;

namespace SimpleBookingWidget.Core.Models
{
    public class PaxVoucher
    {
        public VoucherStatus Status { get; set; }
        public string QrData { get; set; }
        public string VoucherUrl { get; set; }
    }
}

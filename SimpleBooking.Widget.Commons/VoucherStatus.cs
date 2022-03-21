using System.ComponentModel;

namespace SimpleBookingWidget.Commons
{
    public enum VoucherStatus
    {
        [Description("Draft")]
        Draft = 1,
        [Description("Active")]
        Active = 2,
        [Description("Void")]
        Void = 3,
        [Description("Claimed")]
        Claimed = 4,
        [Description("Claimed and Paid")]
        ClaimedAndPaid = 5,
        [Description("Claimed and Reversed")]
        ClaimedAndReversed = 6,
        [Description("Expired")]
        Expired = 7,
    }
}

using System.ComponentModel;

namespace SimpleBookingWidget.Commons
{
    public enum BookingStatus
    {
        [Description("Draft Booking")]
        Draft = 1,
        [Description("Locked Booking")]
        Locked = 2,
        [Description("Abandoned Booking")]
        Abandoned = 3,
        [Description("Cancelled Booking")]
        Cancelled = 4,
        [Description("Pending Finalization")]
        Pending = 5,
        [Description("Finalised Finalization")]
        Finalised = 6,
        [Description("Suspended Finalization")]
        Suspended = 7,
        [Description("Deleted Finalization")]
        Deleted = 8,
    }
}

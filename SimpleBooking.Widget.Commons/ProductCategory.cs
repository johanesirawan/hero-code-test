using System.ComponentModel;

namespace SimpleBookingWidget.Commons
{
    public enum ProductCategory
    {
        [Description("Tours and Activities")]
        ToursAndActivities = 0,
        [Description("Accomodation")]
        Accomodation = 1,
        [Description("Bus and Rail")]
        BusAndRail = 2,
        [Description("Self Drive Hire")]
        SelfDriveHire = 3,
    }
}

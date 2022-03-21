namespace SimpleBookingWidget.Sessions
{
    public interface IMySession
    {
        bool Authenticated { get; }
        bool HasBooking { get; }
        string PaxId { get; }
        string FirstName { get; }
        string LastName { get; }
        string BookingId { get; }
    }
}

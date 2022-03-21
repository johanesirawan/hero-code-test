namespace SimpleBookingWidget.Sessions
{
    public interface IBookingSession
    {
        void SetBookingId(string bookingId);
        void Clear();
    }
}
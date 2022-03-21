namespace SimpleBookingWidget.Sessions
{
    public interface IPaxSession
    {
        void SetSessionsPax(string id, string first, string last);
        void Clear();
    }
}
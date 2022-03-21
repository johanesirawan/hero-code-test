using Microsoft.AspNetCore.Http;
using System.Text;

namespace SimpleBookingWidget.Sessions
{
    public class MySession : IMySession, IPaxSession, IBookingSession
    {
        private static readonly string PAX_ID = "_PaxId";
        private static readonly string PAX_FIRST = "_First";
        private static readonly string PAX_LAST = "_Last";
        private static readonly string BOOKING_ID = "_BookingId";

        private readonly IHttpContextAccessor context;

        public MySession(IHttpContextAccessor context)
        {
            this.context = context;
        }

        public bool Authenticated => !string.IsNullOrEmpty(PaxId);
        public bool HasBooking => !string.IsNullOrEmpty(BookingId);
        public string PaxId => context.HttpContext.Session.GetString(PAX_ID);
        public string FirstName => context.HttpContext.Session.GetString(PAX_FIRST);
        public string LastName => context.HttpContext.Session.GetString(PAX_LAST);
        public string BookingId => context.HttpContext.Session.GetString(BOOKING_ID);

        public void SetSessionsPax(string id, string first, string last)
        {
            context.HttpContext.Session.SetString(PAX_ID, id);
            context.HttpContext.Session.SetString(PAX_FIRST, first);
            context.HttpContext.Session.SetString(PAX_LAST, last);
        }

        public void SetBookingId(string bookingId)
        {
            context.HttpContext.Session.SetString(BOOKING_ID, bookingId);
        }

        public void Clear()
        {
            context.HttpContext.Session.Clear();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using SimpleBookingWidget.Sessions;

namespace SimpleBookingWidget.Controllers.Api
{
    public abstract class BaseApiController : ControllerBase
    {
        protected IMySession Session;

        protected BaseApiController(IMySession session)
        {
            Session = session;
        }
    }
}

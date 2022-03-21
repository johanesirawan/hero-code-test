using Microsoft.AspNetCore.Mvc;
using SimpleBookingWidget.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBookingWidget.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IMySession Session;

        protected BaseController(IMySession session)
        {
            Session = session;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleBookingWidget.Core.ViewModels;
using SimpleBookingWidget.Models;
using SimpleBookingWidget.Services;
using SimpleBookingWidget.Sessions;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SimpleBookingWidget.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookingService _bookingService;

        public HomeController(IMySession session,
            ILogger<HomeController> logger,
            IBookingService productService) : base(session)
        {
            _logger = logger;
            _bookingService = productService;
        }

        public IActionResult Index()
        {
            if (!Session.Authenticated)
                return RedirectToAction("Index", "Pax");
            var model = new SearchProductViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchProductViewModel model)
        {
            if (!Session.Authenticated)
                return RedirectToAction("Index", "Pax");

            model.Products = await _bookingService.SearchProduct(model.Parameters);

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleBookingWidget.Core.Models;
using SimpleBookingWidget.Services;
using SimpleBookingWidget.Sessions;
using System;
using System.Threading.Tasks;

namespace SimpleBookingWidget.Controllers
{
    public class PaxController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPaxService _paxService;

        public PaxController(IMySession session,
            ILogger<HomeController> logger,
            IPaxService paxService) : base(session)
        {
            _logger = logger;
            _paxService = paxService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PaxModel model)
        {
            try
            {
                var result = await _paxService.CreatePax(model);
                return RedirectToAction("Index", "Home");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Something wrong, please try again later.");
            }
        }

        public IActionResult Logout()
        {
            try
            {
                _paxService.ClearPaxSession();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Something wrong, please try again later.");
            }
        }
    }
}

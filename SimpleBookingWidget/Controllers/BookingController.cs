using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleBookingWidget.Core.Models;
using SimpleBookingWidget.Core.ViewModels;
using SimpleBookingWidget.Models;
using SimpleBookingWidget.Services;
using SimpleBookingWidget.Sessions;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SimpleBookingWidget.Controllers
{
    public class BookingController : BaseController
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingService _bookingService;

        public BookingController(IMySession session,
            ILogger<BookingController> logger,
            IBookingService productService) : base(session)
        {
            _logger = logger;
            _bookingService = productService;
        }

        public async Task<IActionResult> Schedule(long id)
        {
            if (!Session.Authenticated)
                return RedirectToAction("Index", "Pax");

            var model = new ScheduleViewModel
            {
                ProductId = id,
                DateStart = DateTime.Now.Date.AddDays(1),
                DateEnd = DateTime.Now.Date.AddDays(7)
            };

            try
            {
                model.Product = await _bookingService.GetProduct(id);
                return View(model);
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

        [HttpPost]
        public async Task<IActionResult> Schedule(ScheduleViewModel model)
        {
            if (!Session.Authenticated)
                return RedirectToAction("Index", "Pax");

            try
            {
                var results = await _bookingService.CheckSchedule(model.ProductId, model.DateStart, model.DateEnd);
                return PartialView("Schedule_Results", results);
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

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingViewModel model)
        {
            if (!Session.Authenticated)
                return RedirectToAction("Index", "Pax");

            try
            {
                var results = await _bookingService.CreateBooking(model.ProductId, model.DateCheckIn, Session.PaxId, Session.BookingId);
                return Ok(results);
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

        [HttpPost]
        public async Task<IActionResult> Pricing(CreateBookingViewModel model)
        {
            if (!Session.Authenticated)
                return RedirectToAction("Index", "Pax");

            try
            {
                var results = await _bookingService.GetProductPricing(model.ProductId, model.DateCheckIn, Session.PaxId);
                return PartialView("Pricing", results);
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

        public async Task<IActionResult> Cart()
        {
            if (!Session.Authenticated)
                return RedirectToAction("Index", "Pax");

            try
            {
                if (!Session.HasBooking)
                    throw new ArgumentException("No booking has made.");

                var model = await _bookingService.GetCart(Session.BookingId);
                return View(model);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return View(new BookingModel());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Something wrong, please try again later.");
            }
        }

        public async Task<IActionResult> Pay()
        {
            if (!Session.HasBooking)
                throw new ArgumentException("No booking has made.");
            var model = new CreatePaymentModel();
            try
            {
                var cart = await _bookingService.GetCart(Session.BookingId);
                model.Amount = cart.PayableAfterDiscount;
                return PartialView("Payment", model);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return PartialView("Payment", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Something wrong, please try again later.");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleBookingWidget.Core.Models;
using SimpleBookingWidget.Models;
using SimpleBookingWidget.Services;
using SimpleBookingWidget.Sessions;
using System;
using System.Threading.Tasks;

namespace SimpleBookingWidget.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingApiController : BaseApiController
    {
        private readonly ILogger<BookingApiController> _logger;
        private readonly IBookingService _bookingService;

        public BookingApiController(IMySession session,
            ILogger<BookingApiController> logger,
            IBookingService bookingService) : base(session)
        {
            _logger = logger;
            _bookingService = bookingService;
        }

        [HttpGet]
        [Route("bookingcount")]
        public async Task<ApiResponse<int>> Bookingcount()
        {
            if (!Session.HasBooking)
                throw new ArgumentException("No booking has made.");

            try
            {
                var model = await _bookingService.GetCart(Session.BookingId);
                return new ApiResponse<int>(model.BookingProducts.Count);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return new ApiResponse<int>(ex.Message, ApiResponseStatus.Error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<int>("Something wrong, please try again later.", ApiResponseStatus.Error);
            }
        }

        [HttpGet]
        [Route("bookingvalidate")]
        public async Task<ApiResponse<ValidateBookingModel>> ValidateBooking()
        {
            try
            {
                if (!Session.HasBooking)
                    throw new ArgumentException("No booking has made.");

                var result = await _bookingService.ValidateBooking(Session.BookingId);

                return new ApiResponse<ValidateBookingModel>(result);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return new ApiResponse<ValidateBookingModel>(ex.Message, ApiResponseStatus.Error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<ValidateBookingModel>("Something wrong, please try again later.", ApiResponseStatus.Error);
            }
        }

        [HttpPost]
        [Route("payments")]
        public async Task<ApiResponse<CreatePaymentModel>> Payments(CreatePaymentModel model)
        {
            try
            {
                if (!Session.HasBooking)
                    throw new ArgumentException("No booking has made.");

                if(model.Amount == 0 || !model.Method.HasValue)
                    throw new ArgumentException("Please complete the form.");

                model.BookingId = Session.BookingId;
                model.IsFinal = true;
                model.PaxId = Session.PaxId;

                var result = await _bookingService.CreatePayment(model);

                return new ApiResponse<CreatePaymentModel>(result);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return new ApiResponse<CreatePaymentModel>(ex.Message, ApiResponseStatus.Error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<CreatePaymentModel>("Something wrong, please try again later.", ApiResponseStatus.Error);
            }
        }

        [HttpGet]
        [Route("vouchers")]
        public async Task<ApiResponse<PaxVoucher>> Vouchers()
        {
            try
            {
                if (!Session.HasBooking)
                    throw new ArgumentException("No booking has made.");

                var voucher = await _bookingService.GetVoucher(Session.BookingId, Session.PaxId);

                return new ApiResponse<PaxVoucher>(voucher);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return new ApiResponse<PaxVoucher>(ex.Message, ApiResponseStatus.Error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<PaxVoucher>("Something wrong, please try again later.", ApiResponseStatus.Error);
            }
        }

    }
}

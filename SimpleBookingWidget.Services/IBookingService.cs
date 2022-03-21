using SimpleBookingWidget.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleBookingWidget.Services
{
    public interface IBookingService
    {
        Task<List<SearchProductModel>> SearchProduct(SearchProductParameter searchParam);
        Task<ProductDetailModel> GetProduct(long productId);
        Task<List<ScheduleModel>> CheckSchedule(long productId, DateTime dateStart, DateTime? dateEnd);
        Task<BookingModel> CreateBooking(long productId, DateTime dateCheckIn, string paxId, string bookingId = null);
        Task<ProductPricingModel> GetProductPricing(long productId, DateTime dateCheckIn, string paxId);
        Task<BookingModel> GetCart(string bookingId);
        Task<ValidateBookingModel> ValidateBooking(string bookingId);
        Task<CreatePaymentModel> CreatePayment(CreatePaymentModel model);
        Task<PaxVoucher> GetVoucher(string bookingId, string paxId);
    }
}
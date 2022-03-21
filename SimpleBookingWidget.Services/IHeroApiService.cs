using SimpleBookingWidget.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleBookingWidget.Services
{
    public interface IHeroApiService
    {
        Task<PaxModel> CreatePax(PaxModel model);
        Task<List<SearchProductModel>> SearchProduct(SearchProductParameter searchParam);
        Task<List<ScheduleModel>> CheckSchedule(long productId, DateTime dateStart, DateTime? dateEnd);
        Task<ProductModel> GetProduct(long productId);
        Task<BookingModel> CreateBooking(CreateBookingModel model);
        Task<ProductPricingModel> CalculateProductPricing(long productId, DateTime dateCheckIn, string paxId);
        Task<BookingModel> GetBooking(string bookingId);
        Task<ValidateBookingModel> ValidateBooking(string bookingId);
        Task<CreatePaymentModel> CreatePayment(CreatePaymentModel model);
        Task<string> FinaliseBooking(string bookingId);
        Task<PaxVoucher> GetVoucher(string bookingId, string paxId);
    }
}

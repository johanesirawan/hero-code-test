using SimpleBookingWidget.Core.Models;
using SimpleBookingWidget.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBookingWidget.Services
{
    public class BookingService : IBookingService
    {
        private readonly IHeroApiService _heroApi;
        private readonly IBookingSession _session;

        public BookingService(IHeroApiService heroApi, IBookingSession session)
        {
            _heroApi = heroApi;
            _session = session;
        }

        public async Task<List<SearchProductModel>> SearchProduct(SearchProductParameter searchParam)
        {
            return await _heroApi.SearchProduct(searchParam);
        }

        public async Task<ProductDetailModel> GetProduct(long productId)
        {
            var product = await _heroApi.GetProduct(productId);

            if (product.NumberResults == 0)
                throw new ArgumentException($"Product detail with id: {productId} not found.");

            var result = product.Products.FirstOrDefault();
            result.ImageUrls = result.ImageUrls.Where(_ => _.Type == 0).ToList();

            return result;
        }

        public async Task<List<ScheduleModel>> CheckSchedule(long productId, DateTime dateStart, DateTime? dateEnd)
        {
            return await _heroApi.CheckSchedule(productId, dateStart, dateEnd);
        }

        public async Task<BookingModel> CreateBooking(long productId, DateTime dateCheckIn, string paxId, string bookingId = null)
        {
            var model = new CreateBookingModel
            {
                BookingName = $"booking_{dateCheckIn.ToString("yyyymmddHHmmss")}",
                BookingProducts = new List<BookingProductModel>
                {
                    new BookingProductModel
                    {
                        DateCheckIn = dateCheckIn,
                        PaxId = new List<string>{ paxId },
                        ProductId = productId
                    }
                }
            };

            if (!string.IsNullOrEmpty(bookingId))
            {
                var cart = await _heroApi.GetBooking(bookingId);
                foreach (var product in cart.BookingProducts)
                {
                    model.BookingProducts.Add(new BookingProductModel
                    {
                        DateCheckIn = product.DateCheckIn,
                        PaxId = product.PaxId,
                        ProductId = product.ProductId
                    });
                }
            }

            var result = await _heroApi.CreateBooking(model);
            _session.SetBookingId(result.Id);
            return result;
        }

        public async Task<ProductPricingModel> GetProductPricing(long productId, DateTime dateCheckIn, string paxId)
        {
            return await _heroApi.CalculateProductPricing(productId, dateCheckIn, paxId);
        }

        public async Task<BookingModel> GetCart(string bookingId)
        {
            return await _heroApi.GetBooking(bookingId);
        }

        public async Task<ValidateBookingModel> ValidateBooking(string bookingId)
        {
            return await _heroApi.ValidateBooking(bookingId);
        }

        public async Task<CreatePaymentModel> CreatePayment(CreatePaymentModel model)
        {
            var result = await _heroApi.CreatePayment(model);
            var finalise = await Finalise(model.BookingId);
            return result;
        }

        private async Task<string> Finalise(string bookingId)
        {
            return await _heroApi.FinaliseBooking(bookingId);
        }

        public async Task<PaxVoucher> GetVoucher(string bookingId, string paxId)
        {
            return await _heroApi.GetVoucher(bookingId, paxId);
        }
    }
}

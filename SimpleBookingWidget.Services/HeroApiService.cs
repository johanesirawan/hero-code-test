using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using SimpleBookingWidget.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SimpleBookingWidget.Services
{
    public class HeroApiService : IHeroApiService
    {
        private readonly IConfiguration _configuration;
        protected readonly string BaseUrl;
        protected readonly string ApiKey;
        public HeroApiService(IConfiguration configuration)
        {
            _configuration = configuration;
            BaseUrl = _configuration.GetSection("HeroApiSettings")?.GetSection("BaseUrl")?.Value;
            ApiKey = _configuration.GetSection("HeroApiSettings")?.GetSection("APIKey")?.Value;

        }
        private RestRequest GenerateHttpRequest(string resource, Method method)
        {
            var httpRequest = new RestRequest(resource, method);
            httpRequest.AddHeader("apiKey", ApiKey);
            httpRequest.AddHeader("Content-Type", "application/json");
            return httpRequest;
        }

        private async Task<RestResponse> Execute(string action, Method method, object body = null, Dictionary<string, string> @params = null)
        {
            var client = new RestClient(BaseUrl);
            var request = GenerateHttpRequest(action, method);

            if (body != null)
                request.AddJsonBody(body);

            if (@params != null)
                foreach (var param in @params)
                {
                    if (param.Value == null) continue;
                    request.AddParameter(param.Key, param.Value);
                }

            var result = await client.ExecuteAsync(request);

            if (!result.IsSuccessful)
                throw result.ErrorException ?? new Exception(result.ErrorMessage);

            return result;
        }

        private async Task<RestResponse> Execute(string action, Method method, Dictionary<string, string> @params)
        {
            var client = new RestClient(BaseUrl);
            var request = GenerateHttpRequest(action, method);
            foreach (var param in @params)
            {
                if (param.Value == null) continue;
                request.AddParameter(param.Key, param.Value);
            }
            var result = await client.ExecuteAsync(request);

            if (!result.IsSuccessful)
                throw result.ErrorException ?? new Exception(result.ErrorMessage);

            return result;
        }

        public async Task<PaxModel> CreatePax(PaxModel model)
        {
            var body = new
            {
                first = model.First,
                last = model.Last,
                email = model.Email,
                mobile = model.Mobile,
                age = model.Age,
                notes = model.Notes,
                gender = (int?)model.Gender,
                address = model.Address,
                country = model.Country
            };
            var response = await Execute("pax", Method.Post, body);

            if (!(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created))
                throw new Exception(response.ErrorMessage);

            var result = JsonConvert.DeserializeObject<PaxModel>(response.Content);

            if (result == null)
                throw new Exception($"Failed to dezerialize response from Hero Api: Create Pax");

            return result;
        }

        public async Task<List<SearchProductModel>> SearchProduct(SearchProductParameter searchParam)
        {
            var @params = new Dictionary<string, string>();
            @params.Add("q", searchParam.Q);
            @params.Add("cat", searchParam.Cat?.ToString());
            @params.Add("lat", searchParam.Lat.ToString());
            @params.Add("lng", searchParam.Lng.ToString());
            @params.Add("rad", searchParam.Rad?.ToString() ?? null);
            var response = await Execute("search", Method.Get, @params);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.ErrorMessage);

            var result = JsonConvert.DeserializeObject<List<SearchProductModel>>(response.Content);

            if (result == null)
                throw new Exception($"Failed to dezerialize response from Hero Api: Search Product");

            return result;
        }

        public async Task<List<ScheduleModel>> CheckSchedule(long productId, DateTime dateStart, DateTime? dateEnd)
        {
            var url = $"schedule/{productId}/{dateStart:yyyy-MM-dd}";
            if (dateEnd.HasValue)
                url += $"/{dateEnd:yyy-MM-dd}";
            var response = await Execute(url, Method.Get);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.ErrorMessage);

            var result = JsonConvert.DeserializeObject<List<ScheduleModel>>(response.Content);

            if (result == null)
                throw new Exception($"Failed to dezerialize response from Hero Api: Check Schedule");

            return result;
        }

        public async Task<ProductModel> GetProduct(long productId)
        {
            var response = await Execute($"products/{productId}", Method.Get);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.ErrorMessage);

            var result = JsonConvert.DeserializeObject<ProductModel>(response.Content);

            if (result == null)
                throw new Exception($"Failed to dezerialize response from Hero Api: Get Product");

            return result;
        }

        public async Task<BookingModel> CreateBooking(CreateBookingModel model)
        {
            var body = new
            {
                model.BookingName,
                BookingProducts = model.BookingProducts.Select(_ => new
                {
                    DateCheckIn = _.DateCheckIn.ToString("yyyy-MM-ddTHH:mm:ss"),
                    _.ProductId,
                    _.PaxId
                })
            };
            var response = await Execute("bookings", Method.Post, body);

            if (!(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created))
                throw new Exception(response.ErrorMessage);

            var result = JsonConvert.DeserializeObject<BookingModel>(response.Content);

            if (result == null)
                throw new Exception($"Failed to dezerialize response from Hero Api: Create Booking");

            return result;
        }

        public async Task<ProductPricingModel> CalculateProductPricing(long productId, DateTime dateCheckIn, string paxId)
        {
            var body = new
            {
                paxId = new List<string> { paxId }
            };
            var @params = new Dictionary<string, string>();
            @params.Add("dateCheckIn", dateCheckIn.ToString("yyyy-MM-ddTHH:mm:ss"));
            var response = await Execute($"productpricing/{productId}", Method.Post, body, @params);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.ErrorMessage);

            var result = JsonConvert.DeserializeObject<ProductPricingModel>(response.Content);

            if (result == null)
                throw new Exception($"Failed to dezerialize response from Hero Api: Calculate Product Pricing");

            return result;
        }

        public async Task<BookingModel> GetBooking(string bookingId)
        {
            var response = await Execute($"bookings/{bookingId}", Method.Get);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.ErrorMessage);

            var result = JsonConvert.DeserializeObject<BookingModel>(response.Content);

            if (result == null)
                throw new Exception($"Failed to dezerialize response from Hero Api: Get Booking");

            return result;
        }

        public async Task<ValidateBookingModel> ValidateBooking(string bookingId)
        {
            var response = await Execute($"bookingvalidate/{bookingId}", Method.Post);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.ErrorMessage);

            var result = JsonConvert.DeserializeObject<ValidateBookingModel>(response.Content);

            if (result == null)
                throw new Exception($"Failed to dezerialize response from Hero Api: Validate Booking");

            return result;
        }

        public async Task<CreatePaymentModel> CreatePayment(CreatePaymentModel model)
        {
            var body = new
            {
                model.BookingId,
                model.PaxId,
                model.Amount,
                model.Method,
                model.IsFinal
            };
            var response = await Execute($"payments", Method.Post, body);

            if (!(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created))
                throw new Exception(response.ErrorMessage);

            var result = JsonConvert.DeserializeObject<CreatePaymentModel>(response.Content);

            if (result == null)
                throw new Exception($"Failed to dezerialize response from Hero Api: Create Payment");

            return result;
        }

        public async Task<string> FinaliseBooking(string bookingId)
        {
            var response = await Execute($"bookingfinalise/{bookingId}", Method.Get);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.ErrorMessage);

            var result = JsonConvert.DeserializeObject<string>(response.Content);

            if (result == null)
                throw new Exception($"Failed to dezerialize response from Hero Api: Finalise Booking");

            return result;
        }

        public async Task<PaxVoucher> GetVoucher(string bookingId, string paxId)
        {
            var response = await Execute($"vouchers/{bookingId}", Method.Get);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.ErrorMessage);

            var result = JsonConvert.DeserializeObject<PaxVoucher>(response.Content);

            if (result == null)
                throw new Exception($"Failed to dezerialize response from Hero Api: Get Voucher");

            return result;
        }
    }

}

using SimpleBookingWidget.Core.Models;
using SimpleBookingWidget.Sessions;
using System.Threading.Tasks;

namespace SimpleBookingWidget.Services
{
    public class PaxService : IPaxService
    {
        private readonly IPaxSession _session;
        private readonly IHeroApiService _heroApi;

        public PaxService(IPaxSession session, IHeroApiService heroApi)
        {
            _session = session;
            _heroApi = heroApi;
        }

        public async Task<PaxModel> CreatePax(PaxModel model)
        {
            var result = await _heroApi.CreatePax(model);
            _session.SetSessionsPax(result.Id, result.First, result.Last);
            return result;
        }

        public bool ClearPaxSession()
        {
            _session.Clear();
            return true;
        }
    }
}

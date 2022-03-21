using SimpleBookingWidget.Core.Models;
using System.Threading.Tasks;

namespace SimpleBookingWidget.Services
{
    public interface IPaxService
    {
        Task<PaxModel> CreatePax(PaxModel model);
        bool ClearPaxSession();
    }
}
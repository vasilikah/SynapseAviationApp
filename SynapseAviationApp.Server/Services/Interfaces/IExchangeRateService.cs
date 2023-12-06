using SynapseAviationApp.Server.Models;

namespace SynapseAviationApp.Server.Services.Interfaces
{
    public interface IExchangeRateService
    {
        Task<List<Kurs>> GetExcangeRates();
    }
}

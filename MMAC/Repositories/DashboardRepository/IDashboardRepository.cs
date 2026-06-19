using MMAC.Models.Cores;

namespace MMAC.Repositories.DashboardRepository
{
    public interface IDashboardRepository
    {
        Task<List<Traveller>> GetTravellersAsync(DateTime fromDate, DateTime toDate);
        Task<List<ArrivalApplication>> GetApplicationsAsync(DateTime fromDate, DateTime toDate);

        Task<List<Traveller>> GetAllTravellersAsync();
        Task<List<ArrivalApplication>> GetAllApplicationsAsync();

    }
}
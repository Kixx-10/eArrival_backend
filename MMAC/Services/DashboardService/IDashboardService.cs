using MMAC.DTOS;

namespace MMAC.Services.DashboardService
{
    public interface IDashboardService
    {
        Task<DashboardDTO> GetDashboardDataAsync(DateTime? fromDate, DateTime? toDate);

    }
}
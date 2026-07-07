using MMAC.DTOS;
using MMAC.Models.Cores;
using MMAC.Repositories.DashboardRepository;

namespace MMAC.Services.DashboardService
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _repo;

        public DashboardService(IDashboardRepository repo)
        {
            _repo = repo;
        }

        public async Task<DashboardDTO> GetDashboardDataAsync(DateTime? fromDate, DateTime? toDate)
        {
            List<Traveller> travellers;
            List<ArrivalApplication> apps;

            if (fromDate.HasValue && toDate.HasValue)
            {
                travellers = await _repo.GetTravellersAsync(fromDate.Value, toDate.Value);
                apps = await _repo.GetApplicationsAsync(fromDate.Value, toDate.Value);
            }
            else
            {
                travellers = await _repo.GetAllTravellersAsync();
                apps = await _repo.GetAllApplicationsAsync();
            }

            var dto = new DashboardDTO
            {
                Travellers = travellers,
                Applications = apps,

                // Submitted Application Counts
                SubmitedApplicationCount = apps.Count,
                SubmitedApplicationByMyanmarCount = apps.Count(a => a.Traveller?.NationalityCode == "MM"),
                SubmitedApplicationByForeignerCount = apps.Count(a => a.Traveller?.NationalityCode != "MM"),

                // Approved Application Counts
                ApprovedApplicationCount = apps.Count(a => a.AppStatus == "Approved"),
                ApprovedApplicationByMyanmarCount = apps.Count(a => a.AppStatus == "Approved" && a.Traveller?.NationalityCode == "MM"),
                ApprovedApplicationByForeignerCount = apps.Count(a => a.AppStatus == "Approved" && a.Traveller?.NationalityCode != "MM")
            };

            return dto;
        }
    }
}
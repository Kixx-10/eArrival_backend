using Microsoft.EntityFrameworkCore;
using MMAC.Data;
using MMAC.Models.Cores;

namespace MMAC.Repositories.DashboardRepository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context) 
        {
            _context = context; 
        }

        public async Task<List<Traveller>> GetTravellersAsync(DateTime fromDate, DateTime toDate)
        {
            return await _context.Traveller
                .Where(t => t.CreatedDate >= fromDate && t.CreatedDate <= toDate)
                .ToListAsync();
        }

        public async Task<List<ArrivalApplication>> GetApplicationsAsync(DateTime fromDate, DateTime toDate)
        {
            return await _context.ArrivalApplication
                .Include(a => a.Traveller)
                .Where(a => a.CreatedDate >= fromDate && a.CreatedDate <= toDate)
                .ToListAsync();
        }


        public async Task<List<Traveller>> GetAllTravellersAsync()
        {
            return await _context.Traveller.ToListAsync();
        }

        public async Task<List<ArrivalApplication>> GetAllApplicationsAsync()
        {
            return await _context.ArrivalApplication
                .Include(a => a.Traveller)
                .ToListAsync();
        }


    }
}
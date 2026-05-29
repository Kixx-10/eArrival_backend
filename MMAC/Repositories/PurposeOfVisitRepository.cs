using Microsoft.EntityFrameworkCore;
using MMAC.Data;
using MMAC.Models.Master;

namespace MMAC.Repositories
{
    public class PurposeOfVisitRepository : IPurposeOfVisitRepository
    {
        private readonly AppDbContext _context;

        public PurposeOfVisitRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PurposeOfVisit?> AddPurposeOfVisitAsync(PurposeOfVisit entity)
        {
            await _context.PurposeOfVisit.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<PurposeOfVisit>> GetAllPurposeOfVisitsAsync()
        {
            return await _context.PurposeOfVisit.ToListAsync();
        }
    }
}
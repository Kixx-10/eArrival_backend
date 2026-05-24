
using Microsoft.EntityFrameworkCore;
using MMAC.Data;
using MMAC.Models.Master;

namespace MMAC.Services
{
    public class PurposeOfVisitService : IPurposeOfVisitService
    {
        private readonly AppDbContext _context;

        public PurposeOfVisitService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<PurposeOfVisit?> CreatePurposeOfVisitAsync(PurposeOfVisit purposeOfVisit)
        {
            try
            {
                await _context.PurposeOfVisit.AddAsync(purposeOfVisit);
                await _context.SaveChangesAsync();

                return purposeOfVisit;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return null;
        }
        public async Task<IEnumerable<PurposeOfVisit>> GetPurposeOfVisitAsync()
        {
            try
            {
                var response = await _context.PurposeOfVisit.ToListAsync();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new List<PurposeOfVisit>();

        }
    }
}
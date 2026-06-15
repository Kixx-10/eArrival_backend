using Microsoft.EntityFrameworkCore;
using MMAC.Data;
using MMAC.Models.Cores;

namespace MMAC.Repositories
{
    public class CompleteArrivalRepository : ICompleteArrivalRepository
    {
        private readonly AppDbContext _context;

        public CompleteArrivalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> SubmitArrivalApplicationAsync(Traveller traveller, ArrivalApplication application)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                //if new user generate id
                if (traveller.TravellerId == Guid.Empty)
                {
                    traveller.TravellerId = Guid.NewGuid();
                }
                await _context.Traveller.AddAsync(traveller);

                //foreign key traveller id ==traveller.id
                application.TravellerId = traveller.TravellerId;
                await _context.ArrivalApplication.AddAsync(application);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return application.AppNo;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"[DB ERROR]: {ex.ToString()}");
                return Guid.Empty;
            }
        }

        public async Task<ArrivalApplication?> GetArrivalApplicationDetailsAsync(Guid appNo)
        {
            return await _context.ArrivalApplication
                .Include(x => x.Traveller)
                .Include(x => x.selectedModeOfTravel)
                .Include(x => x.selectedPortOfArrival)
                .Include(x => x.Township)
                    .ThenInclude(t => t!.District)
                        .ThenInclude(d => d!.StateRegion)
                .FirstOrDefaultAsync(a => a.AppNo == appNo);
        }

        public async Task<bool> IsReferenceNoExistsAsync(string referenceNo)
        {
            return await _context.ArrivalApplication.AnyAsync(a => a.ReferenceNo == referenceNo);
        }
    }
}
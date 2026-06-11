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
                bool isTravellerExist = false;
                if (traveller.TravellerId != Guid.Empty)
                {
                    isTravellerExist = await _context.Traveller.AnyAsync(t => t.TravellerId == traveller.TravellerId);
                }

                // Traveller create or update 
                if (traveller.TravellerId == Guid.Empty || !isTravellerExist)
                {
                    if (traveller.TravellerId == Guid.Empty)
                    {
                        traveller.TravellerId = Guid.NewGuid();
                    }
                    await _context.Traveller.AddAsync(traveller);
                }
                else
                {
                    // if real has database to update
                    _context.Traveller.Update(traveller);
                }

                if (!string.IsNullOrEmpty(application.ReferenceNo))
                {
                    var previousActiveApps = await _context.ArrivalApplication
                        .Where(a => a.ReferenceNo == application.ReferenceNo && a.AppStatus == "Acitve")
                        .ToListAsync();

                    foreach (var oldApp in previousActiveApps)
                    {
                        oldApp.AppStatus = "Invalid";
                        oldApp.UpdatedDate = DateTime.UtcNow.Date;
                    }
                }
                application.TravellerId = traveller.TravellerId;
                await _context.ArrivalApplication.AddAsync(application);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return application.AppNo;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine(ex.ToString());
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
using Microsoft.EntityFrameworkCore;
using MMAC.Models.Address;
using MMAC.Models.Audits;
using MMAC.Models.Cores;
using MMAC.Models.Master;
using MMAC.Models.NRC;
using static MMAC.Models.NRC.NRC_StateRegion;

namespace MMAC.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        //Cores
        public DbSet<Traveller> Traveller { get; set; }
        public DbSet<ArrivalApplication> ArrivalApplication { get; set; }

        //Masters
        public DbSet<Country> Country { get; set; }
        public DbSet<PortOfArrival> PortOfArrival { get; set; }
        public DbSet<ModeOfTravel> ModeOfTravel { get; set; }
        public DbSet<PurposeOfVisit> PurposeOfVisit { get; set; }

        //For Address
        public DbSet<StateRegion> StateRegion { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Township> Township { get; set; }

        //For NRC
        public DbSet<NRCStateRegion> NRC_StateRegion { get; set; }
        public DbSet<NRC_Township> NRC_Township { get; set; }

        //For Audits
        public DbSet<AuditLogs> AuditLogs { get; set; }
    }
}

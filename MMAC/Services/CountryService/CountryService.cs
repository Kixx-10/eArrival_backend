using Microsoft.EntityFrameworkCore;
using MMAC.Data; // 1. Pulls in their Data folder seamlessly
using MMAC.Interfaces;
using MMAC.Models.Master;

namespace MMAC.Services
{
    public class CountryService : ICountryService
    {
        private readonly AppDbContext _context; // 2. Matches their AppDbContext name

        public CountryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Country>> GetAllCountresAsync()
        {
            // 3. Matches their DbSet name which is "_context.Country"
            return await _context.Country.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Country>> GetIcaoMemberCountriesAsync()
        {
            return await _context.Country
                .AsNoTracking()
                .Where(c => c.IsICAOMember == true) // bool ဖြစ်တဲ့အတွက် ရှင်းလင်းသွားတယ်
                .ToListAsync();
        }
        //public async Task<IEnumerable<Country>> GetNationalityCountryAsync()
        //{
        //    // 3. Matches their DbSet name which is "_context.Country"
        //    return await _context
        //        .Country
        //        .AsNoTracking()
        //        .Where(c => c.Type == Country.CountryType.Nationality)
        //        .ToListAsync();
        //}

        // Logic for: GET COUNTRY BY ID (Code)
        public async Task<Country?> GetCountryByCodeAsync(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return null;

            var cleanCode = code.ToUpper().Trim();

            return await _context.Country
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(c => c.CountryCode == cleanCode);
        }
    }
}
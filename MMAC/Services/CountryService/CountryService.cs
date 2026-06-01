using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MMAC.Interfaces;
using MMAC.Models.Master;
using MMAC.Data; // 1. Pulls in their Data folder seamlessly

namespace MMAC.Services
{
    public class CountryService : ICountryService
    {
        private readonly AppDbContext _context; // 2. Matches their AppDbContext name

        public CountryService(AppDbContext context)
        {
            _context = context;
        }

        // Logic for: GET ALL Countries
        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            // 3. Matches their DbSet name which is "_context.Country"
            return await _context.Country.AsNoTracking().ToListAsync();
        }

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
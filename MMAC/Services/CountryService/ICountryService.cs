using System.Collections.Generic;
using System.Threading.Tasks;
using MMAC.Models.Master;

namespace MMAC.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();


        Task<Country?> GetCountryByCodeAsync(string code);
    }
}
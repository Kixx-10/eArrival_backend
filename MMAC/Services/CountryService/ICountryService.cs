using MMAC.Models.Master;

namespace MMAC.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllCountresAsync();
        Task<IEnumerable<Country>> GetIcaoMemberCountriesAsync();


        Task<Country?> GetCountryByCodeAsync(string code);
    }
}
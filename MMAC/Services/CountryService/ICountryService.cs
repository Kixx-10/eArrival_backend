using MMAC.Models.Master;

namespace MMAC.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetPassportIssuedCountryAsync();
        Task<IEnumerable<Country>> GetNationalityCountryAsync();


        Task<Country?> GetCountryByCodeAsync(string code);
    }
}
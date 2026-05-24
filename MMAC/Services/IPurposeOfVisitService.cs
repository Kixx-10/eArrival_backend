
using MMAC.Models.Master;

namespace MMAC.Services
{
    public interface IPurposeOfVisitService
    {
        Task<PurposeOfVisit?> CreatePurposeOfVisitAsync(PurposeOfVisit purposeOfVisit);
        Task<IEnumerable<PurposeOfVisit>> GetPurposeOfVisitAsync();
    }
}

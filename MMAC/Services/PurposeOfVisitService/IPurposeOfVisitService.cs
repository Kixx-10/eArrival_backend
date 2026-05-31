using MMAC.Models.Master;

namespace MMAC.Services.PurposeOfVisitService
{
    public interface IPurposeOfVisitService
    {
        Task<PurposeOfVisit?> CreatePurposeOfVisitAsync(PurposeOfVisit purposeOfVisit);
        Task<IEnumerable<PurposeOfVisit>> GetPurposeOfVisitAsync();
    }
}

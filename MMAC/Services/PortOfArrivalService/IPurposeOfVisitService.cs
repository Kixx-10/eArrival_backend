using MMAC.Models.Master;

namespace MMAC.Services.PortOfArrivalService
{
    public interface IPurposeOfVisitService
    {
        Task<PurposeOfVisit?> CreatePurposeOfVisitAsync(PurposeOfVisit purposeOfVisit);
        Task<IEnumerable<PurposeOfVisit>> GetPurposeOfVisitAsync();
    }
}

using MMAC.Models.Master;

namespace MMAC.Repositories
{
    public interface IPurposeOfVisitRepository
    {
        Task<PurposeOfVisit?> AddPurposeOfVisitAsync(PurposeOfVisit entity);
        Task<IEnumerable<PurposeOfVisit>> GetAllPurposeOfVisitsAsync();
    }
}
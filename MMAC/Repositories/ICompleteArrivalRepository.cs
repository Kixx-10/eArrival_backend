using MMAC.Models.Cores;

namespace MMAC.Repositories
{
    public interface ICompleteArrivalRepository
    {
        // repository accept entity model ,no accept DTO
        Task<Guid> SubmitArrivalApplicationAsync(Traveller traveller, ArrivalApplication application);
        Task<ArrivalApplication?> GetArrivalApplicationDetailsAsync(Guid appNo);
        Task<bool> IsReferenceNoExistsAsync(string referenceNo);
    }
}
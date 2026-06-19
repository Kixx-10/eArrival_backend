using MMAC.Models.Cores;

namespace MMAC.Repositories
{
    public interface ICompleteArrivalRepository
    {
        // repository accept entity model ,no accept DTO
        Task<Guid> SubmitArrivalApplicationAsync(Traveller traveller, ArrivalApplication application);
        Task<bool> IsDuplicateSubmissionWithin24HoursAsync(string fullName, string passportNo, string countryOfBirthCode, DateTime dob);
        Task<ArrivalApplication?> GetArrivalApplicationDetailsAsync(Guid appNo);
        Task<bool> IsReferenceNoExistsAsync(string referenceNo);
        Task<ArrivalApplication?> GetActiveApplicationByReferenceNoAsync(string referenceNo);
        Task<bool> ApproveApplicationAsync(Guid Appno, string AppStatus, string ApproveUser);

    }
}
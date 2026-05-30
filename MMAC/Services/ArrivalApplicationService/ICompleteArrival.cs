
using MMAC.DTOS;

namespace MMAC.Services.ArrivalInterface
{
    public interface ICompleteArrival
    {
        Task<Guid> SubmitAsync(CompleteArrivalDTO dto);
        Task<ResponseCompleteArrivalDTO?> GetScanAsync(Guid AppNo);

    }
}

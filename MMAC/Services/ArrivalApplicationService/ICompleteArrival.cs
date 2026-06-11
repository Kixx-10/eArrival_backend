
using MMAC.DTOS;

namespace MMAC.Services.ArrivalInterface
{
    public interface ICompleteArrival
    {
        Task<ArrivalSubmitResponseDTO> SubmitAsync(CompleteArrivalDTO dto);
        Task<ResponseCompleteArrivalDTO?> GetScanAsync(Guid AppNo);

    }
}

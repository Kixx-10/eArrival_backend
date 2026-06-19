
using MMAC.DTOS;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MMAC.Services.ArrivalInterface
{
    public interface ICompleteArrivalService
    {
        Task<ArrivalSubmitResponseDTO> SubmitAsync(CompleteArrivalDTO dto);
        Task<ResponseCompleteArrivalDTO?> GetScanAsync(Guid AppNo);

        Task<bool> ApproveApplication(Guid Appno, string AppStatus, string ApproveUser);


    }
}

using MMAC.DTOS;

namespace MMAC.Services.UpdateService
{
    public interface IForeignerSearchService
    {
        Task<ResponseForeignerArrivalDTO> SearchForeignerDetailsAsync(ForeignerVerifyRequestDTO dto);
        //Task<ArrivalSubmitResponseDTO> UpdateForeignerDetailsAsync(CompleteArrivalDTO dto);
    }
}


using MMAC.DTOS;

namespace MMAC.Services.SearchService
{
    public interface IMyanmarSearchService
    {
        Task<ResponseMyanmarArrivalDTO> SearchMyanmarDetailsAsync(MyanmarVerifyRequestDTO dto);
    }
}

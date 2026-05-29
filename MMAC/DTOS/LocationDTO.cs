using MMAC.Models.Address;

namespace MMAC.DTOS
{
    public class LocationDTO
    {
        public StateRegion? state { get; set; }
        public List<DistrictDTO>? districts { get; set; }
    }
}

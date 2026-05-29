using MMAC.Models.Address;

namespace MMAC.DTOS
{
    public class DistrictDTO
    {
        public District? district { get; set; }
        public List<Township>? townships { get; set; }
    }
}

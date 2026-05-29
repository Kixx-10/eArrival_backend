using MMAC.Models.Address;
using MMAC.Models.NRC;
using static MMAC.Models.NRC.NRC_StateRegion;

namespace MMAC.DTOS
{
    public class LocationDTO
    {
        public StateRegion? state { get; set; }
        public List<DistrictDTO>? districts { get; set; }
    }

  

    public class NrcDTO
    {
        // Change NRC_StateRegion to NRCStateRegion
        public NRCStateRegion? nrcState { get; set; }
        public List<NRC_Township>? nrcTownships { get; set; }
    }
}

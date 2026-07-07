namespace MMAC.DTOS
{
    public class ArrivalSubmitResponseDTO
    {
        public Guid ApplicationNo { get; set; }
        public string ReferenceNo { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        // public Guid TravellerNo { get; set; }
    }
}

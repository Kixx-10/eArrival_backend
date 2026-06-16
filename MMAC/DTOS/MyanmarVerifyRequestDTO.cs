namespace MMAC.DTOS
{
    public class MyanmarVerifyRequestDTO
    {
        public string ReferenceNo { get; set; } = string.Empty;
        public string NRC { get; set; } = string.Empty;
        public DateTime ArrivalDate { get; set; }
    }
}

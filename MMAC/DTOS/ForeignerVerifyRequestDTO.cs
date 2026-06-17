namespace MMAC.DTOS
{
    public class ForeignerVerifyRequestDTO
    {
        public string ReferenceNo { get; set; } = string.Empty;
        public string PassportNo { get; set; } = string.Empty;
        public string CountryOfBirthCode { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public DateTime DOB { get; set; }
    }
}

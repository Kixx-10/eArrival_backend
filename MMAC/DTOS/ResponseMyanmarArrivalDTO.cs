namespace MMAC.DTOS
{
    public class ResponseMyanmarArrivalDTO
    {
        public string ReferenceNo { get; set; } = string.Empty;
        public string AppStatus { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public required string NationalityCode { get; set; }
        public string NRC { get; set; } = string.Empty;
        public required string FatherName { get; set; } //added required (T)
        public required string Occupation { get; set; } // added occupation (T)
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public required string PlaceOfBirthCode { get; set; } //added PlaceOfBirthCode (T)
        public required string PlaceOfResidenceCode { get; set; } //change address to PlaceOfResidence (T)
        public string PassportNo { get; set; } = string.Empty;
        public string IssuedCountryCode { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        // Arrival Info
        public DateTime ArrivalDate { get; set; }
        public string VehicleNumber { get; set; } = string.Empty;
        public string AddressInMyanmar { get; set; } = string.Empty;
        public string MobileNumberMM { get; set; } = string.Empty;
        public string PurposeOfVisit { get; set; } = string.Empty;
        public string PreviousCity { get; set; } = string.Empty;
        public string HealthDeclaration { get; set; } = string.Empty;
        public string? HealthRecordUrl { get; set; } = string.Empty;

        //original file name
        public string? HealthRecordFileName { get; set; }
        public string DigitalDeclarations { get; set; } = string.Empty;

        // Master Data Names
        public string ModeOfTravelName { get; set; } = string.Empty;
        public string PortOfArrivalName { get; set; } = string.Empty;
        public string StateRegionName { get; set; } = string.Empty;
        public string DistrictName { get; set; } = string.Empty;
        public string TownshipName { get; set; } = string.Empty;
    }
}

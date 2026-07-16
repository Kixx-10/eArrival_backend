namespace MMAC.DTOS
{
    public class ResponseForeignerArrivalDTO
    {
        public string ReferenceNo { get; set; } = string.Empty;
        public string AppStatus { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public required string NationalityCode { get; set; }
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public required string PlaceOfResidence { get; set; } //changed to required
        public required string PlaceOfBirthCode { get; set; } //added place of birth code
        public required string Occupation { get; set; } = string.Empty; //added occupation
        public string VisaNo { get; set; } = string.Empty;
        public string PassportNo { get; set; } = string.Empty;
        public string IssuedCountryCode { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        // Arrival Info
        public DateTime ArrivalDate { get; set; }
        public string? VehicleNumber { get; set; }
        public string? VehicleName { get; set; }
        public string Accommodation { get; set; } = string.Empty;//
        public string AddressInMyanmar { get; set; } = string.Empty;//
        public string PurposeOfVisit { get; set; } = string.Empty;
        public string? PreviousCity { get; set; }
        public string? HealthDeclaration { get; set; }
        public string? HealthRecordUrl { get; set; }

        //original file name

        public string? HealthRecordFileName { get; set; }
        public string? DigitalDeclarations { get; set; }
        public string? GoodsRecordUrl { get; set; }
        public string? GoodsRecordFileName { get; set; }

        // Master Data Names
        public string ModeOfTravelName { get; set; } = string.Empty;
        public string PortOfArrivalName { get; set; } = string.Empty;
        public string StateRegionName { get; set; } = string.Empty;
        public string DistrictName { get; set; } = string.Empty;
        public string TownshipName { get; set; } = string.Empty;
    }
}

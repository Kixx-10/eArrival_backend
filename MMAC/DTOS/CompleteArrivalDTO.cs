namespace MMAC.DTOS
{
    public class CompleteArrivalDTO
    {
        public string? ReferenceNo { get; set; }
        // Traveller Information
        public string FullName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public string CountryOfBirthCode { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;


        public string? VisaNo { get; set; }

        public string? NRC { get; set; }
        public string? FatherName { get; set; }
        public string PassportNo { get; set; } = string.Empty;
        public string IssuedCountryCode { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public DateTime ArrivalDate { get; set; }
        public int ModeOfTravelId { get; set; }
        public int PortOfArrivalId { get; set; }

        public string VehicleNumber { get; set; } = string.Empty;
        public string VehicleName { get; set; } = string.Empty;
        public string Accommodation { get; set; } = string.Empty;

        public string AddressInMyanmar { get; set; } = string.Empty;
        public int StateRegionId { get; set; }
        public int DistrictId { get; set; }
        public int TownshipId { get; set; }
        public string MobileNumberMM { get; set; } = string.Empty;
        public string PurposeOfVisit { get; set; } = string.Empty;
        public string PreviousCity { get; set; } = string.Empty;
        public string HealthDeclaration { get; set; } = string.Empty;
        public string DigitalDeclarations { get; set; } = string.Empty;
    }
}
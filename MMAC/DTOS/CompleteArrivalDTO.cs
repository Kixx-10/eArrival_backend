namespace MMAC.DTOS
{
    public class CompleteArrivalDTO
    {
        //Traveller Information
        public string FullName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public string CountryOfBirthCode { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string VisaNo { get; set; } = string.Empty;
        public string NRC { get; set; } = string.Empty;
        public string FatherName { get; set; } = string.Empty;
        public string PassportNo { get; set; } = string.Empty;
        public string IssuedCountryCode { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        //ArrivalApplication or Tips & Accommondation
        public DateTime ArrivalDate { get; set; }
        public int ModeOfTravelId { get; set; }
        public int PortOfArrivalId { get; set; }
        public string? VehicleNumber { get; set; }
        public string? VehicleName { get; set; }
        public string? Accommodation { get; set; }
        public string AddressInMyanmar { get; set; } = string.Empty;
        public int StateRegionId { get; set; }
        public int DistrictId { get; set; }
        public int TownshipId { get; set; }
        public string MobileNumberMM { get; set; } = string.Empty;
        public string PurposeOfVisit { get; set; } = string.Empty;
        public string? PreviousCity { get; set; }
        public string? HealthDeclaration { get; set; }
        public string? DigitalDeclarations { get; set; }

    }
}


using FluentValidation;
using MMAC.DTOS;

namespace MMAC.Validations
{
    public class CompleteArrivalDTOValidator : AbstractValidator<CompleteArrivalDTO>
    {
        public CompleteArrivalDTOValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Name is required")
                .MaximumLength(30).WithMessage("Name cannot more 50 characters");
            RuleFor(x => x.Gender).NotEmpty().WithMessage("Select your gender")
                .MaximumLength(1).WithMessage("Select M or F");

            RuleFor(x => x.DOB).NotEmpty().WithMessage("DOB is required")
                .LessThan(DateTime.Today).WithMessage("DOB must be in the past");

            RuleFor(x => x.NationalityCode).NotEmpty().WithMessage("NationalltyCode is required")
                .MaximumLength(3).WithMessage("Code maximum 3");

            RuleFor(x => x.PlaceOfBirthCode).NotEmpty().WithMessage("PlaceOfBirthCode is required")
              .MaximumLength(3).WithMessage("Code maximum 3");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required")
                .MaximumLength(30).WithMessage("Email cannot more 30 characters")
                .EmailAddress().WithMessage("Invalid email address");

            RuleFor(x => x.Occupation).NotEmpty().WithMessage("Occupation is required")
                .MaximumLength(30).WithMessage("Occupation cannot more 30 characters");

            RuleFor(x => x.UID)
                .MaximumLength(10).WithMessage("UID cannot exceed 10 characters.");

            RuleFor(x => x.MobileNumber).NotEmpty().WithMessage("Moblie No is required")
                .MaximumLength(20).WithMessage("Mobile No cannot more 20 characters");

            RuleFor(x => x.PlaceOfResidenceCode).NotEmpty().WithMessage("Place of residence is required")
                .MaximumLength(3).WithMessage("PlaceOfResidenceCode cannot more 3 characters");

            RuleFor(x => x.NRC).MaximumLength(30).WithMessage("NRC cannot more 30 characters");

            RuleFor(x => x.FatherName).MaximumLength(50).WithMessage("Father Name cannot more 50 characters");

            RuleFor(x => x.PassportNo).NotEmpty().WithMessage("PassportNo is required")
                .MaximumLength(20).WithMessage("Passport cannot more 50 characters");

            RuleFor(x => x.VisaNo).MaximumLength(50).WithMessage("Visa Number cannot more 50 characters");

            RuleFor(x => x.IssuedCountryCode).NotEmpty().WithMessage("IssuedCountryCode is required")
                .MaximumLength(3).WithMessage("IssuedCounntryCode cannot more 3 characters");

            RuleFor(x => x.IssuedDate)
                .NotEmpty().WithMessage("Passport Issue Date is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Issue date cannot be in the future.")
                //Issued date greater than birthday
                .GreaterThan(x => x.DOB).WithMessage("Issue date must be after your Date of Birth.");

            RuleFor(x => x.ExpiryDate)
                 .NotEmpty().WithMessage("Passport Expiry Date is required.")
                 .GreaterThan(x => x.IssuedDate).WithMessage("Expiry date must be after the Passport Issue Date.")
                 .Must(expiryDate => expiryDate >= DateTime.Today.AddMonths(6))
                 .WithMessage("This passport cannot be used because it has expired or has less than 6 months of validity remaining from today.")
                 .When(x => x.NationalityCode != "MMR");
            RuleFor(x => x.ArrivalDate)
                .NotEmpty().WithMessage("Arrival Date is required.")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Arrival Date cannot be in the past.");


            RuleFor(x => x.ModeOfTravelId)
                .GreaterThan(0).WithMessage("Please select a valid Mode of Travel.");

            RuleFor(x => x.PortOfArrivalId)
                .GreaterThan(0).WithMessage("Please select a valid Port of Arrival.");


            RuleFor(x => x.VehicleNumber)
                .MaximumLength(20).WithMessage("Vehicle Number cannot exceed 10 characters.");


            RuleFor(x => x.Accommodation)
                .MaximumLength(50).WithMessage("Accommodation info cannot exceed 20 characters.");

            RuleFor(x => x.TownshipId).GreaterThan(0).WithMessage("Township Selection is required.");

            RuleFor(x => x.DistrictId).GreaterThan(0).WithMessage("District Selection is required.");

            RuleFor(x => x.StateRegionId).GreaterThan(0).WithMessage("State or Region Selection is required.");

            RuleFor(x => x.MobileNumberMM)
                .MaximumLength(11).WithMessage("Myanmar Mobile Number cannot exceed 20 characters.");

            RuleFor(x => x.PreviousCity)
                .MaximumLength(50).WithMessage("Previous City cannot exceed 20 characters.");

            RuleFor(x => x.HealthDeclaration)
                .MaximumLength(100).WithMessage("Health Declaration info cannot exceed 100 characters.");

            RuleFor(x => x.DigitalDeclarations)
                .MaximumLength(100).WithMessage("Digital Declarations info cannot exceed 100 characters.");
        }
    }
}

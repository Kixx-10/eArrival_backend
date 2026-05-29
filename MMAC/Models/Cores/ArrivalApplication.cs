using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MMAC.Models.Address;
using MMAC.Models.Master;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMAC.Models.Cores
{
    [Table("ArrivalApplication")]
    public class ArrivalApplication
    {
        [Key]
        public Guid AppNo { get; set; } = Guid.NewGuid();

        [Required]
        public Guid TravellerId { get; set; }

        [ForeignKey("TravellerId")]
        public virtual Traveller? Traveller { get; set; }


        [Required]
        [MaxLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string AppStatus { get; set; } = "Pending";

        [Required]
        [Column(TypeName = "date")]
        public DateTime ArrivalDate { get; set; }


        [Required]
        public int ModeOfTravelId { get; set; }
        [ForeignKey("ModeOfTravelId")]
        public virtual ModeOfTravel? selectedModeOfTravel { get; set; }


        [Required]
        public int PortOfArrivalId { get; set; }

        [ForeignKey("PortOfArrivalId")]
        public virtual PortOfArrival? selectedPortOfArrival { get; set; }

        [MaxLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? VehicleNumber { get; set; }

        [MaxLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? VehicleName { get; set; }


        [MaxLength(20)]
        public string? Accommodation { get; set; }

        [Required]
        [MaxLength(100)]
        public string AddressInMyanmar { get; set; } = string.Empty;


        [Required]
        public int TownshipId { get; set; }

        [ForeignKey("TownshipId")]
        public virtual Township? Township { get; set; }
        public int DistrictId { get; set; }
        public int StateRegionId { get; set; }


        [Required]
        [MaxLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string MobileNumberMM { get; set; } = string.Empty;

        [Required]
        public int PurposeOfVisitId { get; set; }
        [ForeignKey("PurposeOfVisitId")]
        public virtual PurposeOfVisit? PurposeOfVisit { get; set; }

        [MaxLength(20)]
        public string? PreviousCity { get; set; }


        [MaxLength(100)]
        public string? HealthDeclaration { get; set; }

        [MaxLength(100)]
        public string? DigitalDeclarations { get; set; }


        public DateTime? ApprovedDate { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? ApprovedUser { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.Date;

        [Column(TypeName = "date")]
        public DateTime? UpdatedDate { get; set; }
    }
}
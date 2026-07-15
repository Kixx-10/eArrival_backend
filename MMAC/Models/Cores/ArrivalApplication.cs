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
        [MaxLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string ReferenceNo { get; set; } = string.Empty;

        [Required]
        public Guid TravellerId { get; set; }

        [ForeignKey("TravellerId")]
        public virtual Traveller? Traveller { get; set; }


        [Required]
        [MaxLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? AppStatus { get; set; }

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

        [Required]
        [MaxLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string VehicleNumber { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Accommodation { get; set; }

        [MaxLength(255)]
        public string? AddressInMyanmar { get; set; }

        [Required]
        public int TownshipId { get; set; }

        [ForeignKey("TownshipId")]
        public virtual Township? Township { get; set; }
        public int DistrictId { get; set; }
        public int StateRegionId { get; set; }


        [MaxLength(11)]
        [Column(TypeName = "varchar(11)")]
        public string? MobileNumberMM { get; set; }

        [MaxLength(100)]
        [Required]
        public string PurposeOfVisit { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string PreviousCity { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string HealthDeclaration { get; set; } = string.Empty;

        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string? HealthRecordUrl { get; set; }

        //original file name
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string? HealthRecordFileName { get; set; }

        [Required]
        [MaxLength(100)]
        public string DigitalDeclarations { get; set; } = string.Empty;

        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string? GoodsRecordUrl { get; set; }

        //original file name
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string? GoodsRecordFileName { get; set; }

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
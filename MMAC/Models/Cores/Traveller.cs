using MMAC.Models.Audits;
using MMAC.Models.Master;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMAC.Models.Cores
{
    [Table("Traveller")]
    public class Traveller
    {
        [Key]
        public Guid TravellerId { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(50)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(1)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "date")]
        public DateTime DOB { get; set; }

        [Required]
        [MaxLength(3)]
        [Column(TypeName = "varchar(3)")]
        public string CountryOfBirthCode { get; set; } = string.Empty;

        [ForeignKey("CountryOfBirthCode")]
        public virtual Country? CountryOfBirth { get; set; }

        [Required]
        [MaxLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string MobileNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? VisaNo { get; set; } = string.Empty;

        [MaxLength(30)]
        public string? NRC { get; set; }


        [MaxLength(50)]
        public string? FatherName { get; set; }

        [Required]
        [MaxLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string PassportNo { get; set; } = string.Empty;

        [Required]
        [MaxLength(3)]
        [Column(TypeName = "varchar(3)")]
        public string IssuedCountryCode { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "date")]
        public DateTime IssuedDate { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime ExpiryDate { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.Date;

        [Column(TypeName = "date")]
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<ArrivalApplication> ArrivalApplications { get; set; }
            = new List<ArrivalApplication>();

        public virtual ICollection<AuditLogs> AuditLogs { get; set; }
            = new List<AuditLogs>();
    }
}
using MMAC.Models.Cores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMAC.Models.Audits

{
    [Table("AuditLogs")]
    public class AuditLogs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogId { get; set; }

        [Required]
        public DateTime LogTime { get; set; } = DateTime.UtcNow;

        // Traveller FK
        [Required]
        public Guid TravellerId { get; set; }

        [ForeignKey(nameof(TravellerId))]
        public Traveller? Traveller { get; set; }

        [Required, MaxLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string LogIPAddr { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Activity { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Inputted { get; set; }
    }

}
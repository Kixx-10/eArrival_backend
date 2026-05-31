using MMAC.Models.Cores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMAC.Models.Master
{
    [Table("PortOfArrival")]
    public class PortOfArrival
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PortOfArrivalId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string PortOfArrivalName { get; set; } = string.Empty;

        [Required]
        public int ModeOfTravelId { get; set; }
        [ForeignKey("ModeOfTravelId")]
        public virtual ModeOfTravel? ModeOfTravel { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.Date;

        [Column(TypeName = "date")]
        public DateTime? UpdatedDate { get; set; }


        public virtual ICollection<ArrivalApplication> ArrivalApplications { get; set; } = new List<ArrivalApplication>();
    }
}
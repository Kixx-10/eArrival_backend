using MMAC.Models.Cores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MMAC.Models.Master
{
    [Table("PurposeOfVisit")]
    public class PurposeOfVisit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurposeOfVisitId { get; set; }

        [Required]
        [MaxLength(20)]
        public string PurposeOfVisitName { get; set; } = string.Empty;

        [JsonIgnore]
        public virtual ICollection<ArrivalApplication> ArrivalApplications { get; set; } =
            new List<ArrivalApplication>();
    }
}

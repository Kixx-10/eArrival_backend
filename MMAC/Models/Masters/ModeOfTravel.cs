using MMAC.Models.Cores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MMAC.Models.Master
{
    [Table("ModeOfTravel")]
    public class ModeOfTravel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ModeOfTravelId { get; set; }

        [Required]
        [MaxLength(20)]
        public string ModeOfTravelName { get; set; } = string.Empty;

        [JsonIgnore]
        public virtual ICollection<ArrivalApplication> ArrivalApplication { get; set; } = new List<ArrivalApplication>();
        [JsonIgnore]
        public virtual ICollection<PortOfArrival> PortOfArrival { get; set; } = new List<PortOfArrival>();
    }
}
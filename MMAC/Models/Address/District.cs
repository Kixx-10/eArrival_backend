using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMAC.Models.Address
{
    [Table("District")]
    public class District
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DistrictId { get; set; }

        [Required]
        public int SRId { get; set; }

        [ForeignKey("SRId")]
        public virtual StateRegion? StateRegion { get; set; }

        [Required]
        [MaxLength(20)]
        public string IdCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string NameMM { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "char(1)")]
        public string SystemUse { get; set; } = "Y";

        public virtual ICollection<Township> Townships { get; set; } = new List<Township>();
    }
}
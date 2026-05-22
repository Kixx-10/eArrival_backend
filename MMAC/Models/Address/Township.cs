using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMAC.Models.Address
{
    [Table("Township")]
    public class Township
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }


        [Required]
        public int DistrictId { get; set; }

        [ForeignKey("DistrictId")]
        public virtual District? District { get; set; }

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
    }
}
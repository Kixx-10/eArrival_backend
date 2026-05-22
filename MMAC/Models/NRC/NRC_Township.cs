using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MMAC.Models.NRC.NRC_StateRegion;

namespace MMAC.Models.NRC
{
    [Table("NRC_Township")]
    public class NRC_Township
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }


        [Required]
        [MaxLength(50)]
        public string IdCode { get; set; } = string.Empty;


        [Required]
        [MaxLength(50)]
        public string CodeMM { get; set; } = string.Empty;


        [Required]
        public int NRC_SRId { get; set; }

        [ForeignKey("NRC_SRId")]
        public virtual NRCStateRegion? StateRegion { get; set; }


        [Required]
        [Column(TypeName = "char(1)")]
        public string SystemUse { get; set; } = "Y";


        public DateTime? CreatedDate { get; set; }


        [MaxLength(100)]
        public string? CreatedUser { get; set; }


        [MaxLength(50)]
        public string? CreatedIPAddr { get; set; }


        public DateTime? UpdatedDate { get; set; }

        [MaxLength(100)]
        public string? UpdatedUser { get; set; }


        [MaxLength(50)]
        public string? UpdatedIPAddr { get; set; }
    }
}

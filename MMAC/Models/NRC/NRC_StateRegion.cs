using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMAC.Models.NRC
{
    public class NRC_StateRegion
    {
        [Table("NRC_StateRegion")]
        public class NRCStateRegion
        {

            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int Id { get; set; }


            [Required]
            [MaxLength(10)]
            public string IdCode { get; set; } = string.Empty;


            [Required]
            [MaxLength(10)]
            public string CodeMM { get; set; } = string.Empty;


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


            public virtual ICollection<NRC_Township> Townships { get; set; } = new List<NRC_Township>();
        }
    }
}
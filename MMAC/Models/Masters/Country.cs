using MMAC.Models.Cores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMAC.Models.Master
{
    [Table("Country")]
    public class Country
    {

        [Key]
        [Required]
        [MaxLength(3)]
        [Column("CountryISOAlpha3Code", TypeName = "varchar(3)")]
        public string CountryCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string NameMM { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.Date;

        [Column(TypeName = "date")]
        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow.Date;


        public virtual ICollection<Traveller> Travellers { get; set; } = new List<Traveller>();

    }
}
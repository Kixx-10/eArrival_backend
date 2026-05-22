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
        [MaxLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string NameMM { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.Date;

        [Column(TypeName = "date")]
        public DateTime? UpdatedDate { get; set; }


        public virtual ICollection<Traveller> Travellers { get; set; } = new List<Traveller>();

    }
}
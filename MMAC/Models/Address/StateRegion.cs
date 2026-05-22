using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMAC.Models.Address
{
    [Table("StateRegion")]
    public class StateRegion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //to stop auto increment
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string IdCode { get; set; } = string.Empty; // MMR001

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty; // Kachin State

        [Required]
        [MaxLength(100)]
        public string NameMM { get; set; } = string.Empty; //Myanmr Name

        [Required]
        [Column(TypeName = "char(1)")]
        public string SystemUse { get; set; } = "Y";


        public virtual ICollection<District> Districts { get; set; } = new List<District>();
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emmaus_api.Models
{
    [Table("provenance")]
    public class Provenance
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("nom")]
        public string Nom { get; set; }
    }

}

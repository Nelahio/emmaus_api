using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emmaus_api.Models
{
    [Table("meuble")]
    public class Meuble
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("nom")]
        public string Nom { get; set; }

        [Required]
        [Column("poids")]
        public int Poids { get; set; }

        [Required]
        [Column("id_categorie_meuble")]
        public int CategorieMeubleId { get; set; }

        [ForeignKey("CategorieMeubleId")]
        public CategorieMeuble CategorieMeuble { get; set; }
    }

}

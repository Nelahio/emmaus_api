using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emmaus_api.Models
{
    [Table("declaration")]
    public class Declaration
    {
        [Column("code_declaration")]
        [Key]
        [Required]
        public string CodeDeclaration { get; set; }

        [Column("date_declaration")]
        [Required]
        public DateTime DateDeclaration { get; set; }

        [Column("trimestre")]
        [RegularExpression("^T[1-4]{1}$", ErrorMessage = "Le trimestre doit être compris entre T1 et T4")]
        [Required]
        public string Trimestre { get; set; }

        [Column("annee")]
        [Required]
        public short Annee { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emmaus_api.Models
{
    [Table("collecter_ramasse_meuble")]
    [PrimaryKey(nameof(MeubleId), nameof(RamasseId))]
    public class CollecterRamasseMeuble
    {
        [Column("id_ramasse")]
        public int? RamasseId { get; set; }

        [Column("id_meuble")]
        public int MeubleId { get; set; }

        [Column("quantite_collectee")]
        public int QuantiteCollectee { get; set; }

        [ForeignKey("RamasseId")]
        public Ramasse? Ramasse { get; set; }

        [ForeignKey("MeubleId")]
        public Meuble? Meuble { get; set; }
    }

}

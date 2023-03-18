using emmaus_api.Data;
using emmaus_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace emmaus_api.Controllers
{
    public class LivraisonAvecMeubles
    {
        public Livraison Livraison { get; set; }
        public List<LivrerMeubleLivraison> MeublesLivres { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class LivraisonController : BaseController
    {
        private readonly EmmausContext dbContext;

        public LivraisonController(EmmausContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<Livraison>> GetLivraisons()
        {
            var livraisons = await dbContext.Livraisons
                .Include(l => l.TrancheHoraire)
                .Include(l => l.FicheTracabiliteReemploi)
                .ToListAsync();
            return JsonContent(livraisons);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<Livraison>> GetLivraisonById(int id)
        {
            var livraison = await dbContext.Livraisons
                .Include(l => l.TrancheHoraire)
                .Include(l => l.FicheTracabiliteReemploi)
                .FirstOrDefaultAsync(l => l.Id == id);
            if (livraison == null)
            {
                return NotFound();
            }
            return JsonContent(livraison);
        }

        [HttpPost]
        public async Task<ActionResult<Livraison>> PostLivraison([FromBody] LivraisonAvecMeubles livraisonAvecMeubles)
        {
            //Vérification des objets
            if (livraisonAvecMeubles?.Livraison == null)
                return BadRequest("La livraison ne peut pas être nulle.");
            if (livraisonAvecMeubles?.MeublesLivres == null)
                return BadRequest("Les meubles ne peuvent pas être nuls.");

            //Ajout de la livraison en bdd
            dbContext.Livraisons.Add(livraisonAvecMeubles.Livraison);
            await dbContext.SaveChangesAsync();

            //Parcours des meubles, liaison avec la livraison et ajout en bdd
            foreach (var meuble in livraisonAvecMeubles.MeublesLivres)
            {
                meuble.LivraisonId = livraisonAvecMeubles.Livraison.Id;
                dbContext.LivrerMeubleFicheLivraisons.Add(meuble);
            }
            await dbContext.SaveChangesAsync();
            return JsonContent(livraisonAvecMeubles);
        }
    }
}

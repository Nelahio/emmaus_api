using emmaus_api.Data;
using emmaus_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace emmaus_api.Controllers
{
    public class FicheTracabiliteAvecRamasse
    {
        public FicheTracabilite FicheTracabilite { get; set; }
        public List<TracerFicheMeuble> TracerFicheMeubles { get; set; }
        public int? RamasseId { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class FicheTracabiliteController : BaseController
    {
        private readonly EmmausContext dbContext;

        public FicheTracabiliteController(EmmausContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<FicheTracabilite>> Get() 
        {
            var ficheTracabilites = await dbContext.FicheTracabilites
                .Include(f => f.Provenance)
                .Include(f => f.TracerFicheMeubles)
                .Include(f => f.FicheTracabiliteCollectes)
                .Include(f => f.FicheTracabiliteReemplois)
                .ToListAsync();
            return JsonContent(ficheTracabilites);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<FicheTracabilite>> GetFicheTracabiliteById(int id)
        {
            var ficheTracabilite = await dbContext.FicheTracabilites
                .Include(f => f.Provenance)
                .Include(fiche => fiche.TracerFicheMeubles)
                .Include(fiche => fiche.FicheTracabiliteCollectes)
                .Include(fiche => fiche.FicheTracabiliteReemplois)
                .FirstOrDefaultAsync(f => f.Id == id);
            if (ficheTracabilite == null)
            {
                return NotFound();
            }
            return JsonContent(ficheTracabilite);
        }

        [HttpGet("{trimestre}/{annee}")]
        public async Task<ActionResult<FicheTracabilite>> GetFicheTracabilitesByTrimestre(int trimestre, int annee)
        {
            // Vérifier que les paramètres envoyés sont valides
            if (trimestre < 1 || trimestre > 4 || annee < 2000 || annee > DateTime.Now.Year)
            {
                return BadRequest("Les paramètres trimestre et annee sont invalides");
            }

            // Calculer les dates de début et de fin du trimestre
            DateTime debutTrimestre = new DateTime(annee, (trimestre - 1) * 3 + 1, 1);
            DateTime finTrimestre = debutTrimestre.AddMonths(3).AddDays(-1);

            // Récupérer toutes les fiches de tracabilité pour le trimestre et l'année envoyés
            var fichesTracabilite = await dbContext.FicheTracabilites
                .Include(f => f.Provenance)
                .Include(f => f.TracerFicheMeubles)
                .Include(f => f.FicheTracabiliteCollectes)
                .Include(f => f.FicheTracabiliteReemplois)
                .Include(f => f.FicheTracabiliteReemplois)
                .Where(f => f.DateFiche >= debutTrimestre && f.DateFiche <= finTrimestre)
                .ToListAsync();

            return JsonContent(fichesTracabilite);
        }

        [HttpPost]
        public async Task<ActionResult<FicheTracabilite>> PostFicheTracabilite([FromBody] FicheTracabiliteAvecRamasse ficheTracabilite)
        {
            if (ficheTracabilite == null)
            {
                return BadRequest("La fiche de traçabilité ne peut pas être nulle");
            }

            dbContext.FicheTracabilites.Add(ficheTracabilite.FicheTracabilite);
            await dbContext.SaveChangesAsync();

            foreach (var ficheMeuble in ficheTracabilite.TracerFicheMeubles)
            {
                ficheMeuble.FicheTracabiliteId = ficheTracabilite.FicheTracabilite.Id;
                dbContext.TracerFicheMeubles.Add(ficheMeuble);
            }
            await dbContext.SaveChangesAsync();

            //Collecte à domicile
            if (ficheTracabilite.FicheTracabilite.ProvenanceId == 2)
            {
                if (ficheTracabilite.RamasseId.HasValue)
                {
                    FicheTracabiliteCollecte ficheCollecte = new FicheTracabiliteCollecte
                    {
                        FicheTracabiliteId = ficheTracabilite.FicheTracabilite.Id,
                        RamasseId = (int)ficheTracabilite.RamasseId
                    }; 
                    dbContext.FicheTracabiliteCollectes.Add(ficheCollecte);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    return BadRequest("La ramasse associée est requise");
                }
            }
            //Réemploi
            if (ficheTracabilite.FicheTracabilite.ProvenanceId == 3)
            {
                FicheTracabiliteReemploi ficheReemploi = new FicheTracabiliteReemploi
                {
                    IdFicheTracabilite = ficheTracabilite.FicheTracabilite.Id
                };
                dbContext.FicheTracabiliteReemplois.Add(ficheReemploi);
                await dbContext.SaveChangesAsync();
            }
            //return CreatedAtAction(nameof(GetFicheTracabiliteById), new { id = ficheTracabilite.FicheTracabilite.Id }, ficheTracabilite);
            return JsonContent(ficheTracabilite);
        }
    }
}

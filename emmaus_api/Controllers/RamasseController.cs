using emmaus_api.Data;
using emmaus_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace emmaus_api.Controllers
{
    public class RamasseAvecMeubles
    {
        public Ramasse Ramasse { get; set; }
        public List<CollecterRamasseMeuble> MeublesCollectes { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class RamasseController : BaseController
    {
        private readonly EmmausContext dbContext;

        public RamasseController(EmmausContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<Ramasse>> Get()
        {
            var ramasses = await dbContext.Ramasses.Include(r => r.TrancheHoraire).ToListAsync();
            return JsonContent(ramasses);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<Ramasse>> GetRamasseById(int id)
        {
            var ramasse = await dbContext.Ramasses.Include(ramasse => ramasse.TrancheHoraire).FirstOrDefaultAsync(r => r.Id == id);
            if (ramasse == null)
            {
                return NotFound();
            }
            return JsonContent(ramasse);
        }

        [HttpPost]
        public async Task<ActionResult<Ramasse>> PostRamasse([FromBody]RamasseAvecMeubles ramasseAvecMeubles)
        {
            //Vérification des objets
            if(ramasseAvecMeubles?.Ramasse == null)            
                return BadRequest("La ramasse ne peut pas être nulle.");
            if (ramasseAvecMeubles?.MeublesCollectes == null)
                return BadRequest("Les meubles ne peuvent pas être nuls.");

            //Ajout de la ramasse en bdd
            dbContext.Ramasses.Add(ramasseAvecMeubles.Ramasse);
            await dbContext.SaveChangesAsync();

            //Parcours des meubles, liaison avec la ramasse et ajout en bdd
            foreach (var meuble in ramasseAvecMeubles.MeublesCollectes)
            {
                meuble.RamasseId = ramasseAvecMeubles.Ramasse.Id;
                dbContext.CollecterRamasseMeubles.Add(meuble);
            }
            await dbContext.SaveChangesAsync();

            //return CreatedAtAction(nameof(GetRamasseById), new { id = ramasseAvecMeubles.Ramasse.Id }, ramasseAvecMeubles.Ramasse);
            return JsonContent(ramasseAvecMeubles);
        }
    }
}

using emmaus_api.Data;
using emmaus_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace emmaus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvenanceController : BaseController
    {
        private readonly EmmausContext dbContext;

        public ProvenanceController(EmmausContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<Provenance>> Get()
        {
            var provenances = await dbContext.Provenances.ToListAsync();
            return JsonContent(provenances);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<Provenance>> GetProvenanceById(int id)
        {
            var provenance = await dbContext.Provenances.FirstOrDefaultAsync(provenance => provenance.Id == id);
            if (provenance == null)
            {
                return NotFound();
            }
            return JsonContent(provenance);
        }

        [HttpGet("nom/{nom}")]
        public async Task<ActionResult<Provenance>> GetProvenanceByNom(string nom)
        {
            var provenance = await dbContext.Provenances.FirstOrDefaultAsync(provenance => provenance.Nom.ToLower().Contains(nom.ToLower()));
            if (provenance == null)
            {
                return NotFound("Cette provenance n'existe pas.");
            }
            return JsonContent(provenance);
        }
    }
}

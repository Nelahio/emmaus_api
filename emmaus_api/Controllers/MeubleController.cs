using emmaus_api.Data;
using emmaus_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace emmaus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeubleController : BaseController
    {
        private readonly EmmausContext dbContext;

        public MeubleController (EmmausContext context)
        {
            this.dbContext = context;
        }
        [HttpGet]
        public async Task<ActionResult<Meuble>> Get()
        {
            var meubles = await dbContext.Meubles.Include(m => m.CategorieMeuble).ToListAsync();
            return JsonContent(meubles);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<Meuble>> GetMeubleById(int id)
        {
            var meuble = await dbContext.Meubles.Include(meuble => meuble.CategorieMeuble).FirstOrDefaultAsync(m => m.Id == id);
            if (meuble == null)
            {
                return NotFound();
            }
            return JsonContent(meuble);
        }

        [HttpGet("nom/{nom}")]
        public async Task<ActionResult<Meuble>> GetMeubleByNom(string nom)
        {
            var meubles = await dbContext.Meubles.Include(meuble => meuble.CategorieMeuble).ToListAsync();
                   var meublesByNom = meubles.Where(m => m.Nom.ToLower().Contains(nom.ToLower()));
            if (meublesByNom.Count() == 0)
            {
                return NotFound("Ce meuble n'existe pas.");
            }
            return JsonContent(meublesByNom);
        }
    }
}

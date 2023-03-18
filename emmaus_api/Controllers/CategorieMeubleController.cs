using emmaus_api.Data;
using emmaus_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace emmaus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorieMeubleController : BaseController
    {
        private readonly EmmausContext dbContext;

        public CategorieMeubleController(EmmausContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var categories = dbContext.CategorieMeubles;
            return JsonContent(categories);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<CategorieMeuble>> GetCategorieById(int id)
        {
            var categorieMeuble = await dbContext.CategorieMeubles.FirstOrDefaultAsync(categorie => categorie.Id == id);
            if (categorieMeuble == null)
            {
                return NotFound();
            }
            return JsonContent(categorieMeuble);
        }

        [HttpGet("nom/{nom}")]
        public async Task<ActionResult<CategorieMeuble>> GetCategorieByNom(string nom)
        {
            var categorieMeuble = await dbContext.CategorieMeubles.FirstOrDefaultAsync(categorie => categorie.Nom.ToLower().Contains(nom.ToLower()));
            if (categorieMeuble == null)
            {
                return NotFound("Cette catégorie n'existe pas.");
            }
            return JsonContent(categorieMeuble);
        }
    }
}

using emmaus_api.Data;
using emmaus_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace emmaus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeclarationController : BaseController
    {
        private readonly EmmausContext dbContext;

        public DeclarationController(EmmausContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<Declaration>> GetDeclarations()
        {
            var declarations = await dbContext.Declarations.ToListAsync();
            return JsonContent(declarations);
        }

        [HttpGet("id/{code}")]
        public async Task<ActionResult<Declaration>> GetDeclarationByCode(string code)
        {
            // Vérifier si le code est valide
            if (code.Length != 6 || code[0] != 'T')
            {
                return BadRequest("Le code de déclaration est invalide.");
            }
            var declaration = await dbContext.Declarations.FirstOrDefaultAsync(d => d.CodeDeclaration == code);
            if (declaration == null)
            {
                return NotFound();
            }
            return JsonContent(declaration);
        }
    }
}

using DietCode.PublicServices.ViewModel.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DietCode.PublicServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentosController : ControllerBase
    {
        public readonly IDocumentApp docGeneratoratorApp;
        public DocumentosController(IDocumentApp docGeneratoratorApp)
        {
            this.docGeneratoratorApp = docGeneratoratorApp;
        }

        [HttpGet("Gerar/CPF/{formatado}")]
        public async Task<IActionResult> GerarCPF(bool formatado = false)
        {
            var result = await docGeneratoratorApp.GerarCPF(formatado);
            return Ok(result);
        }

        [HttpGet("Gerar/CNPJ/{formatado}")]
        public async Task<IActionResult> GerarCNPJ(bool formatado = false)
        {
            var result = await docGeneratoratorApp.GerarCNPJ(formatado);
            return Ok(result);
        }
    }
}

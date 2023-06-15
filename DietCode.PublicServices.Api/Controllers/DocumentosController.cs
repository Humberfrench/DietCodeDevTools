using DietCode.PublicServices.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DietCode.PublicServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentosController : ControllerBase
    {
        public readonly IDocGeneratoratorApp docGeneratoratorApp;
        public DocumentosController(IDocGeneratoratorApp docGeneratoratorApp)
        {
            this.docGeneratoratorApp = docGeneratoratorApp;
        }

        [HttpGet("Gerar/CPF")]
        public async Task<IActionResult> GerarCPF()
        {
            var result = await docGeneratoratorApp.GerarCPF();
            return Ok(result);
        }

        [HttpGet("Gerar/CNPJ")]
        public async Task<IActionResult> GerarCNPJ()
        {
            var result = await docGeneratoratorApp.GerarCNPJ();
            return Ok(result);
        }
    }
}

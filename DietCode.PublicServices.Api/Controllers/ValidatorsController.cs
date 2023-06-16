using DietCode.PublicServices.Domain.Interfaces.Services;
using DietCode.PublicServices.ViewModel.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DietCode.PublicServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidatorsController : ControllerBase
    {
        protected readonly IDocumentApp validator;

        public ValidatorsController(IDocumentApp validator)
        {
            this.validator = validator;
        }

        [HttpGet("{cpf}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obtem os lançamentos do dia")]
        public async Task<IActionResult> ValidarCpf(string cpf)
        {
            var retorno = await validator.ValidarCpf(cpf);
            return Ok(retorno);
        }

        [HttpGet("{cnpj}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Valida CNPJ")]
        public async Task<IActionResult> ValidarCnpj(string cnpj)
        {
            var retorno = await validator.ValidarCnpj(cnpj);
            return Ok(retorno);
        }
    }
}

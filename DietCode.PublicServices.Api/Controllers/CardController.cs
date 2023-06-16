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
    public class CardController : ControllerBase
    {
        protected readonly ICardApp cardApp;

        public CardController(ICardApp validator)
        {
            this.cardApp = validator;
        }


        [HttpGet("{ValidaBandeira}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Valida a Bandeira")]
        public async Task<IActionResult> ValidaBandeira(string card)
        {
            var retorno = await cardApp.ValidaBandeira(card);
            return Ok(retorno);
        }

        [HttpGet("{ValidaCartao}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Valida o Cartão")]
        public async Task<IActionResult> IsValidCreditCardNumber(string card)
        {
            var retorno = await cardApp.IsValidCreditCardNumber(card);
            return Ok(retorno);
        }
    }

}

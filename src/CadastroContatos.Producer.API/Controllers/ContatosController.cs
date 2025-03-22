using CadastroContatos.Application.DTOs;
using CadastroContatos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CadastroContatos.Producer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatosController : ControllerBase
    {
        private readonly IContatoService _contatoService;

        public ContatosController(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        [HttpPost]
        public IActionResult EnviarContato([FromBody] ContatoDto? contatoDto)
        {
            if (contatoDto == null)
                return BadRequest("Dados inválidos.");

            _contatoService.EnviarContatoParaFila(contatoDto);

            return Accepted(new { mensagem = "Contato enviado para a fila com sucesso!" });
        }
    }
}
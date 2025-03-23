using CadastroContatos.Application.DTOs;
using CadastroContatos.Application.Services;
using CadastroContatos.Domain.Domain;
using CadastroContatos.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadastroContatos.Producer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatosController : ControllerBase
    {
        private readonly IContatoService _contatoService;
        private readonly ContactZoneDbContext _context;

        public ContatosController(IContatoService contatoService, ContactZoneDbContext context)
        {
            _contatoService = contatoService;
            _context = context;
        }

        [HttpPost("enviar-fila")]
        public IActionResult EnviarContato([FromBody] ContatoDto? contatoDto)
        {
            if (contatoDto == null)
                return BadRequest("Dados inválidos.");

            _contatoService.EnviarContatoParaFila(contatoDto);

            return Accepted(new { mensagem = "Contato enviado para a fila com sucesso!" });
        }
        
        [HttpPost("cadastrar-direto-banco")]
        public async Task<IActionResult> CadastrarContato([FromBody] ContatoDto? contatoDto)
        {
            if (contatoDto == null)
                return BadRequest("Dados inválidos.");

            var contato = new ContactDomain
            {
                Name = contatoDto.Name,
                DDD = contatoDto.DDD,
                Phone = contatoDto.Phone,
                Email = contatoDto.Email
            };

            _context.Contatos.Add(contato);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Contato cadastrado com sucesso!" });
        }
        
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllContatos()
        {
            var contatos = await _context.Contatos.ToListAsync();
            return Ok(contatos);
        }
    }
}
using CadastroContatos.Application.Dtos;
using CadastroContatos.Application.Services;
using CadastroContatos.Infrastructure.Data;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadastroContatos.Producer.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ContatosController : ControllerBase
    {
        private readonly IContatoService _contatoService;
        private readonly ContactZoneDbContext _context;
        private readonly IValidator<PostContactDto> _validator;

        public ContatosController(IContatoService contatoService, ContactZoneDbContext context, IValidator<PostContactDto> validator)
        {
            _contatoService = contatoService;
            _context = context;
            _validator = validator;
        }

        [HttpPost("cadastro-mensageria")]
        public IActionResult EnviarContato(PostContactDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid contact data.");

            var contactDomain = PostContactDto.ToDomain(new PostContactDto() 
            { 
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                DDD = dto.DDD,
            });
            
            _contatoService.EnviarContatoParaFila(contactDomain);

            return Accepted(new { mensagem = "Contato enviado para a fila com sucesso!" });
        }
        
        [HttpPost("cadastro-api")]
        public async Task<IActionResult> CadastrarContato(PostContactDto? dto)
        {
            if (dto == null)
                return BadRequest("Invalid contact data.");

            var contactDomain = PostContactDto.ToDomain(new PostContactDto() 
            { 
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                DDD = dto.DDD,
            });

            _context.Contatos.Add(contactDomain);
            await _context.SaveChangesAsync();

            return Ok(contactDomain);
        }
        
        [HttpGet("listar-contatos")]
        public async Task<IActionResult> GetAllContatos()
        {
            var contatos = await _context.Contatos.ToListAsync();
            return Ok(contatos);
        }
    }
}
using CadastroContatos.Application.DTOs;

namespace CadastroContatos.Application.Services
{
    public interface IContatoService
    {
        void EnviarContatoParaFila(ContatoDto? contato);
    }
}
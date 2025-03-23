using CadastroContatos.Domain.Domain;

namespace CadastroContatos.Application.Services
{
    public interface IContatoService
    {
        void EnviarContatoParaFila(ContactDomain? contato);
    }
}
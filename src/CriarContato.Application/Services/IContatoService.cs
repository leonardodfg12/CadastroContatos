using CriarContato.Domain.Domain;

namespace CriarContato.Application.Services
{
    public interface IContatoService
    {
        void EnviarContatoParaFila(ContactDomain? contato);
    }
}
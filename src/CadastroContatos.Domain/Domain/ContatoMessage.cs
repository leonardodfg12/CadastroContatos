namespace CadastroContatos.Domain.Domain
{
    public class ContatoMessage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DDD { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
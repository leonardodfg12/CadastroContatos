using System.ComponentModel.DataAnnotations;
using CriarContato.Domain.Domain;

namespace CriarContato.Application.Dtos
{
    public class PostContactDto
    {
        public string Name { get; set; }

        [RegularExpression(@"^\d{2}$")]
        public string DDD { get; set; }

        [Phone]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public static ContactDomain ToDomain(PostContactDto contactDto)
        {
            return new ContactDomain
            {
                Name = contactDto.Name,
                DDD = contactDto.DDD,
                Phone = contactDto.Phone,
                Email = contactDto.Email,
            };
        }
    }
}

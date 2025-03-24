
using System.ComponentModel.DataAnnotations;
using CriarContato.Application.Dtos;

namespace CriarContato.Application.Test.Dtos
{
    [TestFixture]
    public class PostContactDtoTest
    {
        [Test]
        public void ToDomain_MapsPropertiesCorrectly()
        {
            // Arrange
            var dto = new PostContactDto
            {
                Name = "John Doe",
                DDD = "11",
                Phone = "12345678",
                Email = "john.doe@example.com"
            };

            // Act
            var domain = PostContactDto.ToDomain(dto);

            // Assert
            Assert.That(domain.Name, Is.EqualTo(dto.Name));
            Assert.That(domain.DDD, Is.EqualTo(dto.DDD));
            Assert.That(domain.Phone, Is.EqualTo(dto.Phone));
            Assert.That(domain.Email, Is.EqualTo(dto.Email));
        }

        [Test]
        public void PostContactDto_ValidData_PassesValidation()
        {
            // Arrange
            var dto = new PostContactDto
            {
                Name = "John Doe",
                DDD = "11",
                Phone = "12345678",
                Email = "john.doe@example.com"
            };

            // Act & Assert
            Assert.DoesNotThrow(() => ValidateDto(dto));
        }

        [Test]
        public void PostContactDto_InvalidEmail_ThrowsValidationException()
        {
            // Arrange
            var dto = new PostContactDto
            {
                Name = "John Doe",
                DDD = "11",
                Phone = "12345678",
                Email = "invalid-email"
            };

            // Act & Assert
            Assert.Throws<ValidationException>(() => ValidateDto(dto));
        }

        private void ValidateDto(PostContactDto dto)
        {
            var context = new ValidationContext(dto, null, null);
            Validator.ValidateObject(dto, context, true);
        }
    }
}
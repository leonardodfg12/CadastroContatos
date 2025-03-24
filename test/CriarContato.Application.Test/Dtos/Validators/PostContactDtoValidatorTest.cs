
using CriarContato.Application.Dtos;
using CriarContato.Application.Dtos.Validators;
using FluentValidation.TestHelper;

namespace CriarContato.Application.Test.Dtos.Validators
{
    [TestFixture]
    public class PostContactDtoValidatorTest
    {
        private PostContactDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new PostContactDtoValidator();
        }

        [Test]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var model = new PostContactDto { Name = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(contact => contact.Name);
        }

        [Test]
        public void Should_Have_Error_When_DDD_Is_Invalid()
        {
            var model = new PostContactDto { DDD = "123" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(contact => contact.DDD);
        }

        [Test]
        public void Should_Have_Error_When_Phone_Is_Invalid()
        {
            var model = new PostContactDto { Phone = "123" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(contact => contact.Phone);
        }

        [Test]
        public void Should_Have_Error_When_Email_Is_Invalid()
        {
            var model = new PostContactDto { Email = "invalid-email" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(contact => contact.Email);
        }

        [Test]
        public void Should_Not_Have_Error_When_Model_Is_Valid()
        {
            var model = new PostContactDto
            {
                Name = "John Doe",
                DDD = "11",
                Phone = "12345678",
                Email = "john.doe@example.com"
            };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
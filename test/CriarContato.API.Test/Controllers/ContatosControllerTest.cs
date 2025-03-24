
using CriarContato.API.Controllers;
using CriarContato.Application.Dtos;
using CriarContato.Application.Services;
using CriarContato.Domain.Domain;
using CriarContato.Infrastructure.Data;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CriarContato.API.Test.Controllers
{
    [TestFixture]
    public class ContatosControllerTest
    {
        private Mock<IContatoService> _mockContatoService;
        private Mock<IValidator<PostContactDto>> _mockValidator;
        private ContatosController _controller;
        private ContactZoneDbContext _context;

        [SetUp]
        public void Setup()
        {
            _mockContatoService = new Mock<IContatoService>();
            _mockValidator = new Mock<IValidator<PostContactDto>>();

            var options = new DbContextOptionsBuilder<ContactZoneDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ContactZoneDbContext(options);

            _controller = new ContatosController(_mockContatoService.Object, _context, _mockValidator.Object);
        }
        
        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public void EnviarContato_ReturnsAccepted()
        {
            // Arrange
            var dto = new PostContactDto { Name = "John", Email = "john@example.com", Phone = "123456789", DDD = "11" };

            // Act
            var result = _controller.EnviarContato(dto);

            // Assert
            Assert.IsInstanceOf<AcceptedResult>(result);
        }

        [Test]
        public async Task CadastrarContato_ReturnsOk()
        {
            // Arrange
            var dto = new PostContactDto { Name = "John", Email = "john@example.com", Phone = "123456789", DDD = "11" };

            // Act
            var result = await _controller.CadastrarContato(dto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GetAllContatos_ReturnsOk()
        {
            // Arrange
            _context.Contatos.Add(new ContactDomain() { Name = "John", Email = "john@example.com", Phone = "123456789", DDD = "11" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetAllContatos();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}
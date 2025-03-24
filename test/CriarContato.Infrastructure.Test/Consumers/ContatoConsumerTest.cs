using CriarContato.Domain.Domain;
using CriarContato.Infrastructure.Consumers;
using CriarContato.Infrastructure.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace CriarContato.Infrastructure.Test.Consumers
{
    [TestFixture]
    public class ContatoConsumerTest
    {
        private Mock<ILogger<ContatoConsumer>> _loggerMock;
        private ContactZoneDbContext _dbContext;
        private ContatoConsumer _consumer;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<ContatoConsumer>>();

            var options = new DbContextOptionsBuilder<ContactZoneDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new ContactZoneDbContext(options);

            _consumer = new ContatoConsumer(_dbContext, _loggerMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }
        
        [Test]
        public async Task Consume_Should_Save_Contato_To_Database()
        {
            // Arrange
            var contatoMessage = new ContactDomain
            {
                Id = 1,
                Name = "Teste",
                DDD = "11",
                Phone = "999999999",
                Email = "teste@example.com"
            };
            var consumeContextMock = new Mock<ConsumeContext<ContactDomain>>();
            consumeContextMock.Setup(x => x.Message).Returns(contatoMessage);

            // Act
            await _consumer.Consume(consumeContextMock.Object);

            // Assert
            var contato = await _dbContext.Contatos.FindAsync(contatoMessage.Id);
            Assert.NotNull(contato);
            Assert.AreEqual(contatoMessage.Name, contato.Name);
            Assert.AreEqual(contatoMessage.DDD, contato.DDD);
            Assert.AreEqual(contatoMessage.Phone, contato.Phone);
            Assert.AreEqual(contatoMessage.Email, contato.Email);
        }
    }
}
using AutoMapper;
using BigBall.Context.Contracts.Models;
using BigBall.Context.Tests;
using BigBall.Repositories.Contracts.ReadRepositiriesContracts;
using BigBall.Repositories.ReadRepositories;
using BigBall.Repositories.WriteRepositories;
using BigBall.Services.Contracts.Exeptions;
using BigBall.Services.Contracts.Models;
using BigBall.Services.Contracts.ServiceContracts;
using BigBall.Services.Mapper;
using BigBall.Services.Services;
using FluentAssertions;
using Xunit;

namespace BigBall.Services.Tests.Tests
{
    public class PaymentServiceTest : BigBallContextInMemory
    {
        private readonly IPaymentService paymentService;
        private readonly IPaymentReadRepository paymentRead;
        private readonly IMapper mapper;

        public PaymentServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });

            mapper = config.CreateMapper();
            paymentRead = new PaymentReadRepository(Reader);
            paymentService = new PaymentService(paymentRead, new PaymentWriteRepository(WriterContext),
                mapper, UnitOfWork);
        }

        /// <summary>
        /// Тест маппера
        /// </summary>
        [Fact]
        public void TestMapper()
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        /// <summary>
        /// Получение <see cref="Payment"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => paymentService.GetByIdAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BigBallEntityNotFoundException<Payment>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение <see cref="Payment"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Payment();
            await Context.Payments.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await paymentService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Title,
                    target.CardNumber,
                    target.Requisites
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Payment}"/> по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await paymentService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Payment}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Payment();

            await Context.Payments.AddRangeAsync(target,
                TestDataGenerator.Payment(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await paymentService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Удаление несуществуюущего <see cref="Payment"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldNotFoundException()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => paymentService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BigBallEntityNotFoundException<Payment>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Payment"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldInvalidException()
        {
            //Arrange
            var model = TestDataGenerator.Payment(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Payments.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => paymentService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BigBallInvalidOperationException>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Payment"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Payment();
            await Context.Payments.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => paymentService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Payments.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Payment"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var model = mapper.Map<PaymentModel>(TestDataGenerator.Payment());

            //Act
            Func<Task> act = () => paymentService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Payments.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Payment"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var model = mapper.Map<PaymentModel>(TestDataGenerator.Payment());

            //Act
            Func<Task> act = () => paymentService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<BigBallEntityNotFoundException<Payment>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение <see cref="Payment"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var model = mapper.Map<PaymentModel>(TestDataGenerator.Payment());
            var payment = TestDataGenerator.Payment(x => x.Id = model.Id);
            await Context.Payments.AddAsync(payment);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => paymentService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Payments.Single(x => x.Id == payment.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.Title,
                    model.CardNumber,
                    model.Requisites
                });
        }
    }
}

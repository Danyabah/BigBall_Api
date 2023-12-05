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
    public class PromotionServiceTest : BigBallContextInMemory
    {
        private readonly IPromotionService promotionService;
        private readonly IPromotionReadRepository promotionRead;
        private readonly IMapper mapper;

        public PromotionServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });

            mapper = config.CreateMapper();
            promotionRead = new PromotionReadRepository(Reader);
            promotionService = new PromotionService(promotionRead, new PromotionWriteRepository(WriterContext),
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
        /// Получение <see cref="Promotion"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => promotionService.GetByIdAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BigBallEntityNotFoundException<Promotion>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение <see cref="Promotion"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Promotion();
            await Context.Promotions.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await promotionService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.PercentageDiscount,
                    target.Title
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Promotion}"/> по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await promotionService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Promotion}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Promotion();

            await Context.Promotions.AddRangeAsync(target,
                TestDataGenerator.Promotion(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await promotionService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Удаление несуществуюущего <see cref="Promotion"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldNotFoundException()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => promotionService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BigBallEntityNotFoundException<Promotion>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Promotion"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldInvalidException()
        {
            //Arrange
            var model = TestDataGenerator.Promotion(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Promotions.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => promotionService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BigBallInvalidOperationException>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Promotion"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Promotion();
            await Context.Promotions.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => promotionService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Promotions.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Promotion"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var model = mapper.Map<PromotionModel>(TestDataGenerator.Promotion());

            //Act
            Func<Task> act = () => promotionService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Promotions.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Promotion"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var model = mapper.Map<PromotionModel>(TestDataGenerator.Promotion());

            //Act
            Func<Task> act = () => promotionService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<BigBallEntityNotFoundException<Promotion>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение <see cref="Promotion"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var model = mapper.Map<PromotionModel>(TestDataGenerator.Promotion());
            var promotion = TestDataGenerator.Promotion(x => x.Id = model.Id);
            await Context.Promotions.AddAsync(promotion);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => promotionService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Promotions.Single(x => x.Id == promotion.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.Title,
                    model.PercentageDiscount
                });
        }
    }
}

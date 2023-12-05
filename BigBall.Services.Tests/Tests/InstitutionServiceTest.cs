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
    public class InstitutionServiceTest : BigBallContextInMemory
    {
        private readonly IInstitutionService institutionService;
        private readonly IInstitutionReadRepository institutionRead;
        private readonly IMapper mapper;

        public InstitutionServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });

            mapper = config.CreateMapper();
            institutionRead = new InstitutionReadRepository(Reader);
            institutionService = new InstitutionService(institutionRead, new InstitutionWriteRepository(WriterContext),
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
        /// Получение <see cref="Institution"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => institutionService.GetByIdAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BigBallEntityNotFoundException<Institution>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение <see cref="Institution"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Institution();
            await Context.Institutions.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await institutionService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Title,
                    target.Address,
                    target.OpeningTime, 
                    target.ClosingTime
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Institution}"/> по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await institutionService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Institution}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Institution();

            await Context.Institutions.AddRangeAsync(target,
                TestDataGenerator.Institution(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await institutionService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Удаление несуществуюущего <see cref="Institution"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldNotFoundException()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => institutionService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BigBallEntityNotFoundException<Institution>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Institution"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldInvalidException()
        {
            //Arrange
            var model = TestDataGenerator.Institution(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Institutions.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => institutionService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BigBallInvalidOperationException>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Institution"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Institution();
            await Context.Institutions.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => institutionService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Institutions.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Institution"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var model = mapper.Map<InstitutionModel>(TestDataGenerator.Institution());

            //Act
            Func<Task> act = () => institutionService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Institutions.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Institution"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var model = mapper.Map<InstitutionModel>(TestDataGenerator.Institution());

            //Act
            Func<Task> act = () => institutionService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<BigBallEntityNotFoundException<Institution>>()
                .WithMessage($"*{model.Id}*");
        }
       
        /// <summary>
        /// Изменение <see cref="Institution"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var model = mapper.Map<InstitutionModel>(TestDataGenerator.Institution());
            var institution = TestDataGenerator.Institution(x => x.Id = model.Id);
            await Context.Institutions.AddAsync(institution);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => institutionService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Institutions.Single(x => x.Id == institution.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.Title,
                    model.Address,
                    model.OpeningTime,
                    model.ClosingTime
                });
        }
    }
}

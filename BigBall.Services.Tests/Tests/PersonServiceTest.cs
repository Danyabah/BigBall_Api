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
    public class PersonServiceTest : BigBallContextInMemory
    {
        private readonly IPersonService personService;
        private readonly IPersonReadRepository personRead;
        private readonly IMapper mapper;

        public PersonServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });

            mapper = config.CreateMapper();
            personRead = new PersonReadRepository(Reader);
            personService = new PersonService(personRead, new PersonWriteRepository(WriterContext),
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
        /// Получение <see cref="Person"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => personService.GetByIdAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BigBallEntityNotFoundException<Person>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение <see cref="Person"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Person();
            await Context.People.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await personService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.FirstName,
                    target.LastName,
                    target.Age,
                    target.Email,
                    target.Patronymic
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Person}"/> по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await personService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Person}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Person();

            await Context.People.AddRangeAsync(target,
                TestDataGenerator.Person(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await personService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Удаление несуществуюущего <see cref="Person"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldNotFoundException()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => personService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BigBallEntityNotFoundException<Person>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Person"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldInvalidException()
        {
            //Arrange
            var model = TestDataGenerator.Person(x => x.DeletedAt = DateTime.UtcNow);
            await Context.People.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => personService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BigBallInvalidOperationException>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Person"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Person();
            await Context.People.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => personService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.People.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Person"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var model = mapper.Map<PersonModel>(TestDataGenerator.Person());

            //Act
            Func<Task> act = () => personService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.People.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Person"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var model = mapper.Map<PersonModel>(TestDataGenerator.Person());

            //Act
            Func<Task> act = () => personService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<BigBallEntityNotFoundException<Person>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение <see cref="Person"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var model = mapper.Map<PersonModel>(TestDataGenerator.Person());
            var person = TestDataGenerator.Person(x => x.Id = model.Id);
            await Context.People.AddAsync(person);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => personService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.People.Single(x => x.Id == person.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.FirstName,
                    model.LastName,
                    model.Age,
                    model.Email,
                    model.Patronymic
                });
        }
    }
}

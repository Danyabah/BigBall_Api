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
    public class TrackServiceTest : BigBallContextInMemory
    {
        private readonly ITrackService trackService;
        private readonly ITrackReadRepository trackRead;
        private readonly IMapper mapper;

        public TrackServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });

            mapper = config.CreateMapper();
            trackRead = new TrackReadRepository(Reader);
            trackService = new TrackService(trackRead, new TrackWriteRepository(WriterContext),
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
        /// Получение <see cref="Track"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => trackService.GetByIdAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BigBallEntityNotFoundException<Track>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение <see cref="Track"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Track();
            await Context.Tracks.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await trackService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Capacity,
                    target.Number
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Track}"/> по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await trackService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Track}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Track();

            await Context.Tracks.AddRangeAsync(target,
                TestDataGenerator.Track(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await trackService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Удаление несуществуюущего <see cref="Track"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldNotFoundException()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => trackService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BigBallEntityNotFoundException<Track>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Track"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldInvalidException()
        {
            //Arrange
            var model = TestDataGenerator.Track(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Tracks.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => trackService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BigBallInvalidOperationException>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Track"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Track();
            await Context.Tracks.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => trackService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Tracks.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Track"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var model = mapper.Map<TrackModel>(TestDataGenerator.Track());

            //Act
            Func<Task> act = () => trackService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Tracks.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Track"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var model = mapper.Map<TrackModel>(TestDataGenerator.Track());

            //Act
            Func<Task> act = () => trackService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<BigBallEntityNotFoundException<Track>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение <see cref="Track"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var model = mapper.Map<TrackModel>(TestDataGenerator.Track());
            var track = TestDataGenerator.Track(x => x.Id = model.Id);
            await Context.Tracks.AddAsync(track);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => trackService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Tracks.Single(x => x.Id == track.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.Capacity,
                    model.Number
                });
        }
    }
}

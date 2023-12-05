using AutoMapper;
using BigBall.Context.Contracts.Models;
using BigBall.Context.Tests;
using BigBall.Repositories.ReadRepositories;
using BigBall.Repositories.WriteRepositories;
using BigBall.Services.Contracts.Exeptions;
using BigBall.Services.Contracts.ModelsRequest;
using BigBall.Services.Contracts.ServiceContracts;
using BigBall.Services.Mapper;
using BigBall.Services.Services;
using FluentAssertions;
using System.Net.Sockets;
using Xunit;

namespace BigBall.Services.Tests.Tests
{
    public class ReservationServiceTest : BigBallContextInMemory
    {
        private readonly IReservationService reservationService;
        private readonly IMapper mapper;

        public ReservationServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });

            mapper = config.CreateMapper();

            reservationService = new ReservationService(new ReservationReadRepository(Reader), new ReservationWriteRepository(WriterContext),
                new InstitutionReadRepository(Reader),  new PaymentReadRepository(Reader),
                new PersonReadRepository(Reader), new PromotionReadRepository(Reader), 
                new TrackReadRepository(Reader), mapper, UnitOfWork);
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
        /// Получение <see cref="Reservation"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => reservationService.GetByIdAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BigBallEntityNotFoundException<Reservation>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение <see cref="Reservation"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Reservation();
            await Context.Reservations.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await reservationService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Date,
                    target.Price,
                    target.CountPeople
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Reservation}"/> по идентификаторам возвращает пустйю коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await reservationService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Reservation}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Reservation();
            await Context.Reservations.AddRangeAsync(target,
                TestDataGenerator.Reservation(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await reservationService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(0);
        }

        /// <summary>
        /// Удаление не существуюущего <see cref="Reservation"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentCinemaReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => reservationService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BigBallEntityNotFoundException<Reservation>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Reservation"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedCinemaReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Reservation(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Reservations.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => reservationService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BigBallInvalidOperationException>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Reservation"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Reservation();
            await Context.Reservations.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => reservationService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Reservations.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Reservation"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var institution = TestDataGenerator.Institution();
            var payment = TestDataGenerator.Payment();
            var person = TestDataGenerator.Person();
            var track = TestDataGenerator.Track();

            await Context.Institutions.AddAsync(institution);
            await Context.Payments.AddAsync(payment);
            await Context.People.AddAsync(person);
            await Context.Tracks.AddAsync(track);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var model = mapper.Map<ReservationModelRequest>(TestDataGenerator.Reservation());
            model.InstitutionId = institution.Id;
            model.PaymentId = payment.Id;
            model.PersonId = person.Id;
            model.TrackId = track.Id;

            //Act
            Func<Task> act = () => reservationService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Reservations.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Reservation"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var institution = TestDataGenerator.Institution();
            var payment = TestDataGenerator.Payment();
            var person = TestDataGenerator.Person();
            var track = TestDataGenerator.Track();

            await Context.Institutions.AddAsync(institution);
            await Context.Payments.AddAsync(payment);
            await Context.People.AddAsync(person);
            await Context.Tracks.AddAsync(track);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var model = mapper.Map<ReservationModelRequest>(TestDataGenerator.Reservation());
            model.InstitutionId = institution.Id;
            model.PaymentId = payment.Id;
            model.PersonId = person.Id;
            model.TrackId = track.Id;

            //Act
            Func<Task> act = () => reservationService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<BigBallEntityNotFoundException<Reservation>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение <see cref="Reservation"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var institution = TestDataGenerator.Institution();
            var payment = TestDataGenerator.Payment();
            var person = TestDataGenerator.Person();
            var track = TestDataGenerator.Track();

            await Context.Institutions.AddAsync(institution);
            await Context.Payments.AddAsync(payment);
            await Context.People.AddAsync(person);
            await Context.Tracks.AddAsync(track);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var reservation = TestDataGenerator.Reservation();
            reservation.InstitutionId = institution.Id;
            reservation.PaymentId = payment.Id;
            reservation.PersonId = person.Id;
            reservation.TrackId = track.Id;

            var model = mapper.Map<ReservationModelRequest>(reservation);

            await Context.Reservations.AddAsync(reservation);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => reservationService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Reservations.Single(x => x.Id == reservation.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.Date,
                    model.CountPeople
                });
        }
    }
}

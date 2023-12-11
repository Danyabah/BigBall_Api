using BigBall.API.Models.CreateRequest;
using BigBall.API.Validators.Reservation;
using BigBall.Context.Tests;
using BigBall.Repositories.ReadRepositories;
using BigBall.Services.Tests;
using FluentValidation.TestHelper;
using Xunit;

namespace BigBall.API.Tests.ValidatorTests.Reservation
{
    public class CreateReservationRequestValidatorTest : BigBallContextInMemory
    {
        private readonly CreateReservationRequestValidator validator;

        /// <summary>
        /// ctor
        /// </summary>
        public CreateReservationRequestValidatorTest()
        {
            validator = new CreateReservationRequestValidator( new InstitutionReadRepository(Reader),
                new PaymentReadRepository(Reader), new PersonReadRepository(Reader), new TrackReadRepository(Reader));
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorShouldError()
        {
            //Arrange
            var model = new CreateReservationRequest
            {
                CountPeople = 0, 
                Date = DateTimeOffset.Now,
                Description = "1",
                Price = 0,
                PaymentId = Guid.NewGuid(),
                PersonId = Guid.NewGuid(),
                InstitutionId = Guid.NewGuid(),
                TrackId = Guid.NewGuid()
            };

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorShouldSuccess()
        { 
            //Arrange
            var person = TestDataGenerator.Person();
            var institution = TestDataGenerator.Institution();
            var track = TestDataGenerator.Track();
            var payment = TestDataGenerator.Payment();

            await Context.People.AddAsync(person);
            await Context.Institutions.AddAsync(institution);
            await Context.Tracks.AddAsync(track);
            await Context.Payments.AddAsync(payment);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var model = new CreateReservationRequest
            {
                CountPeople = 1,
                Date = DateTimeOffset.Now.AddDays(2),
                Description = $"Name{Guid.NewGuid():N}",
                Price = 2000,
                PaymentId = payment.Id,
                PersonId = person.Id,
                InstitutionId = institution.Id,
                TrackId = track.Id
            };

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

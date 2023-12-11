using BigBall.API.Enums;
using BigBall.API.Models.Request;
using BigBall.API.Validators.Track;
using FluentValidation.TestHelper;
using Xunit;

namespace BigBall.API.Tests.ValidatorTests.Track
{
    public class TrackRequestValidatorTest
    {
        private readonly TrackRequestValidator validator;

        /// <summary>
        /// ctor
        /// </summary>
        public TrackRequestValidatorTest()
        {
            validator = new TrackRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = new TrackRequest
            {
                Id = Guid.NewGuid(),
                Number = -1,
                Capacity = 100,
                Type = (TrackTypeResponse)99
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldSuccess()
        {
            //Arrange
            var model = new TrackRequest
            {
                Id = Guid.NewGuid(),
                Number = 1,
                Capacity = 1,
                Type = (TrackTypeResponse)1
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

using BigBall.API.Enums;
using BigBall.API.Models.CreateRequest;
using BigBall.API.Validators.Track;
using FluentValidation.TestHelper;
using Xunit;

namespace BigBall.API.Tests.ValidatorTests.Track
{
    public class CreateTrackRequestValidatorTest
    {
        private readonly CreateTrackRequestValidator validator;

        /// <summary>
        /// ctor
        /// </summary>
        public CreateTrackRequestValidatorTest()
        {
            validator = new CreateTrackRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = new CreateTrackRequest
            {
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
            var model = new CreateTrackRequest
            {
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

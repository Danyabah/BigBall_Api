using BigBall.API.Models.CreateRequest;
using BigBall.API.Validators.Promotion;
using FluentValidation.TestHelper;
using Xunit;

namespace BigBall.API.Tests.ValidatorTests.Promotion
{
    public class CreatePromotionRequestValidatorTest
    {
        private readonly CreatePromotionRequestValidator validator;

        /// <summary>
        /// ctor
        /// </summary>
        public CreatePromotionRequestValidatorTest()
        {
            validator = new CreatePromotionRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = new CreatePromotionRequest
            {
                Title = "a",
                PercentageDiscount = 100
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
            var model = new CreatePromotionRequest
            {
                Title = $"Name{Guid.NewGuid():N}",
                PercentageDiscount = 15
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

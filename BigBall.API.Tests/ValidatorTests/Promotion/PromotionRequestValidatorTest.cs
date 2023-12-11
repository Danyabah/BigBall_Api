using BigBall.API.Models.Request;
using BigBall.API.Validators.Promotion;
using FluentValidation.TestHelper;
using Xunit;

namespace BigBall.API.Tests.ValidatorTests.Promotion
{
    public class PromotionRequestValidatorTest
    {
        private readonly PromotionRequestValidator validator;

        /// <summary>
        /// ctor
        /// </summary>
        public PromotionRequestValidatorTest()
        {
            validator = new PromotionRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = new PromotionRequest
            {
                Id = Guid.NewGuid(),
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
            var model = new PromotionRequest
            {
                Id = Guid.NewGuid(),
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

using BigBall.API.Models.CreateRequest;
using BigBall.API.Validators.Payment;
using FluentValidation.TestHelper;
using Xunit;

namespace BigBall.API.Tests.ValidatorTests.Payment
{
    public class CreatePaymentRequestValidatorTest
    {
        private readonly CreatePaymentRequestValidator validator;

        /// <summary>
        /// ctor
        /// </summary>
        public CreatePaymentRequestValidatorTest()
        {
            validator = new CreatePaymentRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = new CreatePaymentRequest
            {               
                Title = "a",
                CardNumber = $"Name{Guid.NewGuid():N}",
                Requisites = $"Name{Guid.NewGuid():N}"
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
            var model = new CreatePaymentRequest
            {
                Title = $"Name{Guid.NewGuid():N}",
                CardNumber = "1111222233334444",
                Requisites = $"11112222333344445"
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

using BigBall.API.Models.CreateRequest;
using BigBall.API.Validators.Institution;
using FluentValidation.TestHelper;
using Xunit;

namespace BigBall.API.Tests.ValidatorTests.Institution
{
    public class CreateInstitutionRequestValidatorTest
    {
        private readonly CreateInstitutionRequestValidator validator;

        /// <summary>
        /// ctor
        /// </summary>
        public CreateInstitutionRequestValidatorTest()
        {
            validator = new CreateInstitutionRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = new CreateInstitutionRequest
            {              
                Address = "a",
                ClosingTime = "a",
                OpeningTime = "a",
                Email = "a",
                Title = "a"
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
            var model = new CreateInstitutionRequest
            {
                Address = $"Name{Guid.NewGuid():N}",
                ClosingTime = "12:00",
                OpeningTime = "22:00",
                Email = "test@gmail.com",
                Title = $"Name{Guid.NewGuid():N}"
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

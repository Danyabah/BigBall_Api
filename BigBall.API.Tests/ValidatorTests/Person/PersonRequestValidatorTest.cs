using BigBall.API.Models.Request;
using BigBall.API.Validators.Person;
using FluentValidation.TestHelper;
using Xunit;

namespace BigBall.API.Tests.ValidatorTests.Person
{
    public class PersonRequestValidatorTest
    {
        private readonly PersonRequestValidator validator;

        /// <summary>
        /// ctor
        /// </summary>
        public PersonRequestValidatorTest()
        {
            validator = new PersonRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = new PersonRequest
            {
                Id = Guid.NewGuid(),
                Age = -1,
                Email = $"Name{Guid.NewGuid():N}",
                FirstName = "a",
                LastName = "a",
                Patronymic = "a"
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
            var model = new PersonRequest
            {
                Id= Guid.NewGuid(),
                Age = 18,
                Email = "test@gmail.com",
                FirstName = $"Name{Guid.NewGuid():N}",
                LastName = $"Name{Guid.NewGuid():N}",
                Patronymic = $"Name{Guid.NewGuid():N}"
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

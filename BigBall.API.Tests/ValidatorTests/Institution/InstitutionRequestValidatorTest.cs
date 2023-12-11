using BigBall.API.Models.CreateRequest;
using BigBall.API.Models.Request;
using BigBall.API.Validators.Institution;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BigBall.API.Tests.ValidatorTests.Institution
{
    public class InstitutionRequestValidatorTest
    {
        private readonly InstitutionRequestValidator validator;

        /// <summary>
        /// ctor
        /// </summary>
        public InstitutionRequestValidatorTest()
        {
            validator = new InstitutionRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = new InstitutionRequest
            {
                Id = Guid.NewGuid(),
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
            var model = new InstitutionRequest
            {
                Id = Guid.NewGuid(),
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

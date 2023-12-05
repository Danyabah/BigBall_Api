using BigBall.Context.Tests;
using BigBall.Repositories.Contracts.ReadRepositiriesContracts;
using BigBall.Repositories.ReadRepositories;
using BigBall.Services.Tests;
using FluentAssertions;
using Xunit;

namespace BigBall.Repositories.Tests.Tests
{
    public class PaymentReadTest : BigBallContextInMemory
    {
        private readonly IPaymentReadRepository paymentRead;

        public PaymentReadTest()
        {
            paymentRead = new PaymentReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список способов оплат
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await paymentRead.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список способов оплат
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Payment();

            await Context.Payments.AddRangeAsync(target,
                TestDataGenerator.Payment(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await paymentRead.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение способа оплаты по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await paymentRead.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение способа оплаты по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Payment();
            await Context.Payments.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await paymentRead.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка способов оплат по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsShouldReturnEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await paymentRead.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка способов оплат по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsShouldReturnValue()
        {
            //Arrange
            var target1 = TestDataGenerator.Payment();
            var target2 = TestDataGenerator.Payment(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.Payment();
            var target4 = TestDataGenerator.Payment();
            await Context.Payments.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await paymentRead.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }

        /// <summary>
        /// Поиск способа оплаты в коллекции по идентификатору (true)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnTrue()
        {
            //Arrange
            var target1 = TestDataGenerator.Payment();
            await Context.Payments.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await paymentRead.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeTrue();
        }

        /// <summary>
        /// Поиск способа оплаты в коллекции по идентификатору (false)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnFalse()
        {
            //Arrange
            var target1 = Guid.NewGuid();

            // Act
            var result = await paymentRead.IsNotNullAsync(target1, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }

        /// <summary>
        /// Поиск удаленного способа оплаты в коллекции по идентификатору
        /// </summary>
        [Fact]
        public async Task IsNotNullDeletedEntityReturnFalse()
        {
            //Arrange
            var target1 = TestDataGenerator.Payment(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Payments.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await paymentRead.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }
    }
}

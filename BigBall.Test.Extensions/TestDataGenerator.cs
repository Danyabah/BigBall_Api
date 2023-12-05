using BigBall.Context.Contracts.Models;

namespace BigBall.Services.Tests
{
    public static class TestDataGenerator
    {
        static public Institution Institution(Action<Institution>? settings = null)
        {
            var result = new Institution
            {
                Title = $"{Guid.NewGuid():N}",
                Address = $"{Guid.NewGuid():N}",
                OpeningTime = "12:00",
                ClosingTime = "22:00"
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static public Payment Payment(Action<Payment>? settings = null)
        {
            var result = new Payment
            {
                Title = $"{Guid.NewGuid():N}",
                CardNumber = "1111222233334444",
                Requisites = "12345678912345678"

            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static public Person Person(Action<Person>? settings = null)
        {
            var result = new Person
            {
                FirstName = $"{Guid.NewGuid():N}",
                LastName = $"{Guid.NewGuid():N}",
                Patronymic = $"{Guid.NewGuid():N}",
                Age = 56,
                Email = $"{Guid.NewGuid():N}"
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static public Promotion Promotion(Action<Promotion>? settings = null)
        {
            var result = new Promotion
            {
                Title = $"{Guid.NewGuid():N}",
                PercentageDiscount = 10
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static public Track Track(Action<Track>? settings = null)
        {
            var result = new Track
            {
                Number = 1,
                Capacity = 2,
                Type = Context.Contracts.Enums.TrackType.Fast,
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static public Reservation Reservation(Action<Reservation>? settings = null)
        {
            var result = new Reservation
            {
                Date = DateTimeOffset.Now.AddDays(3),
                Price = 1000,
                CountPeople = 1,
                Description = $"{Guid.NewGuid():N}",
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }      
    }
}

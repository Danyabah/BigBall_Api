using BigBall.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace BigBall.Context.Contracts
{
    public interface IBigBallContext
    {
        /// <summary>Список <inheritdoc cref="Institution"/></summary>
        DbSet<Institution> Institutions { get; }

        /// <summary>Список <inheritdoc cref="Payment"/></summary>
        DbSet<Payment> Payments { get; }

        /// <summary>Список <inheritdoc cref="Person"/></summary>
        DbSet<Person> People { get; }

        /// <summary>Список <inheritdoc cref="Promotion"/></summary>
        DbSet<Promotion> Promotions { get; }

        /// <summary>Список <inheritdoc cref="Track"/></summary>
        DbSet<Track> Tracks { get; }

        /// <summary>Список <inheritdoc cref="Reservation"/></summary>
        DbSet<Reservation> Reservations { get; }
    }
}

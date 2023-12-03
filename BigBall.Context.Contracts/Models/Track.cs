using BigBall.Context.Contracts.Enums;

namespace BigBall.Context.Contracts.Models
{
    /// <summary>
    /// Сущность дорожки
    /// </summary>
    public class Track : BaseAuditEntity
    {
        /// <summary>
        /// Номер
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Вместимость
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// <<see cref="TrackType"/>
        /// </summary>
        public TrackType Type { get; set; } = TrackType.Polished;

        public ICollection<Reservation> Reservations { get; set; }
    }
}

using BigBall.API.Enums;

namespace BigBall.API.Models.Response
{
    /// <summary>
    /// Модель ответа дорожки
    /// </summary>
    public class TrackResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Вместимость
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// <<see cref="TrackTypeResponse"/>
        /// </summary>
        public TrackTypeResponse Type { get; set; } = TrackTypeResponse.Polished;
    }
}

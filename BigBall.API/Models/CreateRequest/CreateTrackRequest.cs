using BigBall.API.Enums;

namespace BigBall.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания дорожки
    /// </summary>
    public class CreateTrackRequest
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
        public TrackTypeResponse Type { get; set; } = TrackTypeResponse.Polished;
    }
}

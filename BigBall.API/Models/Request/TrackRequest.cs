using BigBall.API.Models.CreateRequest;

namespace BigBall.API.Models.Request
{
    /// <summary>
    /// Модель запроса создания дорожки
    /// </summary>
    public class TrackRequest : CreateTrackRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}

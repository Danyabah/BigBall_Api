using BigBall.API.Models.CreateRequest;

namespace BigBall.API.Models.Request
{
    /// <summary>
    /// Модель запроса создания заказа
    /// </summary>
    public class ReservationRequest : CreateReservationRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}

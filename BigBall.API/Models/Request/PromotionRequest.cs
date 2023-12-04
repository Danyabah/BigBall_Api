using BigBall.API.Models.CreateRequest;

namespace BigBall.API.Models.Request
{
    /// <summary>
    /// Модель запроса создания скидок
    /// </summary>
    public class PromotionRequest : CreatePromotionRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}

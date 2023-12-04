namespace BigBall.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания промокода
    /// </summary>
    public class CreatePromotionRequest
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Процентная скидка
        /// </summary>
        public int PercentageDiscount { get; set; }
    }
}

namespace BigBall.API.Models.Response
{
    /// <summary>
    /// Модель ответа скидок и акций
    /// </summary>
    public class PromotionResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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

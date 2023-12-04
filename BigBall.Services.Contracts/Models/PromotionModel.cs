namespace BigBall.Services.Contracts.Models
{
    /// <summary>
    /// Модель промокодов
    /// </summary>
    public class PromotionModel
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

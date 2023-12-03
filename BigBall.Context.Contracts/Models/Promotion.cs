namespace BigBall.Context.Contracts.Models
{
    /// <summary>
    /// Сущность промокодов и акций
    /// </summary>
    public class Promotion : BaseAuditEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Процентная скидка
        /// </summary>
        public int PercentageDiscount { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}

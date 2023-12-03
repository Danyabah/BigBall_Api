namespace BigBall.Context.Contracts.Models
{
    /// <summary>
    /// Способ оплаты
    /// </summary>
    public class Payment : BaseAuditEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Реквизиты
        /// </summary>
        public string Requisites { get; set; } = string.Empty;

        /// <summary>
        /// Номер карты
        /// </summary>
        public string CardNumber { get; set; } = string.Empty;

        public ICollection<Reservation> Reservations { get; set; }
    }
}
